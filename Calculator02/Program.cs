using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator02
{
    class Program
    {
        static void printList(CustomList l)
        {
            Console.WriteLine("#: " + l.Length + "; {" + l + "}");
        }
        static void Main()
        {
            CustomList aList = new CustomList();

            printList(aList);

            aList.Add("1");
            aList.Add("2");
            aList.Add("3");
            aList.Add("4");
            aList.Add("5");
            aList.Add("6");
            aList.Add("7");
            aList.Add("8");
            aList.Add("9");
            aList.Add("10");
            aList.AddFirst("11");
            aList.AddFirst("12");
            aList.AddFirst("13");
            aList.AddFirst("14");
            aList.AddFirst("15");


            //Console.WriteLine("li[8]: " + aList[8]);
            //aList[8] = 5;
            //printList(aList);
            //aList.RemoveAt(7);
            //Console.WriteLine("li[0]: " + aList[0]);
            //printList(aList);

            //lista.usunGlowe();
            printList(aList);
            Console.ReadKey();
        }
    }
}
