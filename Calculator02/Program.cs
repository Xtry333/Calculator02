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
            string input = "";
            bool quit = false;
            bool infoDebug = true;

            Console.ForegroundColor = ConsoleColor.Red;
            
            //while (!quit)
            {
                Console.Clear();
                Console.WriteLine("----------------------------");
                Console.Write("Input <empty to exit>: ");
                //input = Console.ReadLine();

                //input = "3+4*2/(10-5)^2";
                //input = "5*4-3+2-1*3/8*7+3";
                //input = "5*(2-1)*2";
                input = "((10 + (20 * 30)) - (0 - 15))";

                if (input.Trim() == "") quit = true;
                Console.WriteLine(input);

                
                input = Regex.Replace(input, @"(?<number>\d+(\.\d+)?)", " ${number} "); // Numbers           
                input = Regex.Replace(input, @"(?<ops>[+\-*/^()])", " ${ops} "); // Tokens: + - * / ^ ( )
                input = Regex.Replace(input, @"\s+", " ").Trim(); // Trim spaces
                Console.WriteLine(input);
                
                string[] regInput = input.Split(" ".ToCharArray());

                CustomList operatorStack = new CustomList();
                CustomList outputQueue = new CustomList();

                string cToken = ""; // Current Token from operatorStack
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
                                cToken = operatorStack.Peek();
                                while (cToken == "+" || cToken == "-" || cToken == "*" || cToken == "/" || cToken == "^") // While there is an operator at the top
                                {                                    
                                    outputQueue.Add(operatorStack.Pop()); // pop it off the stack, onto the output queue
                                    if (operatorStack.Count > 0) // if there is more
                                    {
                                        cToken = operatorStack.Peek(); // go back to start
                                    } else {
                                        break;
                                    }
                                }
                            }
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #1 (" + s + ")");
                        }
                        if (s == "*" || s == "/")
                        {
                            if (operatorStack.Count > 0)
                            {
                                cToken = operatorStack.Peek();
                                while (cToken == "*" || cToken == "/" || cToken == "^") // While there is an operator at the top, that has higher priority than plus and minus
                                {
                                    outputQueue.Add(operatorStack.Pop()); // pop it off the stack, onto the output queue
                                    if (operatorStack.Count > 0) // if there is more
                                    {
                                        cToken = operatorStack.Peek(); // go back to start
                                    } else {
                                        break;
                                    }
                                }
                            }
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #2 (" + s + ")");
                        }
                        if (s == "^" || s == "(")
                        {
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #3 (" + s + ")");
                        }
                        if (s == ")")
                        {
                            if (infoDebug)
                                Console.WriteLine("\tEncountered right parenthesis!");
                            if (operatorStack.Count > 0)
                            {
                                cToken = operatorStack.Peek();                                
                                while (cToken != "(") // Until the token at the top of the stack is a left parenthesis
                                {                                    
                                    outputQueue.Add(operatorStack.Pop()); // pop operators off the stack onto the output queue
                                    if (infoDebug)
                                        Console.WriteLine("\tEnqueue operator: " + cToken);
                                    if (operatorStack.Count > 0)
                                    {
                                        cToken = operatorStack.Peek();
                                    } else {
                                        // If the stack runs out without finding a left parenthesis,
                                        // then there are mismatched parentheses.
                                        throw new Exception("Unbalanced parenthesis!");
                                    }
                                }
                                // Pop the left parenthesis from the stack
                                operatorStack.Pop();
                            }
                        }
                    }
                    if (infoDebug)
                    {
                        Console.WriteLine("@Stack: " + operatorStack);
                        Console.WriteLine("Output queue: " + outputQueue);
                    }
                    //Console.WriteLine("# - - - - -");
                }

                while (operatorStack.Count != 0)
                {
                    cToken = operatorStack.Pop();
                    // If the operator token on the top of the stack is a parenthesis
                    if (cToken == "(")
                    {
                        // then there are mismatched parenthesis.
                        throw new Exception("Unbalanced parenthesis!");
                    }
                    else
                    {
                        // Pop the operator onto the output queue.
                        outputQueue.Add(cToken);
                    }
                    if (infoDebug)
                        Console.WriteLine("#Adding remaining operators: " + cToken);
                }

                Console.WriteLine("Output RPN: ");
                printList(outputQueue);
            }
            Console.ReadKey();
            Console.WriteLine("Exiting...");
        }
    }
}
