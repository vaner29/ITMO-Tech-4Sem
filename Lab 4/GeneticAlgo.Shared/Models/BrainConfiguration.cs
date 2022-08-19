namespace GeneticAlgo.Shared.Models;

public sealed class BrainConfiguration
{
    private BrainConfiguration()
    {
    }

    private static BrainConfiguration _instance;
    public float Fmax = 0.001f;
    public int GeneAmount = 500;

    public static BrainConfiguration GetInstance()
    {
        if (_instance == null)
            _instance = new BrainConfiguration();
        return _instance;
    }
}