namespace GeneticAlgo.Shared.Models;

public class RunConfiguration
{
    public int TimeDelay { get; set; }
    public int DotAmount { get; set; }
    public int IterationAmount { get; set; }
    public int MaxGenerations { get; set; }
    public float FMax { get; set; }
    public List<BarrierCircle> Obstacles = new List<BarrierCircle>();
}