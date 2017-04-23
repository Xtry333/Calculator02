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
            bool infoDebug = false;

            Console.ForegroundColor = ConsoleColor.Red;

            //printList(aList);
            
            //for(int i = 1; i <= 20; i++)
            //{
            //    if (i <= 10)
            //        aList.Add(i.ToString());
            //    else
            //        aList.Push(i.ToString());
            //}

            //printList(aList);
            //aList[5] = "^";
            //printList(aList);
            //Console.WriteLine("Pop: " + aList.Pop());
            //Console.WriteLine("Peek: " + aList.Peek());
            //Console.WriteLine("Pop: " + aList.Pop());
            //aList[6] = "%";
            //printList(aList);
            ////aList.Reverse();
            //printList(aList);
            //aList.RemoveAt(0);
            //printList(aList);
            //aList.RemoveAt(2);
            //printList(aList);

            //for (int i = 0; i < 20; i++)
            //{
            //    if (i < aList.Count)
            //        aList[i] = i.ToString();
            //    else
            //        aList.Add(i.ToString());
            //}

            //printList(aList);
            //aList.Reverse();
            //printList(aList);
            //Console.ReadKey();
            //Console.Clear();

            //while (!quit)
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.Write("Input <empty to exit>: ");
                //input = Console.ReadLine();
                input = "3+4*2/(10-5)^2";
                //input = "5*4-3+2-1*3/8*7+3";
                //input = "5*(2-1)*2";

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

                CustomList operatorStack = new CustomList();
                CustomList outputQueue = new CustomList();

                string opstoken = "";
                foreach (string s in regInput)
                {
                    if (infoDebug)
                        Console.WriteLine("# regInput: " + s);
                    int isInt = 0;
                    if (int.TryParse(s, out isInt))
                    {
                        outputQueue.Add(s);
                        if (infoDebug)
                            Console.WriteLine("\tEnqueue number: " + s);
                    }
                    else
                    {
                        if (s == "+" || s == "-")
                        {
                            if (operatorStack.Count > 0)
                            {
                                opstoken = operatorStack.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (opstoken == "+" || opstoken == "-" || opstoken == "*" || opstoken == "/" || opstoken == "^")
                                {
                                    // pop o2 off the stack, onto the output queue;
                                    outputQueue.Add(operatorStack.Pop());
                                    if (operatorStack.Count > 0)
                                    {
                                        opstoken = operatorStack.Peek();
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #1: " + s);
                        }
                        if (s == "*" || s == "/")
                        {
                            if (operatorStack.Count > 0)
                            {
                                opstoken = operatorStack.Peek();
                                // while there is an operator, o2, at the top of the stack
                                while (opstoken == "+" || opstoken == "-" || opstoken == "*" || opstoken == "/" || opstoken == "^")
                                {
                                    if (opstoken == "+" || opstoken == "-")
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        // Once we're in here, the following algorithm condition is satisfied.
                                        // o1 is associative or left-associative and its precedence is less than (lower precedence) or equal to that of o2, or
                                        // o1 is right-associative and its precedence is less than (lower precedence) that of o2,

                                        // pop o2 off the stack, onto the output queue;
                                        outputQueue.Add(operatorStack.Pop());
                                        if (operatorStack.Count > 0)
                                        {
                                            opstoken = operatorStack.Peek();
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                            // push o1 onto the operator stack.
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #2: " + s);
                        }
                        if (s == "^" || s == "(")
                        {
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #3: " + s);
                        }
                        if (s == ")")
                        {
                            if (operatorStack.Count > 0)
                            {
                                opstoken = operatorStack.Peek();
                                // Until the token at the top of the stack is a left parenthesis
                                while (opstoken != "(")
                                {
                                    // pop operators off the stack onto the output queue
                                    outputQueue.Add(operatorStack.Pop());
                                    //Console.WriteLine("##Stack: " + operatorStack);
                                    //Console.WriteLine("##Queue: " + outputQueue);
                                    if (operatorStack.Count > 0)
                                    {
                                        opstoken = operatorStack.Peek();
                                    }
                                    else
                                    {
                                        // If the stack runs out without finding a left parenthesis,
                                        // then there are mismatched parentheses.
                                        throw new Exception("Unbalanced parenthesis!");
                                    }

                                }
                                // Pop the left parenthesis from the stack, but not onto the output queue.
                                operatorStack.Pop();
                            }
                        }
                    }
                    if (infoDebug)
                    {
                        Console.WriteLine("@Stack: " + operatorStack);
                        Console.WriteLine("Queue: " + outputQueue);
                    }
                    //Console.WriteLine("# - - - - -");
                }

                while (operatorStack.Count != 0)
                {
                    opstoken = operatorStack.Pop();
                    // If the operator token on the top of the stack is a parenthesis
                    if (opstoken == "(")
                    {
                        // then there are mismatched parenthesis.
                        throw new Exception("Unbalanced parenthesis!");
                    }
                    else
                    {
                        // Pop the operator onto the output queue.
                        outputQueue.Add(opstoken);
                    }
                    if (infoDebug)
                        Console.WriteLine("#Adding remaining operators: " + opstoken);
                }

                printList(outputQueue);
                printList(operatorStack);
            }
            Console.ReadKey();
            Console.WriteLine("Exiting...");
        }
    }
}
