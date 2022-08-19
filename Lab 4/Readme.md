# Отчёт по лабе 4

## Шаг 1, Генетический алгоритм

### Точки и кружочки

Написал максимально простой генетический алгоритм:
Точка имеет позицию, скорость и ускорение, флаги для определения состояния, список объектов на которые нельзя натыкаться и мозг (хромосома), отвечающая за ускорение в каждый момент времени.

```cs
using System.Numerics;
using GeneticAlgo.Shared.Models;

namespace GeneticAlgo.Core;

public class Dot
{
    public Vector2 Position = new Vector2(0, 0);
    public Vector2 Velocity = new Vector2(0, 0);
    public Vector2 Acceleration = new Vector2(0, 0);
    public Brain Chromosome = new Brain(500);
    public bool Dead = false;
    public bool ReachedGoal = false;
    public bool IsBest = false;
    public float Fitness = 0;
    public bool HitCircle = false;
    public ObstacleCourse Obstacles;

    public Dot(ObstacleCourse obstacleCourse)
    {
        Obstacles = obstacleCourse;
    }

    public void Move()
    {
        if (Chromosome.Genes.Count > Chromosome.Step)
        {
            Acceleration = Chromosome.Genes[Chromosome.Step];
            Chromosome.Step++;
        }
        else
        {
            Dead = true;
        }
        Velocity += Acceleration;
        Position += Velocity;
    }

    public void Update()
    {
        if (!Dead && !ReachedGoal)
        {
            Move();
            if (Position.X <= -2 || Position.X >= 2 || Position.Y <= -2 || Position.Y >= 2)
            {
                Dead = true;
            }
            else if (Obstacles.IsDotCrossingObstacles(Position.X, Position.Y))
            {
                Dead = true;
                HitCircle = true;
            }
            else if (Vector2.Distance(Position, new Vector2(1, 1)) <= 0.01)
            {
                ReachedGoal = true;
            }
        }
    }
```

Изначально в хромосоме движения (гены) задаются рандомно


```cs
using System.Numerics;

namespace GeneticAlgo.Shared.Models;

public class Brain
{
    private readonly Random _random;
    public List<Vector2> Genes = new List<Vector2>();
    public int GeneNumber;
    public int Step = 0;

    public Brain(int geneNumber)
    {
        GeneNumber = geneNumber;
        _random = Random.Shared;
        Randomize();
    }

    public void Randomize()
    {
        for (int i = 0; i < GeneNumber; i++)
        {
            Genes.Add(new Vector2((float)(_random.NextDouble() * 2 - 1.0f) * 0.001f,
                (float)(_random.NextDouble() * 2 - 1.0f) * 0.001f));
        }
    }
```

Фитнесс-функция работает следующим образом:
1) Если точка достигла цели, фитнесс считается обратно количеству шагов
2) Если не достигла, то обратно пропорционально оставшемуся расстоянию
3) Если при этом точка ударилась в препятствие, фитнесс уменьшается в 10 раз

```cs
public void CalculateFitness()
    {
        if (ReachedGoal)
        {
            Fitness = 2500000000.0f/(Chromosome.Step * Chromosome.Step);
        }
        else
        {
            Fitness = 1.0f /
                      (Vector2.Distance(Position, new Vector2(1, 1)) * Vector2.Distance(Position, new Vector2(1, 1)));
        }

        if (HitCircle)
            Fitness *= 0.1f;
    }

```
Поколения изменяются следующим образом:
Все точки сдвигаются по указанию мозга, считается их фитнесс, новое расположение точек зарисовывается. Если все точки погибли или пришли к цели, рождается новое поколение.

```cs
public void ReportStatistics(IStatisticsConsumer statisticsConsumer)
    {
        _population.Update();
        _population.CalculateFitness();
        var statistics = new Statistic[_population.Dots.Count];
        var i = 0;
        foreach (var dot in _population.Dots)
        {
            statistics[i] = new Statistic(i, new Point(dot.Position.X, dot.Position.Y), dot.Fitness);
            i++;
        }

        var bestDot = new Dot(_obstacleCourse);

        if (_population.Generation > 1)
        {
            bestDot = _population.Dots.FirstOrDefault(dot => dot.IsBest = true);
        }
        var bestDotStatistic = new Statistic(_population.Dots.Count + 1, new Point(bestDot.Position.X,
            bestDot.Position.Y), bestDot.Fitness);
        var obstacles = _obstacleCourse;
        var circles = obstacles.GetObstacles().ToArray();
        statisticsConsumer.Consume(statistics, circles, bestDotStatistic);
        if (_population.Genocide())
        {
            _population.CalculateFitnessSum();
            _population.NaturalSelection();
            _population.MutateChildren();
        }

    }
```
Зарождение нового поколения происхоодит в популяции

