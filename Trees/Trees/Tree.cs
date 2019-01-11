using System;
using System.Collections.Generic;

public class Tree<T>
{

    private List<Tree<T>> children;
    private T value;

    public Tree(T value, params Tree<T>[] children)
    {
        this.value = value;
        this.children = new List<Tree<T>>(children);
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine(String.Concat(new String(' ', indent), this.value));
        foreach (var item in this.children)
        {
            item.Print(indent + 1);
        }
    }


    public void Each(Action<T> action)
    {
        InvokeAction(action, this);
        
    }

    private void InvokeAction(Action<T> action, Tree<T> tree)
    {
        action.Invoke(tree.value);

        foreach (var child in tree.children)
        {
            InvokeAction(action, child);
        }

        
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();

        this.DFS(this, result);

        return result;

    }

    private void DFS(Tree<T> tree, List<T> result)
    {
        foreach (var child in tree.children)
        {
            this.DFS(child, result);
        }

        result.Add(tree.value);
    }

    public IEnumerable<T> DFSIteration()
    {
        var stack = new Stack<Tree<T>>();
        var result = new Stack<T>();

        stack.Push(this);


        while (stack.Count > 0)
        {
            var current = stack.Pop();
            //  result.Push(current.value);

            foreach (var child in current.children)
            {
                stack.Push(child);
            }

            result.Push(current.value);
        }



        return result.ToArray();

    }



    public IEnumerable<T> OrderBFS()
    {
        var result = this.BFS(this);

        return result;
    }


    private IEnumerable<T> BFS(Tree<T> tree)
    {
        var queue = new Queue<Tree<T>>();
        var result = new List<T>();

        queue.Enqueue(tree);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var child in current.children)
            {
                queue.Enqueue(child);
            }

            result.Add(current.value);
        }

        return result;

    }
}
