using System;
using System.Collections;
using System.Collections.Generic;
using LinkedList;

public class LinkedList<T> : IEnumerable<T>
{ /*shdkjsnd*/

    private Node<T> head;
    private Node<T> tail;

    
    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        // TODO
        var oldHead = this.head;
        this.head = new Node<T>(item);

        if (this.Count == 0)
        {
            this.tail = this.head;
        }

        this.head.Next = oldHead;

        this.Count++;
    }

    public void AddLast(T item)
    {
        // TODO

        var oldTail = this.tail;

        this.tail = new Node<T>(item);

        if (this.Count == 0)
        {
            this.head = this.tail;
        }
        else
        {
            oldTail.Next = this.tail;
        }

        this.Count++;

    }

    public T RemoveFirst()
    {
        // TODO: Throw exception if the list is empty

        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var oldHead = this.head.Value;
        this.head = this.head.Next;
        this.Count--;
        if(this.Count == 0)
        {
            this.head = null;
        }

        return oldHead;



    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var oldTail = this.tail;
        this.tail = null;

        this.Count--;

        if(this.Count == 0)
        {
            this.head = null;
        }
        else
        {
            var current = this.head;
            while(current.Next != oldTail)
            {
                current = current.Next;
            }

            this.tail = current;
            this.tail.Next = null;
        }

        return oldTail.Value;


    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = this.head;
        while (current != null)
        {
            yield return current.Value;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        // TODO
        return this.GetEnumerator();
    }
}
