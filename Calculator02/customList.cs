using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator02
{
    public class Node
    {
        public int value;
        public Node nextNode;

        public Node(int v)
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
        //public Node lastNode = null;

        public void dodajNaPoczatek(Node node)
        {
            node.nextNode = head;
            tail = head;
            head = node;
        }

        public void AddFirst(int v)
        {
            Node node = new Node(v);
            node.nextNode = head;
            tail = head;
            head = node;
        }

        public void Add(int v)
        {
            Node node = new Node(v);
            Node lastNode = head;
            Node prevNode = null;
            //node.nextNode = null;
            //Console.WriteLine("Head: " + head);
            while(lastNode != null)
            {
                prevNode = lastNode;
                lastNode = lastNode.nextNode;
            }
            //Console.WriteLine("LastNode: " + prevNode);
            prevNode.nextNode = node;
            //Console.WriteLine("Head: " + head);
            //tail = head;
            //head = node;
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

        public int this[int index]
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
                return 0;
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
