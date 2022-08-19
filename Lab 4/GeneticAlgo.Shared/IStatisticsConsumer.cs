using GeneticAlgo.Shared.Models;

namespace GeneticAlgo.Shared;

public interface IStatisticsConsumer
{
    void Consume(IReadOnlyList<Statistic> statistics, Statistic bestDot);
}