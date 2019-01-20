using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;
using First_Last_List;



public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{


    private OrderedBag<LinkedListNode<T>> orderedByValue;
    private OrderedBag<LinkedListNode<T>> orderByDescending;

    private LinkedList<T> orderedByAddition;

    public FirstLastList()
    {
        this.orderedByAddition = new LinkedList<T>();
        this.orderedByValue = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.orderByDescending = new OrderedBag<LinkedListNode<T>>((x, y) => -x.Value.CompareTo(y.Value));
    }

    public int Count
    {
        get
        {
            return this.orderedByAddition.Count;
        }
    }

    public void Add(T element)
    {
      
        var node = this.orderedByAddition.AddLast(element);
        this.orderedByValue.Add(node);
        this.orderByDescending.Add(node);
            
    }

    public void Clear()
    {
        this.orderedByAddition.Clear();
        this.orderedByValue.Clear();
        this.orderByDescending.Clear();

    }

    public IEnumerable<T> First(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var node = this.orderedByAddition.First;
        while(count > 0)
        {
            yield return node.Value;
            node = node.Next;
            count--;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        var node = this.orderedByAddition.Last;
     while(count > 0)
        {
            yield return node.Value;
            node = node.Previous;
            count--;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }     
        foreach (var item in this.orderByDescending)
        {
            if(count == 0)
            {
                break;
            }
            yield return item.Value;
            count--;
        }

    }

    public IEnumerable<T> Min(int count)
    {
        if (this.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }

        foreach (var item in this.orderedByValue)
        {
            if(count == 0)
            {
                break;
            }

            yield return item.Value;
            count--;
        }
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);    

        var range = this.orderedByValue.Range(node, true, node, true);

        foreach (var item in range)
        {
            this.orderedByAddition.Remove(item);
        }        
       var elementCount = this.orderByDescending.RemoveAllCopies(node);
        this.orderedByValue.RemoveAllCopies(node);
        
        return elementCount;

    }

    //public void Sort()
    //{
    //    int n = orderedByValue.Count;

    //    for (int i = n / 2; i >= 0; i--)
    //    {
    //        HeapifyDown(i, n);
    //    }

    //    for (int i = n - 1; i > 0; i--)
    //    {

    //        Swap(0, i);
    //        HeapifyDown(0, i);
    //    }
    //}

    //private void HeapifyDown(int currentIndex, int length)
    //{
    //    while (currentIndex < length / 2)
    //    {
    //        var leftChild = GetChildIndex(currentIndex);
    //        if (!ChildExist(leftChild))
    //        {
    //            break;
    //        }

    //        if (leftChild + 1 < length && IsLess(leftChild, leftChild + 1))
    //        {
    //            leftChild += 1;
    //        }

    //        if (!IsLess(currentIndex, leftChild))
    //        {
    //            break;
    //        }

    //        Swap(currentIndex, leftChild);
    //        currentIndex = leftChild;
    //    }
    //}


    //private void Swap(int currentIndex, int leftChild)
    //{
    //    var temp = orderedByValue[currentIndex];
    //    orderedByValue[currentIndex] = orderedByValue[leftChild];
    //    orderedByValue[leftChild] = temp;
    //}

    //private bool IsLess(int leftChild, int rightChild)
    //{
    //    return orderedByValue[leftChild].CompareTo(orderedByValue[rightChild]) <= 0;
    //}

    //private bool ChildExist(int childIndex)
    //{
    //    return orderedByValue.Count > childIndex;
    //}

    //private int GetChildIndex(int currentIndex)
    //{
    //    return 2 * currentIndex + 1;
    //}

    //private int FindLeftChild(int index)
    //{
    //    return 2 * index + 1;
    //}

    //private bool HasChild(int childIndex)
    //{
    //    return childIndex < this.orderedByValue.Count;
    //}
}
