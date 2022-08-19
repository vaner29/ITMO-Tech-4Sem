using GeneticAlgo.Shared.Models;

namespace GeneticAlgo.Shared
{
    public interface IExecutionContext
    {
        Task<IterationResult> ExecuteIterationAsync();
        void ReportStatistics(IStatisticsConsumer statisticsConsumer);
        int GetGenerations();
    }
}