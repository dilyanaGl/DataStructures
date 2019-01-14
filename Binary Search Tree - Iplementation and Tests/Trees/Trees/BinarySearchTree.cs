using System;
using System.Collections.Generic;
using Trees;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node<T> root;

    public BinarySearchTree()
    {

    }

    public BinarySearchTree(Node<T> node)
    {
        this.CopyTree(node);
    }

    private void CopyTree(Node<T> node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.CopyTree(node.Left);
        this.CopyTree(node.Right);


    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node<T>(value);
            return;
        }

        SetValue(this.root, value);


    }

    private void SetValue(Node<T> node, T value)
    {
        var comparisonValue = node.Value.CompareTo(value);
        if (comparisonValue > 0)
        {
            if (node.Left != null)
            {
                SetValue(node.Left, value);
            }
            else
            {
                node.Left = new Node<T>(value);
            }

        }
        else if (comparisonValue < 0)
        {
            if (node.Right != null)
            {
                SetValue(node.Right, value);
            }
            else
            {
                node.Right = new Node<T>(value);
            }
        }
        else
        {
            return;
        }


    }

    public bool Contains(T value)
    {
        return CheckNodeForValue(this.root, value);
    }

    public bool CheckNodeForValue(Node<T> node, T value)
    {
        var current = node;
        while (current != null)
        {
            var comparer = current.Value.CompareTo(value);
            if (comparer == 0)
            {
                return true;
            }

            if (comparer < 0)
            {
                CheckNodeForValue(current.Right, value);
            }

            if (comparer > 0)
            {
                CheckNodeForValue(current.Left, value);
            }

        }

        return false;

    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        var current = this.root;
        Node<T> parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;

        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {

            parent.Left = current.Right;

        }


    }

    public BinarySearchTree<T> Search(T item)
    {
        var binaryTree = Find(root, item);

        if (binaryTree == null)
        {
            return new BinarySearchTree<T>();
        }

        return binaryTree;
    }

    private BinarySearchTree<T> Find(Node<T> node, T item)
    {
        if (node == null || node.Value == null)
        {
            return null;
        }

        var comparisonValue = node.Value.CompareTo(item);
        if (comparisonValue == 0)
        {
            var binaryTree = new BinarySearchTree<T>();
            binaryTree.root = node;
            //SetValue(binaryTree.root.Left, node.Left.Value);
            //SetValue(binaryTree.root.Right, node.Right.Value);

            return binaryTree;
        }

        if (comparisonValue < 0)
        {
            return Find(node.Right, item);
        }

        else if (comparisonValue > 0)
        {
            return Find(node.Left, item);
        }

        return null;

    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        if (this.root == null)
        {
            return null;
        }

        var queue = new Queue<T>();


        TakeElementsInRange(this.root, startRange, endRange, ref queue);


        return queue;
    }

    private void TakeElementsInRange(Node<T> node, T startRange, T endRange, ref Queue<T> queue)
    {
        if (node == null)
        {
            return;
        }

        if (node.Left != null)
        {
            TakeElementsInRange(node.Left, startRange, endRange, ref queue);
        }

        if (IsElementInRange(node, startRange, endRange))
        {
            queue.Enqueue(node.Value);
        }

        if (node.Right != null)
        {
            TakeElementsInRange(node.Right, startRange, endRange, ref queue);
        }
    }

    private bool IsElementInRange(Node<T> node, T startValue, T endValue)
    {
        return IsElementHigherThanOrEqual(node, startValue)
            && IsElementLowerThanOrEqual(node, endValue);
    }


    private bool IsElementLowerThanOrEqual(Node<T> node, T value)
    {
        return node.Value.CompareTo(value) <= 0;
    }

    private bool IsElementHigherThanOrEqual(Node<T> node, T value)
    {
        return node.Value.CompareTo(value) >= 0;
    }



    public void EachInOrder(Action<T> action)
    {
        Traverse(root, action);
    }

    private void Traverse(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        if (node.Left != null)
        {
            Traverse(node.Left, action);
        }

        if (node.Value != null)
        {
            action.Invoke(node.Value);
        }

        if (node.Right != null)
        {
            Traverse(node.Right, action);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {

    }
}
