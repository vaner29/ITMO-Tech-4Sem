# Отчёт по лабе

## Таска 1, Интеропы

### Использование C++ в C#

Написал в общем-то Hello World с нюансом. 

```cpp
#include "pch.h"
#include <cstdio>
extern "C"
{

    __declspec (dllexport) void __stdcall helloThere()
    {

        printf("General Kenobi");

    }

}
```

Проект делал в вижуалке, поэтому как темплейт использовал dll-библиотеку, после чего 
просто забилдил проект - появилась dll-ка, функции из которой уже можно вызывать в C#
 при помощи dllexport

```cs
using System.Runtime.InteropServices;

namespace CLibUse;

class Program
{
    [DllImport(@"D:\work\TechLab1\C#\CLibUse\SharpDll.dll")]
    static extern void helloThere();
    
    static void Main(string[] args)
    {
        helloThere();
    }
}
```

### Использование C++ в Java

По факту всё то же самое, но в названиие функции, которая будет использоваться, 
вставил Java_<Название класса>_<название метода>, потому что индус сказал что так надо, 
и это заработало, так что я и не против. В джаве надо просто подгрузить дллку как библиотеку и 
так же объявить метод, всё.

```java
public class CMyClass {
    public CMyClass() {
    }

    public native void helloThere();

    public static void main(String[] args) {
        (new CMyClass()).helloThere();
    }

    static {
        System.loadLibrary("MyJavaLib");
    }
}
```

___

## Таска 2, смешные функции F# и Scala в C# и Java соответственно

### F# в C#

В Фарше я воспользовался Discriminated Union'ом из которого сделал чето типа иерархии объектов,
 чтобы потом декомпилировать и посмотреть что будет

```fs
module FarshLibary

type Shape =
| Circle of float
| EquilateralTriangle of double
| Square of double
| Rectangle of double * double
let pi = 3.141592654

let area myShape =
    match myShape with
    | Circle radius -> pi * radius * radius
    | EquilateralTriangle s -> (sqrt 3.0) / 4.0 * s * s
    | Square s -> s * s
    | Rectangle (h, w) -> h * w
```
В Шарпах код юзается отлично:

```cs
using Microsoft.FSharp.Collections;

namespace FarshToCS
{
    class Program
    {
        static void Main(string[] args)
        {
            FarshLibary.Shape mySquare = FarshLibary.Shape.NewSquare(5);
            Console.WriteLine($"Is this a square: {mySquare.IsSquare}");
            Console.WriteLine(FarshLibary.area(mySquare));
        }
    } 
}
```

С кайфом выводятся правильные результаты, очень приятно и позитивно, мне понравилось
Декомпиляцию я провел через SharpLab и увидел что-то страшное, но в общем и целом там 
получается огромная иерархия объектов и их методов, сюда вставлять я 950 строк кода не буду, 
но в репу положу

### Scala в Java

Scala - порождение сатаны и мне было очень грустно в ней разбираться, поэтому я использовал 
только простейший пример с пайпом

```scala

  import scala.util.chaining._
  import scala.language.implicitConversions
  class ScalaLiba {

    def plus1(i: Int) = i + 1
    def double(i: Int) = i * 2
    def square(i: Int) = i * i
    def pipeSample(i: Int) = i.pipe(plus1).pipe(double).pipe(square)

    def main(args: Array[String]) = {
      // println(pipeSample(1))
    }
  }
  ```
  Но плюс в том, что мне не пришлось разность всё в разные проекты, поскольку в отличие 
  от .NETа тут всё компилируется не по проектам а по файлам, поэтому прямо в том же проекте 
  я создал Java-вский файл и заюзал функцию с пайпами

  ```java
  public class JavaClass {
    public static void main(String[] args) {
        var ass = new ScalaLiba();
        System.out.println(ass.pipeSample(3));
    }
}
```

