using System;
using System.Text.RegularExpressions;

namespace json_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Regex r = new Regex("sdfsdf");
            // Objects => {}, {"age": 23 }
            // Arrays => [ {} , "", ...]
            // Value => "test" | 123 | 23.2 | {} | [] | true | false | null

            //string jsonInput = "{\"menu\": {\"id\": \"file\",\"value\": \"File\",\"popup\": {\"menuitem\": [{\"value\": \"New\", \"onclick\": \"CreateNewDoc()\"},{\"value\": \"Open\", \"onclick\": \"OpenDoc()\"},{\"value\": \"Close\", \"onclick\": \"CloseDoc()\"}]}}}";
            string jsonInput = "{\"widget\": {\"debug\": false, \"listOfStuff\":[123, 123.44, \"test\", {}],\"window\": { \"fullscreen\": true, \"title\": \"Sample Konfabulator Widget\",\"name\": \"main_window\",\"width\": 500,\"height\": 500},\"image\": { \"src\": \"Images/Sun.png\",\"name\": \"sun1\",\"hOffset\": 250,        \"vOffset\": 250,        \"alignment\": \"center\"    },    \"text\": {        \"data\": \"Click Here\",        \"size\": 36,        \"style\": \"bold\",        \"name\": \"text1\",        \"hOffset\": 250,        \"vOffset\": 100,        \"alignment\": \"center\",        \"onMouseUp\": \"sun1.opacity = (sun1.opacity / 100) * 90;\"    }}}    ";
            string tmp = string.Empty;
            string numbers = "0123456789";
            string lastToken = string.Empty;

            // use a while loop instead
            // to get n-number of nested objects/arrays etc need to use recursion.
            
            for (int i = 0; i < jsonInput.Length; i++)
            {
                char c = jsonInput[i];

                if (c == '{')
                {
                    lastToken = "[OBJECT_START]";
                    Console.WriteLine(lastToken);
                }
                else if (c == '}')
                {
                    lastToken = "[OBJECT_END]";
                    Console.WriteLine(lastToken);
                }
                else if (c == '"')
                {
                    lastToken = "[STRING_START]";
                    Console.WriteLine(lastToken);
                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == '"')
                        {
                            lastToken = "[STRING_END]";
                            Console.WriteLine(lastToken);
                            i = j;
                            break;
                        }
                    }
                }
                else if ((c == 'f' || c == 't') && lastToken == "[COLON]")
                {
                    lastToken = "[BOOLEAN_START]";
                    Console.WriteLine(lastToken);

                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == 'e')
                        {
                            lastToken = "[BOOLEAN_END]";
                            Console.WriteLine(lastToken);
                            i = j;
                            break;
                        }
                    }
                }
                else if (numbers.Contains(c))
                {
                    lastToken="[NUMBER_START]";
                    Console.WriteLine(lastToken);

                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == ',' || c2 == '}')
                        {
                            lastToken = "[NUMBER_END]";
                            Console.WriteLine(lastToken);
                            i = j;
                            break;
                        }
                    }
                }
                else if (c == '[')
                {
                    lastToken = "[ARRAY_START]";
                    Console.WriteLine(lastToken);
                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == ']')
                        {
                            lastToken = "[ARRAY_END]";
                            Console.WriteLine(lastToken);
                            i = j;
                            break;
                        }
                        else if (c2 == '{')
                        {
                            lastToken = "[OBJECT_START]";
                            Console.WriteLine(lastToken);
                        }
                        else if (c2 == '}')
                        {
                            lastToken = "[OBJECT_END]";
                            Console.WriteLine(lastToken);
                        }
                        else if (c2 == ',')
                        {
                            lastToken = "[SEPERATOR]";
                            Console.WriteLine(lastToken);
                        }
                        else if (c2 == '"')
                        {
                            lastToken = "[STRING_START]";
                            Console.WriteLine(lastToken);
                            for (int k = j + 1; k < jsonInput.Length; k++)
                            {
                                char c3 = jsonInput[k];
                                if (c3 == '"')
                                {
                                    lastToken = "[STRING_END]";
                                    Console.WriteLine(lastToken);
                                    j = k;
                                    break;
                                }
                            }
                        }
                        else if (numbers.Contains(c2))
                        {
                            lastToken="[NUMBER_START]";
                            Console.WriteLine(lastToken);

                            for (int k = j + 1; k < jsonInput.Length; k++)
                            {
                                char c3 = jsonInput[k];
                                if (c3 == ',' || c3 == '}')
                                {
                                    lastToken = "[NUMBER_END]";
                                    Console.WriteLine(lastToken);
                                    j = k;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (c == ',')
                {
                    lastToken = "[SEPERATOR]";
                    Console.WriteLine(lastToken);
                }
                else if (c == ':')
                {
                    lastToken = "[COLON]";
                    Console.WriteLine(lastToken);
                }
            }

            Console.ReadLine();
        }
    }
}
