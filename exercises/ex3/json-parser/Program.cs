using System;
using System.Text.RegularExpressions;

namespace json_parser
{
    class Program
    {
        const string numbers = "0123456789";

        static void Main(string[] args)
        {
            string jsonInput = "{\"widget\": {\"handle\":null, \"debug\": false, \"listOfStuff\":[123, 123.44, \"test\", {}],\"window\": { \"fullscreen\": true, \"title\": \"Sample Konfabulator Widget\",\"name\": \"main_window\",\"width\": 500,\"height\": 500},\"image\": { \"src\": \"Images/Sun.png\",\"name\": \"sun1\",\"hOffset\": 250,        \"vOffset\": 250,        \"alignment\": \"center\"    },    \"text\": {        \"data\": \"Click Here\",        \"size\": 36,        \"style\": \"bold\",        \"name\": \"text1\",        \"hOffset\": 250,        \"vOffset\": 100,        \"alignment\": \"center\",        \"onMouseUp\": \"sun1.opacity = (sun1.opacity / 100) * 90;\"    }}}    ";
            
            string lastToken = string.Empty;

            ProcessRootState(jsonInput, out lastToken);
        }

        static void ProcessRootState(string jsonInput, out string lastToken)
        {
            for (int i = 0; i < jsonInput.Length; i++)
            {
                char c = jsonInput[i];

                if (c == '{')
                {
                    i = ProcessObjectState(jsonInput, i, out lastToken);
                } 
                else if ( c == '[')
                {
                    i = ProcessArrayState(jsonInput, i, out lastToken);
                }
            }

            lastToken = "[END]";
        }

        static int ProcessObjectState(string jsonInput, int i, out string lastToken)
        {
            lastToken = "[OBJECT_START]";
            Console.WriteLine(lastToken);
            for (int j = i + 1; j < jsonInput.Length; j++)
            {
                char c = jsonInput[j];

                if (c == '{')
                {
                    j = ProcessObjectState(jsonInput, j, out lastToken);
                } else if (c == '}')
                {
                    lastToken = "[OBJECT_END]";
                    Console.WriteLine(lastToken);
                }
                else if (c == '"')
                {
                    j = ProcessStringState(jsonInput, j, out lastToken);
                }
                else if ((c == 'n') && lastToken == "[COLON]")
                {
                    j = ProcessNullState(jsonInput, j, out lastToken);
                }
                else if ((c == 'f' || c == 't') && lastToken == "[COLON]")
                {
                    j = ProcessBooleanState(jsonInput, j, out lastToken);
                }
                else if (numbers.Contains(c) && lastToken == "[COLON]")
                {
                    j = ProcessNumberState(jsonInput, j, out lastToken);
                }
                else if (c == '[')
                {
                    j = ProcessArrayState(jsonInput, j, out lastToken);
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

            return jsonInput.Length;
        }

        static int ProcessArrayState(string jsonInput, int i, out string lastToken)
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
                    return j;
                }
                else if (c2 == '{')
                {
                    j = ProcessObjectState(jsonInput, j, out lastToken);
                }
                else if (c2 == ',')
                {
                    lastToken = "[SEPERATOR]";
                    Console.WriteLine(lastToken);
                }
                else if (c2 == '"')
                {
                    j = ProcessStringState(jsonInput, j, out lastToken);
                }
                else if (numbers.Contains(c2))
                {
                    j = ProcessNumberState(jsonInput, j, out lastToken);
                }
            }

            return jsonInput.Length;
        }

        static int ProcessNumberState(string jsonInput, int i, out string lastToken)
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
                    return j;
                }
            }
            return jsonInput.Length;
        }

        static int ProcessBooleanState(string jsonInput, int i, out string lastToken)
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
                    return j;
                }
            }
            return jsonInput.Length;
        }

        static int ProcessNullState(string jsonInput, int i, out string lastToken)
        {
            lastToken = "[NULL_START]";
            Console.WriteLine(lastToken);

            for (int j = i + 1; j < jsonInput.Length; j++)
            {
                char c2 = jsonInput[j];
                if (c2 == 'l')
                {
                    lastToken = "[NULL_END]";
                    Console.WriteLine(lastToken);
                    return j;
                }
            }

            return jsonInput.Length;
        }

        static int ProcessStringState(string jsonInput, int i, out string lastToken)
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
                    return j;
                }
            }

            return jsonInput.Length;
        }
    }
}