Хотя бы код вызывается приятно и без проблем :)
Дальше при помощи Java Decompiler Online (а в нём некий Procyon)

```java
import java.lang.invoke.SerializedLambda;
import scala.Function1;
import scala.runtime.BoxesRunTime;
import scala.util.package;
import scala.util.ChainingOps$;
import scala.reflect.ScalaSignature;

// 
// Decompiled by Procyon v0.5.36
// 

@ScalaSignature(bytes = "\u0006\u0005m2Aa\u0002\u0005\u0001\u0017!)!\u0003\u0001C\u0001'!)a\u0003\u0001C\u0001/!)Q\u0004\u0001C\u0001=!)\u0001\u0005\u0001C\u0001C!)1\u0005\u0001C\u0001I!)a\u0005\u0001C\u0001O\tI1kY1mC2K'-\u0019\u0006\u0002\u0013\u00059A(Z7qift4\u0001A\n\u0003\u00011\u0001\"!\u0004\t\u000e\u00039Q\u0011aD\u0001\u0006g\u000e\fG.Y\u0005\u0003#9\u0011a!\u00118z%\u00164\u0017A\u0002\u001fj]&$h\bF\u0001\u0015!\t)\u0002!D\u0001\t\u0003\u0015\u0001H.^:2)\tA2\u0004\u0005\u0002\u000e3%\u0011!D\u0004\u0002\u0004\u0013:$\b\"\u0002\u000f\u0003\u0001\u0004A\u0012!A5\u0002\r\u0011|WO\u00197f)\tAr\u0004C\u0003\u001d\u0007\u0001\u0007\u0001$\u0001\u0004tcV\f'/\u001a\u000b\u00031\tBQ\u0001\b\u0003A\u0002a\t!\u0002]5qKN\u000bW\u000e\u001d7f)\tAR\u0005C\u0003\u001d\u000b\u0001\u0007\u0001$\u0001\u0003nC&tGC\u0001\u0015,!\ti\u0011&\u0003\u0002+\u001d\t!QK\\5u\u0011\u0015ac\u00011\u0001.\u0003\u0011\t'oZ:\u0011\u00075q\u0003'\u0003\u00020\u001d\t)\u0011I\u001d:bsB\u0011\u0011\u0007\u000f\b\u0003eY\u0002\"a\r\b\u000e\u0003QR!!\u000e\u0006\u0002\rq\u0012xn\u001c;?\u0013\t9d\"\u0001\u0004Qe\u0016$WMZ\u0005\u0003si\u0012aa\u0015;sS:<'BA\u001c\u000f\u0001")
public class ScalaLiba
{
    public int plus1(final int i) {
        return i + 1;
    }
    
    public int double(final int i) {
        return i * 2;
    }
    
    public int square(final int i) {
        return i * i;
    }
    
    public int pipeSample(final int i) {
        return BoxesRunTime.unboxToInt(ChainingOps$.MODULE$.pipe$extension(package.chaining$.MODULE$.scalaUtilChainingOps(ChainingOps$.MODULE$.pipe$extension(package.chaining$.MODULE$.scalaUtilChainingOps(ChainingOps$.MODULE$.pipe$extension(package.chaining$.MODULE$.scalaUtilChainingOps((Object)BoxesRunTime.boxToInteger(i)), (Function1)(i -> $this.plus1(i)))), (Function1)(i -> $this.double(i)))), (Function1)(i -> $this.square(i))));
    }
    
    public void main(final String[] args) {
    }
}
```

Опять некрасиво и без кайфа, ну хотя бы не 1000 строк, а просто чето страшное, 
пайп навернул каких-то модулей и экстеншенов, по ощущениям - просто заюзал тот же пайп из 
этих сторонних модулей

## Таска 3, запилить и заюзать библиотеки

### C# и F#

жестко (((написал))) целый класс графа и алгоритмов обхода

