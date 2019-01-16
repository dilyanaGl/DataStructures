﻿using System;

public class HeapExample
{
    static void Main()
    {
        //Console.WriteLine("Created an empty heap.");
        //var heap = new BinaryHeap<int>();
        //heap.Insert(5);
        //heap.Insert(8);
        //heap.Insert(1);
        //heap.Insert(3);
        //heap.Insert(12);
        //heap.Insert(-4);

        var arr = new int[] { 5, 8, 1, 3, 12, -4, -8 };
        Heap<int>.Sort(arr);
        Console.WriteLine(String.Join(" ", arr));
        
        //Console.WriteLine("Heap elements (max to min):");
        //while (heap.Count > 0)
        //{
        //    var max = heap.Pull();
        //    Console.WriteLine(max);
        //}
    }
}
