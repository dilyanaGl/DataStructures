using System;
using System.Collections.Generic;

public class Tree<T>
{

    private List<Tree<T>> children;
    private Tree<T> parent;
    private T value;

    public List<Tree<T>> Children { get => children; set => children = value; }
    public Tree<T> Parent { get => parent; set => parent = value; }
    public T Value { get => value; private set => this.value = value; }

    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = new List<Tree<T>>();
        foreach(var child in children)
        {
            this.Children.Add(child);
            child.Parent = this;
        }
    }

    public void Print(int indent = 0)
    {
        Console.WriteLine(String.Concat(new String(' ', indent), this.Value));
        foreach (var item in this.Children)
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
        action.Invoke(tree.Value);

        foreach (var child in tree.Children)
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
        foreach (var child in tree.Children)
        {
            this.DFS(child, result);
        }

        result.Add(tree.Value);
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

            foreach (var child in current.Children)
            {
                stack.Push(child);
            }

            result.Push(current.Value);
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

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }

            result.Add(current.Value);
        }

        return result;

    }
}
