using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circular_Queue
{
    public class Node<T>
    {
        private T value;
        private Node<T> next;

        public Node(T value)
        {
            this.value = value;
        }

        public Node<T> Next { get => next; set => next = value; }
        public T Value { get => value; set => this.value = value; }
    }
}
