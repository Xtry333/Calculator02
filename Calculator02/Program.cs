using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator02
{
    class Program
    {
        static void Main()
        {
            CustomList aList = new CustomList();

            Console.WriteLine("#: {1}: {0}", aList, aList.Length);

            aList.dodajNaPoczatek(new Node(1));
            aList.dodajNaPoczatek(new Node(2));
            aList.dodajNaPoczatek(new Node(3));
            aList.dodajNaPoczatek(new Node(4));
            aList.AddFirst(8);
            aList.AddFirst(14);
            aList.AddFirst(55);
            aList.AddFirst(32);
            aList.AddFirst(15);

            aList.Add(69);
            aList.Add(70);
            aList.Add(71);
            aList.Add(72);

            Console.WriteLine("li[8]: " + aList[8]);
            aList[8] = 5;
            Console.WriteLine("#: {1}: {0}", aList, aList.Length);
            aList.RemoveAt(7);
            Console.WriteLine("li[0]: " + aList[0]);
            Console.WriteLine("#: {1}: {0}", aList, aList.Length);

            //lista.usunGlowe();
            Console.ReadKey();
        }
    }
}
