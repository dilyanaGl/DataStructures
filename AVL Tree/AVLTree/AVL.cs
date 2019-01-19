using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }
        node = Balance(node);
      
        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        if(node == null)
        {
            return null;
        }

        var balance = this.GetBalance(node);
        if (balance < -1)
        {
            var childBalance = this.GetBalance(node.Right);
            if(childBalance >= 1)
            {
                node.Right = this.RotateRight(node.Right);
            }
            node = this.RotateLeft(node);
        }
        else if (balance > 1)
        {
            var childBalance = this.GetBalance(node.Left);
            if(childBalance <= -1)
            {
                node.Left = this.RotateLeft(node.Left);
            }
            node = this.RotateRight(node);
        }

        node.Height = this.Height(node);
        
        return node;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var leftNode = node.Left;
        node.Left = leftNode.Right;
        leftNode.Right = node;

        node.Height = Height(node);

        return leftNode;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var rightNode = node.Right;
        node.Right = rightNode.Left;
        rightNode.Left = node;

        node.Height = Height(node);
        return rightNode;
    }



    private int GetBalance(Node<T> node)
    {
        if(node == null)
        {
            return 0;
        }

        var leftHeight = Height(node.Left);
        var rightHeight = Height(node.Right);

        return leftHeight - rightHeight;
    }

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return 1 + Math.Max(Height(node.Left), Height(node.Right));
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
