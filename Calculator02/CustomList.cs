using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomStringList
{
    public class Node
    {
        public string value;
        public Node nextNode;

        public Node(string v)
        {
            this.value = v;
            this.nextNode = null;
        }

        public int Count
        {
            get { return (nextNode == null ? 1 : 1 + nextNode.Count); }
        }

        public override string ToString()
        {
            if (nextNode == null)
                return value + "";
            return value + " " + nextNode.ToString();
        }
    }

    public class CustomList
    {
        public Node firstNode = null;
        public Node tail = null;

        public Node lastNode
        {
            get
            {
                Node last = firstNode;
                Node node = firstNode;
                while (node != null)
                {
                    last = node;
                    node = node.nextNode;
                }
                return last;
            }
        }

        public void Push(string v)
        {
            Node node = new Node(v);
            node.nextNode = firstNode;
            tail = firstNode;
            firstNode = node;
        }

        public string Pop()
        {
            string retVal = "NULL";
            if (firstNode != null)
            {
                retVal = firstNode.value;
                firstNode = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
            return retVal;
        }

        public string Peek()
        {
            return (firstNode == null ? "NULL" : firstNode.value);
        }

        public void Add(string v)
        {
            Node node = new Node(v);
            if (firstNode == null)
            {
                firstNode = node;
            } else {
                lastNode.nextNode = node;
            }
        }
        
        public int Count
        {
            get { return (firstNode == null ? 0 : firstNode.Count); }
        }

        public void Reverse()
        {
            Node node = firstNode, n = null;
            while (node != null)
            {
                Node tmp = node.nextNode;
                node.nextNode = n;
                n = node;
                node = tmp;
            }
            firstNode = n;
            tail = firstNode.nextNode;

        }

        public override string ToString()
        {
            if (firstNode == null) return "NULL";
            return firstNode.ToString();
        }

        public string this[int index]
        {
            get
            {
                if (index >= this.Count) throw new System.IndexOutOfRangeException();
                Node node = firstNode;
                while (index > 0)
                {
                    index--;
                    node = node.nextNode;
                }
                if (index == 0) return node.value;
                return "";
            }
            set
            {
                if (index >= this.Count) throw new System.IndexOutOfRangeException();
                Node node = firstNode;
                while (index > 0)
                {
                    index--;
                    node = node.nextNode;
                }
                if (index == 0) node.value = value;
            }
        }

        public void RemoveAt(int index)
        {
            if (index >= this.Count) throw new System.IndexOutOfRangeException();
            if (index == 0)
            {
                if (firstNode != null)
                {
                    firstNode = tail;
                    if (tail != null)
                        tail = tail.nextNode;
                }
            } else {
                index--;
                Node node = firstNode;
                while (index > 0)
                {
                    index--;
                    node = node.nextNode;
                }
                node.nextNode = node.nextNode.nextNode;
            }
            Console.WriteLine("Count: " + Count);
        }
        public void Clear()
        {
            firstNode = null;
            tail = null;
        }
    }
}