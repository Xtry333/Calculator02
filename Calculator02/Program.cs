using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomStringList;

namespace Calculator02
{
    class Program
    {
        static void printList(CustomList l)
        {
            Console.WriteLine("#: " + l.Count + "; {" + l + "}");
        }
        static void Main()
        {
            CustomList aList = new CustomList();

            printList(aList);

            /*aList.Add("1");
            aList.Add("2");
            aList.Add("3");
            aList.Add("4");
            aList.Add("5");
            aList.Add("6");
            aList.Add("7");
            aList.Add("8");
            aList.Add("9");
            aList.Add("10");
            aList.Push("11");
            aList.Push("12");
            aList.Push("13");
            aList.Push("14");
            aList.Push("15");*/
            for(int i = 1; i <= 20; i++)
            {
                if (i <= 10)
                    aList.Add(i.ToString());
                else
                    aList.Push(i.ToString());
            }

            printList(aList);
            aList[5] = "^";
            printList(aList);
            Console.WriteLine("Pop: " + aList.Pop());
            Console.WriteLine("Peek: " + aList.Peek());
            Console.WriteLine("Pop: " + aList.Pop());
            aList[6] = "%";
            printList(aList);
            //aList.Reverse();
            printList(aList);
            aList.RemoveAt(2);
            //Console.WriteLine("li[8]: " + aList[8]);
            //aList[8] = 5;
            //printList(aList);
            //aList.RemoveAt(7);
            //Console.WriteLine("li[0]: " + aList[0]);
            //printList(aList);

            //lista.usunGlowe();

            for (int i = 0; i < 20; i++)
            {
                if (i < aList.Count)
                    aList[i] = i.ToString();
                else
                    aList.Add(i.ToString());
            }

            printList(aList);
            aList.Reverse();
            printList(aList);
            Console.ReadKey();
        }
    }
}
