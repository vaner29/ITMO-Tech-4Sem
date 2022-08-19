using System.Collections.Generic;
using System.Linq;
using GeneticAlgo.Shared;
using GeneticAlgo.Shared.Models;
using OxyPlot.Series;

namespace GeneticAlgo.WpfInterface.Tools;

public class PlotStatisticConsumer : IStatisticsConsumer
{
    private readonly ScatterSeries _circleSeries;
    private readonly ScatterSeries _scatterSeries;
    private readonly LinearBarSeries _linearBarSeries;
    private readonly ScatterSeries _bestSeries;

    public PlotStatisticConsumer(ScatterSeries circleSeries, ScatterSeries scatterSeries, ScatterSeries targetSeries, ScatterSeries bestSeries, LinearBarSeries linearBarSeries, int dotAmount)
    {
        _circleSeries = circleSeries;
        _scatterSeries = scatterSeries;
        _scatterSeries.Points.AddRange(Enumerable.Repeat(new ScatterPoint(0 ,0), dotAmount));
        _linearBarSeries = linearBarSeries;
        _bestSeries = bestSeries;
        targetSeries.Points.Add(new ScatterPoint(1, 1, 3));
        foreach (var circle in ObstacleCourse.GetInstance().GetObstacles())
        {
            _circleSeries.Points.Add(new ScatterPoint(circle.Center.X, circle.Center.Y, circle.Radius));
        }
    }

    public void Consume(IReadOnlyList<Statistic> statistics, Statistic bestDot)
    {
        for (int i = 0; i < statistics.Count; i++)
        {
            _scatterSeries.Points[i] = new ScatterPoint(statistics[i].Point.X, statistics[i].Point.Y);
        }
        
        _bestSeries.Points.Clear();
        _bestSeries.Points.Add(new ScatterPoint(bestDot.Point.X, bestDot.Point.Y));

        _linearBarSeries.ItemsSource = statistics
            .Select(s => new FitnessModel(s.Id, s.Fitness))
            .ToArray();
    }
}