Сначала считается общий фитнесс всех особей, затем естественный отбор:
Чем больше у точки фитнесс, тем больше шанс что мы воспользуемся её ребёнком, таким образом всегда остается разнообразие особей.
Затем каждый ген каждого ребёнка с настраиваемым шансом может мутировать. Лучшая точка поколения всегда записывается и сохраняется как есть. Так же новым точкам запрещается делать шагов больше, чем понадобилось какой-либо точке ранее чтобы достичь цели.

```cs
using GeneticAlgo.Core;

namespace GeneticAlgo.Shared.Models;

public class Population
{
    public List<Dot> Dots = new List<Dot>();
    public int MinStep = 500;
    public float Fitness;
    private Random _random;
    public int Generation = 1;
    public ObstacleCourse Obstacles;
    public Dot BestDot;

    public Population(int dotNumber, ObstacleCourse obstacles)
    {
        Obstacles = obstacles;
        BestDot = new Dot(obstacles);
        for (int i = 0; i < dotNumber; i++)
        {
            Dots.Add(new Dot(obstacles));
        }
        _random = Random.Shared;
    }

    public void Update()
    {
        foreach (var dot in Dots)
        {
            if (dot.Chromosome.Step > MinStep)
            {
                dot.Dead = true;
            }
            else
            {
                dot.Update();
            }
        }
    }

    public void CalculateFitness()
    {
        foreach (var dot in Dots)
        {
            dot.CalculateFitness();
        }
    }

    public void CalculateFitnessSum()
    {
        Fitness = 0;
        CalculateFitness();
        foreach (var dot in Dots)
        {
            Fitness += dot.Fitness;
        }
    }

    public bool Genocide()
    {
        return Dots.All(dot => dot.Dead || dot.ReachedGoal);
    }

    public void NaturalSelection()
    {
        var newDots = new Dot[Dots.Count];
        CalculateFitnessSum();
        SetBestDot();
        newDots[0] = BestDot.GetChild();
        newDots[0].IsBest = true;
        for (int i = 1; i < Dots.Count; i++)
        {
            var parent = SelectParent();
            newDots[i] = (parent.GetChild());
        }
        

        Dots = newDots.ToList();
        Generation++;

    }

    public void SetBestDot()
    {
        float max = 0;
        var bestDot = new Dot(Obstacles);
        foreach (var dot in Dots)
        {
            if (dot.Fitness > max)
            {
                max = dot.Fitness;
                bestDot = dot;
            }
        }
        
        Console.WriteLine();
        BestDot = bestDot;
        BestDot.IsBest = true;

        if (bestDot.ReachedGoal)
        {
            MinStep = bestDot.Chromosome.Step;
            Console.WriteLine("step: " + MinStep);
        }
    }

    public Dot SelectParent()
    {
        var factorX = (float)(_random.NextDouble()*Fitness);
        float runningSum = 0;
        if (factorX == 0)
            return Dots.First();
        foreach (var dot in Dots)
        {
            runningSum += dot.Fitness;
            if (runningSum >= factorX)
                return dot;
        }

        throw new Exception();
    }

    public void MutateChildren()
    {
        foreach (var dot in Dots)
        {  
            if (!dot.IsBest)
                dot.Chromosome.Mutate();
        }
    }
}
```

___

## Шаг 2, Метрики и Километрики

### dotMemory

Тестирование проходило в следующих сетапах:
Time delay = 10ms, dotAmount = 10, geneAmount = 100
Time delay = 10ms, dotAmount = 100, geneAmount = 200
Time delay = 10ms, dotAmount = 500, geneAmount = 500
Time delay = 1000ms, dotAmount = 500, geneAmount = 500
Time delay = 10ms, dotAmount = 1000, geneAmount = 500

