using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private Node root;

    public int ElementsCount { get; set; }

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private BinarySearchTree(Node node)
    {
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
        this.ElementsCount++;
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
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

        this.ElementsCount--;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void Delete(T element)
    {
        this.Delete(this.root, element);

    }

    private void Delete(Node node, T element)
    {
        if (node == null)
        {
            return;
        }

        var current = node;
        Node parent = null;
        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                parent = current;
                current = current.Left;

            }
            else if (current.Value.CompareTo(element) < 0)
            {
                parent = current;
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        if(current == null)
        {
            return;
        }

        if (parent == null)
        {
            var rightChild = current.Right;

            if (rightChild.Left != null)
            {
                rightChild = rightChild.Left;
            }

            rightChild.Left = this.root.Left;

            this.root = rightChild;

        }
        else
        {
            var previous = current;

            if (current.Right == null)
            {
                current = current.Left;
            }
            else if (current.Left == null)
            {
                current = current.Right;
            }
            else if (current.Right != null && current.Left != null)
            {
                var currentRight = current.Right;

                Node leftChild = currentRight.Left;
                if (leftChild != null)
                {
                    leftChild.Left = current.Left;
                    current = leftChild;
                }

                else
                {
                    currentRight.Left = current.Left;
                    current = currentRight;
                }

            }

            if (previous.Value.CompareTo(parent.Value) < 0)
            {
                parent.Left = current;
            }
            else
            {
                parent.Right = current;
            }


        }


    }

    public void DeleteMax()
    {
        this.DeleteMax(this.root);
    }

    private void DeleteMax(Node root)
    {
        var current = root;
        Node parent = null;

        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        if (parent == null)
        {
            parent = this.root.Left;
        }
        else
        {
            parent.Right = current.Left;
        }

        this.ElementsCount--;
    }

    public int Count()
    {
        return this.Count(this.root);

    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return 1 + Count(node.Left) + Count(node.Right);

    }

    public int Rank(T element)
    {
        return this.Rank(this.root, element);
    }

    private int Rank(Node node, T element)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = node.Value.CompareTo(element);

        if (compare >= 0)
        {
            return this.Rank(node.Left, element);
        }

        if (compare < 0)
        {
            return 1 + this.Rank(node.Left, element) + this.Rank(node.Right, element);
        }

        return 1;
    }

    public T Select(int rank)
    {
        return this.Select(this.root, rank);
    }

    private T Select(Node node, int rank)
    {
        if (node == null)
        {
            return default(T);
        }

        var currentRank = this.Rank(node.Value);
        int comparer = currentRank.CompareTo(rank);

        if (comparer == 0)
        {
            return node.Value;
        }

        else if (comparer < 0)
        {
            return Select(node.Right, rank);
        }

        else
        {
            return Select(node.Left, rank);
        }


    }

    private bool IsLower(Node node, T value)
    {
        return node.Value.CompareTo(value) < 0;
    }

    private bool IsHigherOrEqual(Node node, T value)
    {
        return node.Value.CompareTo(value) >= 0;
    }

    private bool IsHigher(Node node, T value)
    {
        return node.Value.CompareTo(value) > 0;
    }

    public T Ceiling(T element)
    {
        var stack = new Stack<T>();
        this.Ceiling(this.root, element, ref stack);

        return stack.Count == 0 ? default(T) : stack.Pop();
    }

    private void Ceiling(Node node, T element, ref Stack<T> stack)
    {
        if (node == null)
        {
            return;
        }

        var comparer = node.Value.CompareTo(element);

        if (comparer <= 0)
        {
            Ceiling(node.Right, element, ref stack);
        }
        else
        {
            if (this.IsHigher(node, element))
            {
                stack.Push(node.Value);
                //return node.Value;
            }

            Ceiling(node.Left, element, ref stack);
        }
    }

    public T Floor(T element)
    {
        return this.Floor(this.root, element);
    }

    private T Floor(Node node, T element)
    {
        if (node == null)
        {
            return default(T);
        }

        var comparer = node.Value.CompareTo(element);

        //Node value is lower than element --> we need to check if the right node is lower
        if (comparer < 0)
        {
            if (node.Right == null)
            {
                return node.Value;
            }

            if (IsHigherOrEqual(node.Right, element))
            {
                if (node.Right.Left != null && IsLower(node.Right.Left, element))
                {
                    return node.Right.Left.Value;
                }
                else
                {
                    return node.Value;
                }
            }
            return this.Floor(node.Right, element);
        }
        else
        {
            if (node.Left == null && IsLower(node, element))
            {
                return node.Value;
            }
            return this.Floor(node.Left, element);
        }
        //else
        //{
        //    return this.Floor(node.Left, element);
        //}

    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);
        bst.Insert(42);

        //Console.WriteLine("Node for 11: {0}", bst.Floor(11));
        //Console.WriteLine("Node for 38: {0}", bst.Floor(38));
        //Console.WriteLine("Node for 40: {0}", bst.Floor(40));
        //Console.WriteLine("Node for 46: {0}", bst.Floor(46));
        //Console.WriteLine("Node for 10: {0}", bst.Ceiling(10));
        //Console.WriteLine("Node for 9: {0}", bst.Ceiling(9));
        //Console.WriteLine("Node for 8: {0}", bst.Ceiling(8));
        //Console.WriteLine("Node for 39: {0}", bst.Ceiling(39));
        //Console.WriteLine("Node for 3 : {0}", bst.Ceiling(3));
        //Console.WriteLine("Node for 4 : {0}", bst.Ceiling(4));
        //Console.WriteLine("Node for 1 : {0}", bst.Ceiling(1));
        //Console.WriteLine("Node for 45: {0}", bst.Ceiling(45));
        //Console.WriteLine("Node for 42: {0}", bst.Ceiling(42));
        //Console.WriteLine("Node for 16: {0}", bst.Ceiling(16));
        //Console.WriteLine("Node for 2: {0}", bst.Ceiling(2));
        //Console.WriteLine("Node for 36: {0}", bst.Ceiling(36));
        //Console.WriteLine("Node for 5: {0}", bst.Ceiling(5));
        //Console.WriteLine("Node for 0: {0}", bst.Ceiling(0));
        //Console.WriteLine("Node for int max: {0}", bst.Ceiling(int.MaxValue));
        //Console.WriteLine();
        //Console.WriteLine(bst.Count());
        //Console.WriteLine();
        //bst.DeleteMax();

        bst.Delete(11);
        bst.EachInOrder(Console.WriteLine);



    }
}