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
            string jsonInput = "{\"widget\": {\"debug\": false,\"window\": {\"title\": \"Sample Konfabulator Widget\",\"name\": \"main_window\",\"width\": 500,\"height\": 500},\"image\": { \"src\": \"Images/Sun.png\",\"name\": \"sun1\",\"hOffset\": 250,        \"vOffset\": 250,        \"alignment\": \"center\"    },    \"text\": {        \"data\": \"Click Here\",        \"size\": 36,        \"style\": \"bold\",        \"name\": \"text1\",        \"hOffset\": 250,        \"vOffset\": 100,        \"alignment\": \"center\",        \"onMouseUp\": \"sun1.opacity = (sun1.opacity / 100) * 90;\"    }}}    ";
            string tmp = string.Empty;
            string numbers = "0123456789";

            // use a while loop instead
            for (int i = 0; i < jsonInput.Length; i++)
            {
                char c = jsonInput[i];

                if (c == '{')
                {
                    Console.WriteLine("[OBJECT_START]");
                }
                else if (c == '}')
                {
                    Console.WriteLine("[OBJECT_END]");
                }
                else if (c == '"')
                {
                    Console.WriteLine("[STRING_START]");
                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == '"')
                        {
                            Console.WriteLine("[STRING_END]");
                            i = j;
                            break;
                        }
                    }
                }
                else if (numbers.Contains(c))
                {
                    Console.WriteLine("[NUMBER_START]");

                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == ',' || c2 == '}')
                        {
                            Console.WriteLine("[NUMBER_END]");
                            i = j;
                            break;
                        }
                    }
                }
                else if (c == '[')
                {
                    Console.WriteLine("[ARRAY_START]");
                    for (int j = i + 1; j < jsonInput.Length; j++)
                    {
                        char c2 = jsonInput[j];
                        if (c2 == ']')
                        {
                            Console.WriteLine("[ARRAY_END]");
                            i = j;
                            break;
                        }
                        else if (c2 == '{')
                        {
                            Console.WriteLine("[OBJECT_START]");
                        }
                        else if (c2 == '}')
                        {
                            Console.WriteLine("[OBJECT_END]");
                        }
                        else if (c2 == ',')
                        {
                            Console.WriteLine("[SEPERATOR]");
                        }
                        else if (c2 == '"')
                        {
                            Console.WriteLine("[STRING_START]");
                            for (int k = j + 1; k < jsonInput.Length; k++)
                            {
                                char c3 = jsonInput[k];
                                if (c3 == '"')
                                {
                                    Console.WriteLine("[STRING_END]");
                                    j = k;
                                    break;
                                }
                            }
                        }
                    }
                }
                else if (c == ',')
                {
                    Console.WriteLine("[SEPERATOR]");
                }
                else if (c == ':')
                {
                    Console.WriteLine("[COLON]");
                }
            }

            Console.ReadLine();
        }
    }
}
