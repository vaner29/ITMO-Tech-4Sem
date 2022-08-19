using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Numerics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using GeneticAlgo.Shared;
using GeneticAlgo.Shared.Models;
using GeneticAlgo.Shared.Tools;
using GeneticAlgo.WpfInterface.Tools;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Serilog;

namespace GeneticAlgo.WpfInterface
{
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window
    {

        private bool _isActive = true;

        private IStatisticsConsumer _statisticsConsumer;
        private readonly IExecutionContext _executionContext;
        private readonly ExecutionConfiguration _configuration;
        private int _dotAMount;

        public PlotModel ScatterModel { get; private set; }
        public PlotModel BarModel { get; private set; }

        public MainWindow()
        {
            var runConfig = JsonConvert.DeserializeObject<RunConfiguration>(File.ReadAllText(@"..\..\..\Config.json"));
            var obstacles = ObstacleCourse.GetInstance();
            obstacles.SetBarriers(runConfig.Obstacles);
            var brains = BrainConfiguration.GetInstance();
            brains.Fmax = runConfig.FMax;
            brains.GeneAmount = runConfig.IterationAmount;
            _dotAMount = runConfig.DotAmount;
            
            InitializeComponent();

            Logger.Init();
            
            _executionContext = new ExecutionContext(_dotAMount, runConfig.MaxGenerations);
            _configuration = new ExecutionConfiguration(TimeSpan.FromMilliseconds(runConfig.TimeDelay));

            

            InitPlots();

            PlotSample.Model = ScatterModel;
            PlotSample2.Model = BarModel;
            var worker = new BackgroundWorker();
            worker.DoWork += StartSimulation;
            worker.RunWorkerAsync();
        }

        public void InitPlots()
        {
            var lineSeries = new ScatterSeries
            {
                MarkerSize = 3,
                MarkerStroke = OxyColors.ForestGreen,
                MarkerType = MarkerType.Plus,
            };
            
            var bestSeries = new ScatterSeries
            {
                MarkerSize = 4,
                MarkerStroke = OxyColors.DarkRed,
                MarkerType = MarkerType.Plus,
                MarkerStrokeThickness = 2
            };

            var circleSeries = new ScatterSeries
            {
                MarkerFill = OxyColors.Red,
                MarkerType = MarkerType.Circle,
            };

            var targetSeries = new ScatterSeries()
            {
                MarkerFill = OxyColors.Black,
                MarkerType = MarkerType.Circle
            };

            ScatterModel = new PlotModel
            {
                Title = "Points",
                Series = { circleSeries, lineSeries, targetSeries, bestSeries },
                Axes = { 
                    new LinearAxis()
                {
                    Minimum = -2,
                    Maximum = 2,
                    Position = AxisPosition.Bottom
                },
                    new LinearAxis()
                    {
                        Minimum = -2,
                        Maximum = 2,
                        Position = AxisPosition.Left
                    }
                }
            };

            var barSeries = new LinearBarSeries
            {
                DataFieldX = nameof(FitnessModel.X),
                DataFieldY = nameof(FitnessModel.Y),
            };

            BarModel = new PlotModel
            {
                Title = "Fitness",
                Series = { barSeries },
            };

            _statisticsConsumer = new PlotStatisticConsumer(circleSeries, lineSeries, targetSeries, bestSeries, barSeries, _dotAMount);
        }
        

        private void StartSimulation(object? sender, DoWorkEventArgs e)
        {
            while (_isActive)
            {
                StartSimulator();
            }
            
        }

        public async void StartSimulator()
        {
            if (!_isActive)
                return;

            Log.Information("Start simulation");
            _isActive = true;

            IterationResult iterationResult;

            do
            {
                iterationResult = await _executionContext.ExecuteIterationAsync();
                _executionContext.ReportStatistics(_statisticsConsumer);

                ScatterModel.InvalidatePlot(true);
                BarModel.InvalidatePlot(true);

                Task.Delay(_configuration.IterationDelay).Wait();
                
                if (iterationResult == IterationResult.SolutionFound)
                {
                    Log.Information("Finished generation {0} with solution found", _executionContext.GetGenerations());
                }
            
                else Log.Information("Finished generation {0} without finding the solution", _executionContext.GetGenerations());
            }
            while (/*iterationResult == IterationResult.IterationFinished && */_isActive);

            _isActive = false;
        }
    }
}
