using System.Numerics;

namespace GeneticAlgo.Shared.Models;

public class Dot
{
    public Vector2 Position = new Vector2(0, 0);
    public Vector2 Velocity = new Vector2(0, 0);
    public Vector2 Acceleration = new Vector2(0, 0);
    public Brain Chromosome = new Brain();
    public bool Dead = false;
    public bool ReachedGoal = false;
    public bool IsBest = false;
    public float Fitness = 0;
    public bool HitCircle = false;

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
            else if (ObstacleCourse.GetInstance().IsDotCrossingObstacles(Position.X, Position.Y))
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

    public void CalculateFitness()
    {
        if (ReachedGoal)
        {
            Fitness = BrainConfiguration.GetInstance().GeneAmount * BrainConfiguration.GetInstance().GeneAmount * 10000.0f/(Chromosome.Step * Chromosome.Step);
        }
        else
        {
            Fitness = 1.0f /
                      (Vector2.Distance(Position, new Vector2(1, 1)) * Vector2.Distance(Position, new Vector2(1, 1)));
        }

        if (HitCircle)
            Fitness *= 0.1f;
    }

    public Dot GetChild()
    {
        var child = new Dot();
        child.Chromosome = Chromosome.Clone();
        return child;
    }
}