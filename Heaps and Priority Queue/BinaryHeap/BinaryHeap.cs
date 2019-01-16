using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;
    private int count;

    public BinaryHeap()
    {
        heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        this.heap.Add(item);
        this.HeapifyUp();

    }

    private void HeapifyUp()
    {
        T item = this.heap[this.Count - 1];
        int index = this.Count - 1;

        while (true)
        {
            if (index == 0)
            {
                break;
            }
            int parentIndex = (index - 1) / 2;


            if (this.heap[parentIndex].CompareTo(this.heap[index]) >= 0)
            {
                break;
            }

            this.heap[index] = this.heap[parentIndex];
            this.heap[parentIndex] = item;
            index = parentIndex;
        }

    }

    private void HeapifyDown(int index)
    {
        while (index < this.Count / 2)
        {
            var leftChildIndex = this.FindLeftChild(index);

            if(!HasChild(leftChildIndex))
            {
                break;
            }

            // check if we have a right child and the right child is bigger than the left child
            if (HasChild(leftChildIndex + 1) && IsLess(leftChildIndex, leftChildIndex + 1))
            {
                // we switch the element with the right child
                leftChildIndex += 1;
            }


            if(IsLess(index, leftChildIndex))
            {
                break;
            }

            this.Swap(index, leftChildIndex);
            index = leftChildIndex;
        }
    }

    private void Swap(int index, int leftChildIndex)
    {
        var temp = this.heap[index];
        this.heap[index] = this.heap[leftChildIndex];
        this.heap[leftChildIndex] = this.heap[index];
    }

    private bool IsLess(int leftChildIndex, int rightChildIndex)
    {
        return this.heap[leftChildIndex].CompareTo(this.heap[rightChildIndex]) > 0;
    }

    private int FindLeftChild(int index)
    {
        return 2 * index + 1;
    }

    private bool HasChild(int childIndex)
    {
        return childIndex < this.heap.Count;
    }

    public T Peek()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        T maxElement = this.heap[0];

        this.heap.RemoveAt(0);

        HeapifyDown(0);

        return maxElement;
    }
}
