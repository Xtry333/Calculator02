using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            Console.ForegroundColor = ConsoleColor.Red;

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
            Console.ReadKey();
            Console.Clear();
            //while (!quit)
            {
                Console.Write("Input <empty to exit>: ");
                //input = Console.ReadLine();
                input = "3+4*2/(10-5)^2";

                if (input.Trim(' ') == "") quit = true;
                Console.WriteLine(input);

                // captures numbers. Anything like 11 or 22.34 is captured
                input = Regex.Replace(input, @"(?<number>\d+(\.\d+)?)", " ${number} ");
                // captures these symbols: + - * / ^ ( )
                input = Regex.Replace(input, @"(?<ops>[+\-*/^()])", " ${ops} ");
                // trims up consecutive spaces and replace it with just one space
                input = Regex.Replace(input, @"\s+", " ").Trim();
                Console.WriteLine(input);
                
                string[] regInput = input.Split(" ".ToCharArray());

                CustomList stack = new CustomList();
                CustomList output = new CustomList();
                
                foreach (string s in regInput)
                {
                    int isInt = 0;
                    if (int.TryParse(s, out isInt))
                    {
                        output.Add(s);
                    } else {
                        stack.Add(s);
                    }
                }
                printList(output);
                printList(stack);
            }
            Console.ReadKey();
            Console.WriteLine("Exiting...");
        }
    }
}
