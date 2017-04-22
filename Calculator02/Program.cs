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
            string input = "";
            bool quit = false;

            printList(aList);
            
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
            aList.RemoveAt(0);
            printList(aList);
            aList.RemoveAt(2);
            printList(aList);

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
            while (!quit)
            {
                input = Console.ReadLine();
                if (input.Trim(' ') == "") quit = true;
                Console.WriteLine(input);
            }
            Console.WriteLine("Exiting...");
        }
    }
}
