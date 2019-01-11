using System;
using Circular_Queue;

public class LinkedStack<T> 
{
    private const int DefaultCapacity = 4;

    private int startIndex;
    private int endIndex;
    private int capacity;
    private T[] elements;

    public int Count { get; private set; }

    public LinkedStack(int capacity = DefaultCapacity)
    {
        // TODO
        this.capacity = capacity;
        this.elements = new T[this.capacity];
        this.startIndex = 0;
        this.endIndex = 0;
        this.Count = 0;
    }

    public void Enqueue(T element)
    {
       if(this.Count == capacity)
        {
            this.Resize();
        }        
        this.elements[endIndex] = element;
        endIndex++;       
        this.Count++;

    }

    private void Resize()
    {
        // TODO
     
            this.capacity *= 2;
            var newElements = new T[capacity];
            Array.Copy(this.elements, newElements, this.Count);
            this.elements = newElements;
        
    }

    private void CopyAllElements(T[] newArray)
    {
        for (int i = 0; i < this.elements.Length; i++)
        {
            if(i >= newArray.Length)
            {
                break;
            }
            this.elements[i] = newArray[i];
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if(this.Count == 0 || endIndex == 0)
        {
            throw new InvalidOperationException();
        }      
        
        var element = this.elements[startIndex];
       
        
        this.MoveAllElementsForward();

        this.endIndex--;
        this.elements[endIndex] = default(T);
      
        this.Count--;

        if(this.Count < this.capacity / 2)
        {
            this.Shrink();
        }

        return element;
    }

    private void MoveAllElementsForward()
    {
        for (int i = 0; i < this.endIndex - 1; i++)
        {
            this.elements[i] = this.elements[i + 1];
        }
    }

    private void Shrink()
    {
        this.capacity /= 2;
        var newArr = new T[this.capacity];
        Array.Copy(this.elements, newArr, this.Count);
        this.elements = newArr;

    }

    public T[] ToArray()
    {
        var array = new T[this.Count];
        int index = 0;
        
        for (int i = this.startIndex; i < this.endIndex; i++)
        {
            if(i >= this.elements.Length)
            {
                i %= this.elements.Length;
            }
           
            array[index] = this.elements[i];
            index++;
        }

        return array;
    }
}


public class Example
{
    public static void Main()
    {

        LinkedStack<int> queue = new LinkedStack<int>();

        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        queue.Enqueue(4);
        queue.Enqueue(5);
        queue.Enqueue(6);

        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        int first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-7);
        queue.Enqueue(-8);
        queue.Enqueue(-9);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        queue.Enqueue(-10);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");

        first = queue.Dequeue();
        Console.WriteLine("First = {0}", first);
        Console.WriteLine("Count = {0}", queue.Count);
        Console.WriteLine(string.Join(", ", queue.ToArray()));
        Console.WriteLine("---------------------------");
    }
}
