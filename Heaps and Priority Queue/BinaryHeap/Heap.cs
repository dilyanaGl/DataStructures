using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        int n = arr.Length;

        for (int i = n / 2; i >= 0; i--)
        {
            HeapifyDown(arr, i, n);
        }

        for (int i = n - 1; i > 0; i--)
        {
            
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);
        }
    }

    private static void HeapifyDown(T[] arr, int currentIndex, int length)
    {
        while (currentIndex < length / 2)
        {
            var leftChild = GetChildIndex(currentIndex);
            if (!ChildExist(arr, leftChild))
            {
                break;
            }

            if (leftChild + 1 < length && IsLess(arr, leftChild, leftChild + 1))
            {
                leftChild += 1;
            }

            if (!IsLess(arr, currentIndex, leftChild))
            {
                break;
            }

            Swap(arr, currentIndex, leftChild);
            currentIndex = leftChild;
        }
    }


    private static void Swap(T[] arr, int currentIndex, int leftChild)
    {
        var temp = arr[currentIndex];
        arr[currentIndex] = arr[leftChild];
        arr[leftChild] = temp;
    }

    private static bool IsLess(T[] arr, int leftChild, int rightChild)
    {
        return arr[leftChild].CompareTo(arr[rightChild]) < 0;
    }

    private static bool ChildExist(T[] arr, int childIndex)
    {
        return arr.Length > childIndex;
    }

    private static int GetChildIndex(int currentIndex)
    {
        return 2 * currentIndex + 1;
    }
}
