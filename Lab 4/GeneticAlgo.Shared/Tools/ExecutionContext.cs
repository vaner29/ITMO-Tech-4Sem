using GeneticAlgo.Shared.Models;

namespace GeneticAlgo.Shared.Tools;

public class ExecutionContext : IExecutionContext
{
    private Population _population;
    private int _maxGenerations;
    private Statistic[] _dotStatistics;

    public ExecutionContext(int pointCount, int maxGenerations)
    {
        _maxGenerations = maxGenerations;
        _population = new Population(pointCount);
        _dotStatistics = new Statistic[_population.Dots.Count];
    }
    

    public Task<IterationResult> ExecuteIterationAsync()
    {
        if (_population.Dots.Any(dot => dot.ReachedGoal))
            return Task.FromResult(IterationResult.SolutionFound);
        if (_population.Generation > _maxGenerations)
            return Task.FromResult(IterationResult.SolutionCannotBeFound);
        return Task.FromResult(IterationResult.IterationFinished);
    }

    public void ReportStatistics(IStatisticsConsumer statisticsConsumer)
    {
        _population.Update();
        _population.CalculateFitness();
        var i = 0;
        foreach (var dot in _population.Dots)
        {
            _dotStatistics[i] = new Statistic(i, new Point(dot.Position.X, dot.Position.Y), dot.Fitness);
            i++;
        }

        var bestDot = new Dot();

        if (_population.Generation > 1)
        {
            bestDot = _population.Dots.FirstOrDefault(dot => dot.IsBest = true);
        }
        var bestDotStatistic = new Statistic(_population.Dots.Count + 1, new Point(bestDot.Position.X,
            bestDot.Position.Y), bestDot.Fitness);
        statisticsConsumer.Consume(_dotStatistics, bestDotStatistic);
        if (_population.Genocide())
        {
            _population.CalculateFitnessSum();
            _population.NaturalSelection();
            _population.MutateChildren();
        }

    }

    public int GetGenerations()
    {
        return _population.Generation;
    }
}