```cs
namespace Sharp;

// This class represents a directed
// graph using adjacency list
// representation
public class Graph
{
// No. of vertices    
    private int _V;

//Adjacency Lists
    LinkedList<int>[] _adj;

    public Graph(int V)
    {
        _adj = new LinkedList<int>[V];
        for (int i = 0; i < _adj.Length; i++)
        {
            _adj[i] = new LinkedList<int>();
        }

        _V = V;
    }

// Function to add an edge into the graph
    public void AddEdge(int v, int w)
    {
        _adj[v].AddLast(w);
    }

// Prints BFS traversal from a given source s
    public void BFS(int s)
    {
        // Mark all the vertices as not
        // visited(By default set as false)
        bool[] visited = new bool[_V];
        for (int i = 0; i < _V; i++)
            visited[i] = false;

        // Create a queue for BFS
        LinkedList<int> queue = new LinkedList<int>();

        // Mark the current node as
        // visited and enqueue it
        visited[s] = true;
        queue.AddLast(s);

        while (queue.Any())
        {
            // Dequeue a vertex from queue
            // and print it
            s = queue.First();
            Console.Write(s + " ");
            queue.RemoveFirst();

            // Get all adjacent vertices of the
            // dequeued vertex s. If a adjacent
            // has not been visited, then mark it
            // visited and enqueue it
            LinkedList<int> list = _adj[s];

            foreach (var val in list)
            {
                if (!visited[val])
                {
                    visited[val] = true;
                    queue.AddLast(val);
                }
            }
        }
    }

// A function used by DFS
    public void DFSUtil(int v, bool[] visited)
    {
        // Mark the current node as visited
        // and print it
        visited[v] = true;
        Console.Write(v + " ");

        // Recur for all the vertices
        // adjacent to this vertex
        LinkedList<int> vList = _adj[v];
        foreach (var n in vList)
        {
            if (!visited[n])
                DFSUtil(n, visited);
        }
    }

// The function to do DFS traversal.
// It uses recursive DFSUtil()
    public void DFS(int v)
    {
        // Mark all the vertices as not visited
        // (set as false by default in c#)
        bool[] visited = new bool[_V];

        // Call the recursive helper function
        // to print DFS traversal
        DFSUtil(v, visited);
    }
}

// This code is contributed by anv89 <-точно-точно я
```

После этого я сбилдил проект и запаковал его в nupkg через встроенные функции райдера. 
Публиковать на Nupkg.org я не стал дабы не захламлять сайт говнокодом.
После этого я сделал новый проект, в нем в сурсах пакетов указал путь до папки с пакетов
и подгрузил его, а затем с кайфом использовал функции в фарше, они даже правильно работали

```fs
let g = Sharp.Graph(4)
g.AddEdge(0, 1);
g.AddEdge(0, 2);
g.AddEdge(1, 2);
g.AddEdge(2, 0);
g.AddEdge(2, 3);
g.AddEdge(3, 3)
g.BFS(2)
g.DFS(2)
```

### Java

Работать со скалой ещё раз я побоялся, поэтому юзал джаву в джаве. Сначала опять ///Жестко
написал/// целый дфс бфс

