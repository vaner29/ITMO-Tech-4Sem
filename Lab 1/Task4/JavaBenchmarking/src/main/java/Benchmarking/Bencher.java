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