Метрики памяти:
![dm1010100](https://user-images.githubusercontent.com/37649552/168180243-ef6e7b02-9b3e-4a6c-a568-9331265f267a.png)
![dm10100200](https://user-images.githubusercontent.com/37649552/168180259-d8af4083-b497-4c0d-b460-e28ae33306a2.png)
![dm10500500](https://user-images.githubusercontent.com/37649552/168180265-762ebff2-f518-4172-b701-3ca9bf6ef3e6.png)
![dm1000500500](https://user-images.githubusercontent.com/37649552/168180271-5c67e5fc-18e8-45e2-8df5-c96918e9613b.png)
![dotMemory1](https://user-images.githubusercontent.com/37649552/168128809-56ca4191-61e8-4035-b271-d1ea9e727312.png)

Метрики времени:
![dt1010100](https://user-images.githubusercontent.com/37649552/168180315-cb3fd512-cb08-4a47-ad07-e298649f3445.png)
![dt10100200](https://user-images.githubusercontent.com/37649552/168180320-4489ba58-073e-4418-8195-c54164ff6387.png)
![dt10500500](https://user-images.githubusercontent.com/37649552/168180325-7367cf7a-fe96-4a01-8997-f895663176b0.png)
![dt1000500500](https://user-images.githubusercontent.com/37649552/168180329-04488178-728b-40bf-a6cf-9601ed9e0546.png)
![DotTrace2](https://user-images.githubusercontent.com/37649552/168128909-1dc7913c-b98e-4a10-a299-9115126becb8.png)


Память не утекает, но GC работает, а значит часто приходится высвобождать память, что окей при переходе между поколениями, но не очень хорошо в остальное время, что-то можно попытаться поправить

Большая часть времени уходит на отрисовку и совсем немного на алгоритмы. Я по жизни вообще ничего в отрисовке не понимаю, так что не уверен, можно ли это исправить, но как минимум есть возможность отрисовывать некоторые объекты (препятствия) не по несколько раз, а всего 1, и не стирать их после этого.
___

## Шаг 3, Оптимизация

Первое, что я сделал - вместо засовывания объектов-препятствий в каждую точку сделал из класса синглтон. То же самое я сделал для конфигурации мозга, чтобы удобнее было изменять точки.
В методах собирания статистики отрисовки вместо того чтобы выделять память на массив каждый раз с нуля, я выделял её один раз и затем изменял. Изменил так же и фитнесс-функцию, чтобы она зависела от максимального возможного количества итераций.
Объекты-препятствия теперь отрисовываются только один раз в начале, а не каждую итерацию заново


## Шаг 4, Километрики, опять

Изменения дотмемори и доттрейса после фиксов:

![dm1010100](https://user-images.githubusercontent.com/37649552/168180469-610ecde7-d638-43e5-8515-b0190df1e633.png)
![dm10100200](https://user-images.githubusercontent.com/37649552/168180470-66e58e26-4f1d-4d8e-9666-4afdd68d07e5.png)
![dm10500500](https://user-images.githubusercontent.com/37649552/168180472-b72f90dc-c75e-40ee-bf21-754f0c500bbc.png)
![dm1000500500](https://user-images.githubusercontent.com/37649552/168180467-3d834fa7-916f-41b8-8485-140529c7cd3a.png)
![absabsa](https://user-images.githubusercontent.com/37649552/168128950-9dc58c77-000c-41d4-a151-1ec07436ec65.png)

![DotTrace4](https://user-images.githubusercontent.com/37649552/168128966-10fd3c95-f53c-426d-b07a-bf2d501aae8d.png)
![dt1010100](https://user-images.githubusercontent.com/37649552/168180494-6b3e1959-1f93-4064-89f7-0bda84fd8be9.png)
![dt10100200](https://user-images.githubusercontent.com/37649552/168180488-8c7c09f8-7a18-468f-82ee-fb8bda4bcf42.png)
![dt10500500](https://user-images.githubusercontent.com/37649552/168180490-9cd2c97d-d9ab-4b67-b148-4d47df5c27ce.png)
![dt1000500500](https://user-images.githubusercontent.com/37649552/168180493-f11d9f84-2823-41e4-a4b0-c7d894ff4373.png)

Относительно GC особо ничего исправить не вышло, память всё ещё выделяется и сбрасывается, что, кажется, неизбежно при апдейте и отрисовке точек, но если сравнивать само количество выделенной памяти - на больших тестах можно увидеть, что затраты памяти стабильно уменьшаются, то есть что-то оптимизировать удалось.
По мнению райдера половина памяти у меня уходит на встроенную в oxyplot функцию invalidateplot, с чем я ничего не могу сделать при всём желании, остальные большие затраты уходят на отрисовку, которую я оптимизровал как мог даже относительно изначального шаблона. 

По времени в общем и целом всё то же самое, но опять же на больших тестах можно увидеть что чуть меньше времени уходит на отрисовку, значит оптимизация прошла не просто так.

Саму работу функции по времени оптимизровать проблематично, ибо это околорандом и нужно проводить сотни тестов чтобы заметить изменения в выборках, да и фитнесс-функция вроде работает на ура