```java
package graph;

import java.io.*;
import java.util.*;

    // This class represents a directed graph using adjacency list
// representation
    public class Graph
    {
        private int V;   // No. of vertices
        private LinkedList<Integer> adj[]; //Adjacency Lists

        // Constructor
        public Graph(int v)
        {
            V = v;
            adj = new LinkedList[v];
            for (int i=0; i<v; ++i)
                adj[i] = new LinkedList();
        }

        // Function to add an edge into the graph
        public void addEdge(int v,int w)
        {
            adj[v].add(w);
        }

        // prints BFS traversal from a given source s
        public void BFS(int s)
        {
            // Mark all the vertices as not visited(By default
            // set as false)
            boolean visited[] = new boolean[V];

            // Create a queue for BFS
            LinkedList<Integer> queue = new LinkedList<Integer>();

            // Mark the current node as visited and enqueue it
            visited[s]=true;
            queue.add(s);

            while (queue.size() != 0)
            {
                // Dequeue a vertex from queue and print it
                s = queue.poll();
                System.out.print(s+" ");

                // Get all adjacent vertices of the dequeued vertex s
                // If a adjacent has not been visited, then mark it
                // visited and enqueue it
                Iterator<Integer> i = adj[s].listIterator();
                while (i.hasNext())
                {
                    int n = i.next();
                    if (!visited[n])
                    {
                        visited[n] = true;
                        queue.add(n);
                    }
                }
            }
        }
        public void DFSUtil(int v, boolean visited[])
        {
            // Mark the current node as visited and print it
            visited[v] = true;
            System.out.print(v + " ");

            // Recur for all the vertices adjacent to this
            // vertex
            Iterator<Integer> i = adj[v].listIterator();
            while (i.hasNext()) {
                int n = i.next();
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        // The function to do DFS traversal.
        // It uses recursive
        // DFSUtil()
        public void DFS(int v)
        {
            // Mark all the vertices as
            // not visited(set as
            // false by default in java)
            boolean visited[] = new boolean[V];

            // Call the recursive helper
            // function to print DFS
            // traversal
            DFSUtil(v, visited);
        }

    }
// This code is contributed by Aakash Hasija <- я, по паспорту
```

Пакет я делал при помощи Мавена. Внезапно это было довольно приятно, потому что когда я
ошибался в нюгетовском пакете, мне приходилось его удалять, открывать заново райдер, 
три раза хлопать в ладоши, смотреть под подушку и тд.
Здесь же можно просто сначала собрать пакет командой maven package, указать его в новом проекте
в модулях как библиотеку, и затем при любом изменении делать maven install.
Сегодня Java победила, 1:100 в пользу .NETа

```java
package main.java.com.company;

import graph.Graph;

public class Main {

    public static void main(String args[])
    {
        Graph g = new Graph(4);

        g.addEdge(0, 1);
        g.addEdge(0, 2);
        g.addEdge(1, 2);
        g.addEdge(2, 0);
        g.addEdge(2, 3);
        g.addEdge(3, 3);

        System.out.println("Following is Breadth First Traversal "+
                "(starting from vertex 2)");

        g.BFS(2);
        g.DFS(2);
    }
}
```

## Таска 4, Бенчмаркинг

### C#

Для C# я воспользовался библиотекой BenchMarkDotNet

```cs
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
```

Библиотекая классная и простая в использовании (единственное - нельзя указать единицы 
измерения, или я не справился с их нахождением, но в целом пофиг).
Результаты получились довольно медленные, но по крайней мере логичные,
 почему - я так и не понял, но предполагаю, что всему виной деревянный компьютер

