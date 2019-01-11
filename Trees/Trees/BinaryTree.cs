using System;

public class BinaryTree<T>
{
    private T value;
    private BinaryTree<T> leftChild;
    private BinaryTree<T> rightChild;

    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.value = value;
        this.leftChild = leftChild;
        this.rightChild = rightChild;
    }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Print(indent);
    }

    private void Print(int indent)
    {
        Console.WriteLine(String.Concat(new string(' ', indent), this.value));

        if (this.leftChild != null)
        {
            this.leftChild.Print(indent + 1);
        }

        if (this.rightChild != null)
        {
            this.rightChild.Print(indent + 1);
        }

    }

    public void EachInOrder(Action<T> action)
    {
        if (this.leftChild != null)
        {
            this.leftChild.EachInOrder(action);
        }

        action.Invoke(this.value);

        if (this.rightChild != null)
        {
            this.rightChild.EachInOrder(action);
        }

    }



    public void EachPostOrder(Action<T> action)
    {
        if (this.leftChild != null)
        {
            this.leftChild.EachPostOrder(action);
        }        

        if (this.rightChild != null)
        {
            this.rightChild.EachPostOrder(action);
        }

        action.Invoke(this.value);
    }
}
