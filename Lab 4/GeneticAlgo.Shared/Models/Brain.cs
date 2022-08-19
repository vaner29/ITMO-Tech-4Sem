using System.Numerics;

namespace GeneticAlgo.Shared.Models;

public class Brain
{
    private readonly Random _random;
    public List<Vector2> Genes = new List<Vector2>();
    public int GeneNumber = BrainConfiguration.GetInstance().GeneAmount;
    public int Step = 0;

    public Brain()
    {
        _random = Random.Shared;
        Randomize();
    }

    public void Randomize()
    {
        for (int i = 0; i < GeneNumber; i++)
        {
            Genes.Add(new Vector2((float)(_random.NextDouble() * 2 - 1.0f) * BrainConfiguration.GetInstance().Fmax,
                (float)(_random.NextDouble() * 2 - 1.0f) * BrainConfiguration.GetInstance().Fmax));
        }
    }

    public Brain Clone()
    {
        var clone = new Brain();
        for (var i = 0; i < GeneNumber; i++)
        {
            clone.Genes[i] = Genes[i];
        }

        return clone;
    }

    public void Mutate()
    {
        var mutationRate = 0.01f; //mutation chance
        for (var i = 0; i < GeneNumber; i++)
        {
            var factorY = (float)(_random.NextDouble());
            if (factorY < mutationRate)
            {
                Genes[i] = new Vector2((float)(_random.NextDouble() * 2 - 1.0f) * BrainConfiguration.GetInstance().Fmax,
                    (float)(_random.NextDouble() * 2 - 1.0f) * BrainConfiguration.GetInstance().Fmax);
            }
        }
        
    }
}