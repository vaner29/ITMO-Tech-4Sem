using System.Collections.Immutable;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Running;

namespace SharpBenchmarking;

[MemoryDiagnoser()]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn()]
public class SortingBenchmarks
{
    private SortAlgorithms _sortAlgosAlgorithms = new();
    public List<int> ArrayToSort { get; set; }

    [Params( 10,100,1000,10000)] public int Length { get; set; }

    [GlobalSetup]
    public void CreateArray()
    {
        ArrayToSort = new List<int>(new int[Length]);
    }

    [IterationSetup]
    public void FillArray()
    {
        Random rand = new Random();
        for (int i = 0; i < ArrayToSort.Count; i++)
        {
            ArrayToSort[i] = rand.Next(1, 10000);
        }
    }

    [Benchmark]
    public void SortArrayWithBubble()
    {
        _sortAlgosAlgorithms.BubbleSort(ArrayToSort);
    }
    
    [Benchmark]
    public void SortArrayWithMerge()
    {
        _sortAlgosAlgorithms.MergeSort(ArrayToSort);
    }

    [Benchmark]
    public void SortArrayWithInBuiltSort()
    {
        ArrayToSort.Sort();
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var results = BenchmarkRunner.Run<SortingBenchmarks>();
        }
    }
}