namespace SharpBenchmarking;
using System;

public class SortAlgorithms
{
    static void Merge(List<int> array, int lowIndex, int middleIndex, int highIndex)
    {
        var left = lowIndex;
        var right = middleIndex + 1;
        var tempArray = new int[highIndex - lowIndex + 1];
        var index = 0;

        while ((left <= middleIndex) && (right <= highIndex))
        {
            if (array[left] < array[right])
            {
                tempArray[index] = array[left];
                left++;
            }
            else
            {
                tempArray[index] = array[right];
                right++;
            }

            index++;
        }

        for (var i = left; i <= middleIndex; i++)
        {
            tempArray[index] = array[i];
            index++;
        }

        for (var i = right; i <= highIndex; i++)
        {
            tempArray[index] = array[i];
            index++;
        }

        for (var i = 0; i < tempArray.Length; i++)
        {
            array[lowIndex + i] = tempArray[i];
        }
    }
    
    static List<int> MergeSort(List<int> array, int lowIndex, int highIndex)
    {
        if (lowIndex < highIndex)
        {
            var middleIndex = (lowIndex + highIndex) / 2;
            MergeSort(array, lowIndex, middleIndex);
            MergeSort(array, middleIndex + 1, highIndex);
            Merge(array, lowIndex, middleIndex, highIndex);
        }

        return array;
    }

    public List<int> MergeSort(List<int> array)
    {
        return MergeSort(array, 0, array.Count - 1);
    }

    public List<int> BubbleSort(List<int> array)
    {
        for (int i = 0; i < array.Count; i++)
        {
            for (int j = 0; j < array.Count - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    (array[j], array[i]) = (array[i], array[j]);
                }
            }
        }

        return array;
    }
}
