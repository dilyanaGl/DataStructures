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

    public void Delete(T v)
    {
        if (!this.Contains(v))
        {
            return;
        }
        if (this.root.Value.CompareTo(v) == 0)
        {
            var leftChild = root.Left;
            var rightChild = root.Right;
            var parentOfLeftChild = this.FindParentOfMinNode(this.root.Right);
            Node<T> newRoot = null;

            if(parentOfLeftChild == null)
            {
                newRoot= this.root.Right;
            }
            else
            {
                newRoot = parentOfLeftChild.Left;
                parentOfLeftChild.Left = null;
                
            }
            this.root = newRoot;
            if (this.root != null)
            {
                this.root.Left = leftChild;
                this.root.Right = this.Balance(rightChild);
                UpdateHeight(this.root.Right);
            }

            this.UpdateHeight(this.root);
            
        }
        else
        {
            var parent = this.FindParent(this.root, v);
            if (parent == null)
            {
                return;
            }

            var cmp = parent.Value.CompareTo(v);
            if (cmp > 0)
            {
                var heightOfLeftChild = this.Height(parent.Left);
                if (heightOfLeftChild == 0)
                {
                    parent.Left = null;
                    parent = this.Balance(parent);
                    UpdateHeight(parent);
                    return;
                }
                else
                {
                    if (parent.Left.Right != null)
                    {
                        var leftChild = parent.Left.Left;
                        parent.Left = parent.Left.Right;
                        parent.Left.Left = leftChild;
                        
                        UpdateHeight(parent.Left);
                        UpdateHeight(parent);
                        parent = this.Balance(parent);
                    }
                    else
                    {
                        var leftChild = parent.Left.Left;
                        parent.Left = leftChild;
                        parent = this.Balance(parent);
                        UpdateHeight(parent);
                    }
                }

            }
            else if (cmp < 0)
            {
                var heightOfRightChild = Height(parent.Right);
                if (heightOfRightChild == 0)
                {
                    parent.Right = null;
                   parent = this.Balance(parent);
                    UpdateHeight(parent);
                    return;
                }
                else
                {
                    if (parent.Right.Left != null)
                    {
                        var leftChild = parent.Right.Left;
                        var lastRightChild = parent.Right.Right;
                        leftChild.Right = lastRightChild;
                        parent.Right = leftChild;
                       parent = this.Balance(parent);
                        UpdateHeight(parent.Right);
                        UpdateHeight(parent);
                    }
                    else
                    {
                        var rightChild = parent.Right.Right;
                        parent.Right = rightChild;
                        //  UpdateHeight(parent.Right);
                      parent = this.Balance(parent);
                        UpdateHeight(parent);
                    }
                }
            }
        }

       this.root = Balance(this.root);
        this.UpdateHeight(this.root);
       
    }

    private Node<T> FindParent(Node<T> node, T v)
    {
        if (node == null)
        {
            return null;
        }

        var comparer = node.Value.CompareTo(v);

        if (comparer > 0)
        {

            if (node.Left == null)
            {
                return null;
            }
            else if (node.Left.Value.CompareTo(v) == 0)
            {
                return node;
            }
            else
            {
                node = FindParent(node.Left, v);
            }
        }
        else if (comparer < 0)
        {
            if (node.Right == null)
            {
                return null;
            }
            else if (node.Right.Value.CompareTo(v) == 0)
            {
                return node;
            }
            else
            {
                node = FindParent(node.Right, v);
            }

        }

        return node;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }
        if (this.root.Left == null)
        {
            this.root = root.Right;
        }
        else
        {
            var parentOfMinNode = this.FindParentOfMinNode(this.root);
            parentOfMinNode.Left = null;
            UpdateHeight(parentOfMinNode);
        }

    }

    private Node<T> FindParentOfMinNode(Node<T> node)
    {
        if (node == null)
        {
            return null;
        }
        if (node.Left == null)
        {
            return null;
        }

        while (node.Left.Left != null)
        {
            node = node.Left;
        }

        return node;
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
        UpdateHeight(node);
        return node;
    }

    private Node<T> Balance(Node<T> node)
    {
        var balance = Height(node.Left) - Height(node.Right);
        if (balance > 1)
        {
            var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = RotateLeft(node.Left);
            }

            node = RotateRight(node);
        }
        else if (balance < -1)
        {
            var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = RotateRight(node.Right);
            }

            node = RotateLeft(node);
        }

        return node;
    }

    private void UpdateHeight(Node<T> node)
    {
        if(node == null)
        {
            return;
        }
        node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
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

    private int Height(Node<T> node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        var left = node.Left;
        node.Left = left.Right;
        left.Right = node;

        UpdateHeight(node);

        return left;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        var right = node.Right;
        node.Right = right.Left;
        right.Left = node;

        UpdateHeight(node);

        return right;
    }
}
