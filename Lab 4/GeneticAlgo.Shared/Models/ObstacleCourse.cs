using System.Numerics;

namespace GeneticAlgo.Shared.Models;

public sealed class ObstacleCourse
{
    private ObstacleCourse()
    {
    }

    private static ObstacleCourse _instance;

    private static List<BarrierCircle> _obstacles = new List<BarrierCircle>();

    public static ObstacleCourse GetInstance()
    {
        if (_instance == null)
        {
            _instance = new ObstacleCourse();
        }

        return _instance;
    }

    public bool IsDotCrossingObstacles(float x, float y)
    {
        foreach (var obstacle in _obstacles)
        {
            if (Vector2.Distance(new Vector2(x, y), obstacle.Center) <= obstacle.Radius / 100)
                return true;
        }
        //return _obstacles.Any(obs => Vector2.Distance(new Vector2(x, y), obs.Center) <= obs.Radius/100);
        return false;
    }

    public List<BarrierCircle> GetObstacles()
    {
        return _obstacles;
    }

    public void SetBarriers(List<BarrierCircle> obstacles)
    {
        _obstacles = obstacles;
    }
}