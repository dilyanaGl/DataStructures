using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace First_Last_List
{
    public class Node<T> : IComparable<Node<T>> where T : IComparable<T>
    {
        public Node(T value, int index)
        {
            this.Value = value;
            this.InsertionIndex = index;

        }
        public T Value { get; set; }

        public int InsertionIndex { get; set; }


        public int CompareTo(Node<T> other)
        {
            var cmp = this.Value.CompareTo(other.Value);

            if(cmp == 0)
            {
                return this.InsertionIndex.CompareTo(other.InsertionIndex);
            }

            return cmp;
         
        }

       
    }
}
