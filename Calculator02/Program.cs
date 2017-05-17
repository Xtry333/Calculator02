using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CustomStringList;

namespace Calculator02
{
    class Program
    {
        static int errCount = 0;
        static CustomList debugLog = new CustomList();
        static void RPNDebugLog(string message, int err = 0)
        {
            if (err >= 1)
            {
                debugLog.Add("<" + debugLog.Count + "> ERROR(" + err + "): " + message);
                errCount++;
            } else {
                debugLog.Add("<" + debugLog.Count + "> " + message);
            }
        }

        static void printList(CustomList l)
        {
            Console.WriteLine("#: " + l.Count + "; {" + l + "}");
        }

        static string RPNFormatString(string str)
        {
            int p = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '(') p++;
                if (str[i] == ')') p--;
            }
            if (p > 0) RPNDebugLog("RPNFormatString: Adding " + p + " right parenthesis.");
            if (p < 0) RPNDebugLog("RPNFormatString: Adding " + -p + " left parenthesis.");
            while (p > 0) { p--; str += ")"; }
            while (p < 0) { p++; str = "(" + str; }

            str = Regex.Replace(str, @"(?<=\d)(?=\()|(?<=\))(?=\d)|(?<=\))(?=\()", "*"); // Inserting multiplication between parenthesis w/o it
            RPNDebugLog(str);
            str = Regex.Replace(str, @"(?<number>\d+(\.\d+)?)", " ${number} "); // Encasing numbers in spaces
            RPNDebugLog(str);
            str = Regex.Replace(str, @"(?<token>[+\-*/^()])", " ${token} "); // Encasing operators in spaces
            RPNDebugLog(str);
            str = Regex.Replace(str, @"\s+", " ").Trim(); // Trim any additional spaces
            RPNDebugLog(str);
            str = Regex.Replace(str, @"((^-)|((?<=\( )-(?= [\d\(]))|((?<=[\-\(] )-(?= [\d\-\(]))|(?<=[/*] )-)", "~"); // Unary Minus
            RPNDebugLog(str);
            return str;
        }

        static void Main()
        {
            string input = "";
            bool quit = false;
            bool infoDebug = true;

            Console.ForegroundColor = ConsoleColor.Red;
            
            while (!quit)
            {
                Console.WriteLine("-------------------------------");
                if (infoDebug)
                    Console.Write("Input <debug>: ");
                else
                    Console.Write("Input <empty to exit>: ");
                input = Console.ReadLine();
                Console.Clear();

                //input = "3+4*2/(10-5)^2";
                //input = "5*4-3+2-1*3/8*7+3";
                //input = "5*(2-1)*2";
                //input = "((10 + (20 * 30)) - ( - 15))";
                //input = "-((-10+14*-14)/-10)-10";

                if (input.Trim() == "") quit = true;
                if (input.Trim() == "d") infoDebug = !infoDebug;
                Console.WriteLine("Raw input: " + input);

                


                //input = Regex.Replace(input, @"(?<=\d)(?=\()|(?<=\))(?=\d)|(?<=\))(?=\()", "*"); // Inserting multiplication
                //Console.WriteLine("RegExp step #1: " + input);
                //input = Regex.Replace(input, @"(?<number>\d+(\.\d+)?)", " ${number} "); // Encasing numbers
                //Console.WriteLine("RegExp step #2: " + input);
                //input = Regex.Replace(input, @"(?<ops>[+\-*/^()])", " ${ops} "); // Encasing tokens: + - * / ^ ( )
                //Console.WriteLine("RegExp step #3: " + input);
                //input = Regex.Replace(input, @"\s+", " ").Trim(); // Trim spaces
                //Console.WriteLine("RegExp step #4: " + input);
                //input = Regex.Replace(input, @"(- -)", "+"); // Minus and minus equals plus
                //Console.WriteLine("RegExp step #5: " + input);
                //input = Regex.Replace(input, @"((^-)|((?<=\( )-(?= [\d\(]))|((?<=[\-\(] )-(?= [\d\-\(]))|(?<=[/*] )-)", "~"); // Unary Minus
                //Console.WriteLine("RegExp step #5: " + input);
                //input = Regex.Replace(input, @"((?<=[+\-*/^()] )- (?=(\d*\.)?\d+))|(-(?= \())|(^\- )", "-");
                //Console.WriteLine("RegExp step #5: " + input);
                //input = Regex.Replace(input, @"(?<=\d )- (?=\d)", "+ -");
                //Console.WriteLine("RegExp step #6: " + input);
                //input = Regex.Replace(input, @"(- \()", "-1 * (");
                //Console.WriteLine("RegExp step #7: " + input);
                //input = Regex.Replace(input, @"(-(?=[(e)(pi)(sin)]))", "-1 * ");
                //Console.WriteLine("RegExp step #8: " + input);
                //Console.WriteLine("RegExp step #4: " + input);
                //input = Regex.Replace(input, @"(?<var>(- \())", " ( 0 ${var} ) ");
                //Console.WriteLine("RegExp step #5: " + input);

                // Step 1.
                //input = Regex.Replace(input, "-", "MINUS");
                //Console.WriteLine("RegExp step #4: " + input);
                // Step 2. Looking for pi or e or generic number \d+(\.\d+)?
                //input = Regex.Replace(input, @"(?<number>(pi|e|(\d+(\.\d+)?)))\s+MINUS", "${number} -");
                //Console.WriteLine("RegExp step #5: " + input);
                // Step 3. Use the tilde ~ as the unary minus operator
                //input = Regex.Replace(input, "MINUS", "~");
                //Console.WriteLine("RegExp step #6: " + input);

                input = RPNFormatString(input);

                Console.WriteLine("Formated input: " + input);

                string[] regInput = input.Split(" ".ToCharArray());

                CustomList operatorStack = new CustomList();
                CustomList outputQueue = new CustomList();

                string cToken = ""; // Current Token from operatorStack
                foreach (string s in regInput)
                {
                    if (infoDebug)
                        Console.WriteLine("# regInput: " + s);
                    double dNumber = 0;
                    if (double.TryParse(s.Replace('.', ','), out dNumber))
                    {
                        outputQueue.Add(dNumber.ToString());
                        if (infoDebug)
                            Console.WriteLine("\tEnqueue number: " + dNumber);
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
                        if (s == "^" || s == "~" || s == "(")
                        {
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #3 (" + s + ")");
                        }
                        if (s == "pi" || s == "e")
                        {
                            outputQueue.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #4(" + s + ")");
                        }
                        if (s == "sin" || s == "cos" || s == "tan")
                        {
                            operatorStack.Push(s);
                            if (infoDebug)
                                Console.WriteLine("\tPushed #5(" + s + ")");
                        }
                        if (s == ")")
                        {
                            if (operatorStack.Count > 0)
                            {
                                cToken = operatorStack.Peek();                                
                                while (cToken != "(") // Until the token at the top of the stack is a left parenthesis
                                {                                    
                                    outputQueue.Add(operatorStack.Pop()); // pop operators off the stack onto the output queue
                                    if (infoDebug)
                                        Console.WriteLine("\tPoped operator: " + cToken);
                                    if (operatorStack.Count > 0)
                                    {
                                        cToken = operatorStack.Peek();
                                    } else {
                                        // If the stack runs out without finding a left parenthesis,
                                        // then there are mismatched parentheses.
                                        RPNDebugLog("Unbalanced parenthesis.", 1);
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
                        RPNDebugLog("Unbalanced parenthesis.", 1);
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
                Console.WriteLine(" ");

                //Console.ReadKey();

                CustomList result = new CustomList();
                double val1 = 0.0, val2 = 0.0;

                if (outputQueue.Count == 0)
                {
                    RPNDebugLog("Output Queue is empty.", 1);
                }

                for (int i = 0; i < outputQueue.Count; i++) // While there are input tokens left
                {
                    // Read the next token from input.
                    string str = outputQueue[i];
                    //Console.WriteLine("String: " + str);
                    switch (str)
                    {                        
                        case "+":
                            if (result.Count >= 2)
                            {
                                double.TryParse(result.Pop(), out val1);
                                double.TryParse(result.Pop(), out val2);
                                result.Push((val1 + val2).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "-":
                            if (result.Count >= 2)
                            {
                                double.TryParse(result.Pop(), out val1);
                                double.TryParse(result.Pop(), out val2);
                                result.Push((val2 - val1).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "*":
                            if (result.Count >= 2)
                            {
                                double.TryParse(result.Pop(), out val1);
                                double.TryParse(result.Pop(), out val2);
                                result.Push((val1 * val2).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "/":
                            if (result.Count >= 2)
                            {
                                double.TryParse(result.Pop(), out val1);
                                double.TryParse(result.Pop(), out val2);
                                result.Push((val2 / val1).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "^":
                            if (result.Count >= 2)
                            {
                                double.TryParse(result.Pop(), out val1);
                                double.TryParse(result.Pop(), out val2);
                                result.Push(Math.Pow(val2, val1).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "~":
                            if (result.Count >= 1)
                            {
                                double.TryParse(result.Pop(), out val1);
                                result.Push((-val1).ToString());
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "pi":
                                result.Push((Math.PI).ToString());
                            break;
                        case "e":
                                result.Push((Math.E).ToString());
                            break;
                        case "sin":
                            if (result.Count >= 1)
                            {
                                double.TryParse(result.Pop(), out val1);
                                result.Push((Math.Sin(val1)).ToString());
                                RPNDebugLog("Functions are a little funky.");
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "cos":
                            if (result.Count >= 1)
                            {
                                double.TryParse(result.Pop(), out val1);
                                result.Push((Math.Cos(val1)).ToString());
                                RPNDebugLog("Functions are a little funky.");
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        case "tan":
                            if (result.Count >= 1)
                            {
                                double.TryParse(result.Pop(), out val1);
                                result.Push((Math.Tan(val1)).ToString());
                                RPNDebugLog("Functions are a little funky.");
                            } else {
                                RPNDebugLog("Evaluation error.", 1);
                            }
                            break;
                        default:
                            // If the token is a number
                            double val = 0.0;
                            double.TryParse(str, out val);
                            result.Push(val.ToString());
                            break;
                    }
                    if (infoDebug)
                    {
                        Console.WriteLine("#Current str: " + str);
                        Console.WriteLine("#Evaluation: " + val1 + ", " + val2);
                        Console.WriteLine("#Result: " + result);
                    }
                }
                if (infoDebug)
                {
                    Console.WriteLine("#-----------------------------#");
                    for (int i = 0; i < debugLog.Count; i++)
                    {
                        Console.WriteLine(debugLog[i]);
                    }
                }
                Console.WriteLine("#-#-#-#-#-#-#-#-#-#-#-#-#-#-#-#");
                if (errCount == 0)
                    Console.WriteLine("Result: " + (result.Count == 1 ? result.ToString() : "Evaluation error!"));
                else
                    Console.WriteLine("Invalid Expression!");
                errCount = 0;
                debugLog.Clear();
            }
            //Console.ReadKey();
            Console.WriteLine("Exiting...");
        }
    }
}