![BenchC](https://user-images.githubusercontent.com/37649552/156646059-92c85b01-53c2-401c-95e9-f6945d0944e9.jpg)

### Java

В джавее пришлось использовать грустный JMH, в котором нет нормально трекинга памяти и вообще
всё грустно, но время отслеживает и ладно.

```java
package Benchmarking;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.concurrent.TimeUnit;

import org.openjdk.jmh.annotations.*;
import org.openjdk.jmh.runner.Runner;
import org.openjdk.jmh.runner.RunnerException;
import org.openjdk.jmh.runner.options.Options;
import org.openjdk.jmh.runner.options.OptionsBuilder;

class MergeSort{
    public static void mergeSort(int[] a, int n) {
        if (n < 2) {
            return;
        }
        int mid = n / 2;
        int[] l = new int[mid];
        int[] r = new int[n - mid];

        for (int i = 0; i < mid; i++) {
            l[i] = a[i];
        }
        for (int i = mid; i < n; i++) {
            r[i - mid] = a[i];
        }
        mergeSort(l, mid);
        mergeSort(r, n - mid);

        merge(a, l, r, mid, n - mid);
    }
    public static void merge(
            int[] a, int[] l, int[] r, int left, int right) {

        int i = 0, j = 0, k = 0;
        while (i < left && j < right) {
            if (l[i] <= r[j]) {
                a[k++] = l[i++];
            }
            else {
                a[k++] = r[j++];
            }
        }
        while (i < left) {
            a[k++] = l[i++];
        }
        while (j < right) {
            a[k++] = r[j++];
        }
    }
}

@BenchmarkMode(Mode.AverageTime)
@OutputTimeUnit(TimeUnit.NANOSECONDS)
@Measurement(iterations = 3)
@Fork(1)
@Warmup(iterations = 1)
@State(Scope.Benchmark)
public class Bencher {

    @Param({"10", "100", "1000", "10000"})
    public int Length;
    int[] array;
    Random random;

    @Setup(Level.Invocation)
    public void init() {
    }

    @Benchmark
    public void SortArrayWithInBuiltSort() {
        random = new Random();
        array = new int[Length];
        for (int i = 0; i < Length; i++) {
            int randomNumber = random.nextInt();
            array[i] = randomNumber;
        }
        Arrays.sort(array);
    }

    @Benchmark
    public void SortArrayWithBubbleSort(){
        random = new Random();
        array = new int[Length];
        for (int i = 0; i < Length; i++) {
            int randomNumber = random.nextInt();
            array[i] = randomNumber;
        }
        for (int i = 0; i < Length; i++){
            for (int j = 0; j < Length - 1; j++){
                if (array[j] > array[j+1]){
                    var temp = array[j];
                    array[j] = array[j+1];
                    array[j+1] = temp;
                }
            }
        }
    }

    @Benchmark
    public void SortArrayWithMergeSort() {
        random = new Random();
        array = new int[Length];
        for (int i = 0; i < Length; i++) {
            int randomNumber = random.nextInt();
            array[i] = randomNumber;
        }
        MergeSort.mergeSort(array, array.length);
    }



    public static void main(String[] args) throws RunnerException {

        Options options = new OptionsBuilder()
                .include(Bencher.class.getSimpleName()).threads(1)
                .forks(1).shouldFailOnError(true).shouldDoGC(true)
                .jvmArgs("-server").build();
        new Runner(options).run();

    }
}
```

Результаты получились логичные, местами быстрее, местами медленнее, глобально медленно,
 с пивом пойдёт

![9b28da22-f5b9-4211-b3d7-f4f078ec23bf](https://user-images.githubusercontent.com/37649552/156646140-adc2c1aa-ba4d-4d0e-bb81-1d04ceca26ce.jpg)


## Таска 5, dotTrace и dotMemory

Я делал замеры на примере 5000 созданий и удалений рестор поинта, с файловой системой и без.

### Без ФС:

![Screenshot_No](https://user-images.githubusercontent.com/37649552/156646198-b8afdcd4-1e68-4216-ad1c-681a2b08bd37.png)

![Screenshot_1](https://user-images.githubusercontent.com/37649552/156646182-fc4fa30a-1231-4273-b09c-c8484577958a.png)


Ничего интересного не происходит, память выделяется только потому что я создаю и сэтаю 
бэкап джобу вне цикла, разок срабатывает сборщик мусора, всё быстро заканчивается.

### С ФС:

![Screenshot_2](https://user-images.githubusercontent.com/37649552/156646210-f7797ec1-7b43-4777-b405-431918815b37.png)
![Screenshot_Files](https://user-images.githubusercontent.com/37649552/156646225-da0edffd-2222-41b8-b838-7b3605251dd1.png)


Тут мем в том, что постепенно выдяляется память, которая тратится ещё и на файлы, поэтому GC
срабатывает множество раз, освобождая ненужные объекты. Часть памяти он очистить всё равно
не может - значит где-то что-то точно можно и нужно  оптимизировать, чтобы у реального приложения
не отвалилась жопа.
