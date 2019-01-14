using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public class Node<T> where T : IComparable<T>
    {
        public Node(T value)
        {
            this.Value = value;
        }
        public T Value { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }
    }
}
