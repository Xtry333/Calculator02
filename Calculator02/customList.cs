using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator02
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

        public int Length
        {
            get { return (nextNode == null ? 1 : 1 + nextNode.Length); }
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
        public Node head = null;
        public Node tail = null;
        
        public void AddFirst(string v)
        {
            Node node = new Node(v);
            node.nextNode = head;
            tail = head;
            head = node;
        }

        public void Add(string v)
        {
            Node node = new Node(v);
            if (head == null)
            {
                head = node;
            } else {
                lastNode.nextNode = node;
            }
        }

        public Node lastNode
        {           
            get
            {
                Node last = head;
                Node node = head;
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
            if (head != null)
            {
                head = tail;
                if (tail != null)
                    tail = tail.nextNode;
            }
        }

        public void deleteSecond()
        {
            if (head != null)
            {
                if (head.nextNode != null)
                    head.nextNode = head.nextNode.nextNode;
            }
        }

        public int Length
        {
            get { return (head == null ? 0 : head.Length); }
        }

        public override string ToString()
        {
            if (head == null) return "NULL";
            return head.ToString();
        }

        public string this[int index]
        {
            get
            {
                if (index >= this.Length) throw new Exception("Index out of bounds");
                tail = head;
                while (index > 0)
                {
                    index--;
                    tail = tail.nextNode;
                }
                if (index == 0) return tail.value;
                return "";
            }
            set
            {
                if (index >= this.Length) throw new Exception("Index out of bounds");
                tail = head;
                while (index > 0)
                {
                    index--;
                    tail = tail.nextNode;
                }
                if (index == 0) tail.value = value;
            }
        }

        public void RemoveAt(int index)
        {
            if (index == 0) usunGlowe();
            if (index >= this.Length) throw new Exception("Index out of bounds");
            index--;
            tail = head;
            while (index > 0)
            {
                index--;
                tail = tail.nextNode;
            }
            tail.nextNode = tail.nextNode.nextNode;
        }

    }
}
