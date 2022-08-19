namespace GeneticAlgo.Shared.Models;

public class Population
{
    public List<Dot> Dots = new List<Dot>();
    public int MinStep = BrainConfiguration.GetInstance().GeneAmount;
    public float Fitness;
    private Random _random;
    public int Generation = 1;
    public Dot BestDot;

    public Population(int dotNumber)
    {
        BestDot = new Dot();
        for (int i = 0; i < dotNumber; i++)
        {
            Dots.Add(new Dot());
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
        var bestDot = new Dot();
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