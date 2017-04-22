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
            return value + ", " + nextNode.ToString();
        }
    }

    public class CustomList
    {
        public Node firstNode = null;
        public Node tail = null;
        
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

        public void usunGlowe()
        {
            if (firstNode != null)
            {
                firstNode = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
        }

        public int Count
        {
            get { return (firstNode == null ? 0 : firstNode.Count); }
        }

        public void Reverse()
        {
            //Console.WriteLine("Operation reverse BEGIN");
            CustomList copy = new CustomList();
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                //Console.WriteLine("Old: " + this);
                copy.Push(this.Pop());
                //Console.WriteLine("Copy: " + copy);
            }
            firstNode = copy.firstNode;
            //Console.WriteLine("Operation reverse END");
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
                if (index >= this.Count) throw new Exception("Index out of bounds");
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
                if (index >= this.Count) throw new Exception("Index out of bounds");
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
            if (index == 0)
            {
                if (firstNode != null)
                {
                    firstNode = tail;
                    if (tail != null)
                        tail = tail.nextNode;
                }
            }
            if (index >= this.Count) throw new Exception("Index out of bounds");
            index--;
            Node node = firstNode;
            while (index > 0)
            {
                index--;
                node = node.nextNode;
            }
            node.nextNode = node.nextNode.nextNode;
        }

    }
}