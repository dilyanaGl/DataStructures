namespace Hierarchy.Core
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node<T> root;

        private Dictionary<T, Node<T>> nodeByValue;

        private int count;

        public Hierarchy(T root)
        {
            this.root = new Node<T>(root);
            this.count = 1;
            this.nodeByValue = new Dictionary<T, Node<T>>();
            this.nodeByValue[root] = this.root;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public void Add(T element, T child)
        {
            if (!this.nodeByValue.ContainsKey(element) || this.nodeByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            var node = this.nodeByValue[element];
            var childNode = new Node<T>(child);
            this.nodeByValue[child] = childNode;

            node.Children.Add(childNode);
            childNode.Parent = node;
            this.count++;
        }

        public void Remove(T element)
        {
            if (this.count == 0)
            {
                throw new InvalidOperationException();
            }

            if (element.Equals(root.Value))
            {
                throw new InvalidOperationException();
            }

            var elementToRemove = this.FindElement(element);

            if (elementToRemove == null)
            {
                throw new ArgumentException();
            }

            var parent = elementToRemove.Parent;
            this.nodeByValue.Remove(element);
            parent.Children.Remove(elementToRemove);
            var children = elementToRemove.Children;
            parent.Children.AddRange(children);

            foreach (var item in children)
            {               
                item.Parent = parent;
            }

            this.count--;
        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.nodeByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            var node = this.nodeByValue[item];
            foreach (var child in node.Children)
            {
                yield return child.Value;
            }
                    

        }

        private Node<T> FindElement(T element)
        {
            if (!this.nodeByValue.ContainsKey(element))
            {
                return null;
            }

            return this.nodeByValue[element];
        }

        public T GetParent(T item)
        {
            if (this.root.Value.Equals(item))
            {
                return default(T);
            }

            if (!this.nodeByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }
            return this.nodeByValue[item].Parent.Value;

        }

        public bool Contains(T value)
        {
            return this.nodeByValue.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var set = new HashSet<T>();
                      
            foreach (var item in other.nodeByValue.Select(p => p.Key).ToArray())
            {
                if (this.nodeByValue.ContainsKey(item))
                {
                    set.Add(item);
                }
            }

            return set.Reverse();
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Hierarchy<T>>();

            var result = TraverseTree(this.root);

            foreach (var item in result)
            {
                yield return item;
            }
        }

        private IEnumerable<T> TraverseTree(Node<T> node)
        {
            var currentQueue = new Queue<Node<T>>();
            var resultQueue = new HashSet<T>();

            currentQueue.Enqueue(node);

            while (currentQueue.Count > 0)
            {
                var currentNode = currentQueue.Dequeue();

                foreach (var child in currentNode.Children)
                {
                    currentQueue.Enqueue(child);
                }

                resultQueue.Add(currentNode.Value);

            }

            return resultQueue;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }


}
