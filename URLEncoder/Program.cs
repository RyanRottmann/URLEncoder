using System;

namespace URLEncoder
{
    class Program
    {
        static void Main(string[] args)
        {

            string projectName;
            string activityName;

            bool validity;

            Console.WriteLine("Enter your project name");
            projectName = Console.ReadLine();
            validity = IsValid(projectName);
            while (validity == false)
            {
                Console.WriteLine("Invalid input you entered a control character!");
                Console.WriteLine("Enter a valid project name");
                projectName = Console.ReadLine();

            }

            Console.WriteLine("Enter your activity name");
            activityName = Console.ReadLine();
            validity = IsValid(activityName);
            while (validity == false)
            {
                Console.WriteLine("Invalid input you entered a control character!");
                Console.WriteLine("Enter a valid activity name");
                activityName = Console.ReadLine();

            }

            string encodedProjectName = Encode(projectName);
            string encodedActivityName = Encode(activityName);
            //Console.WriteLine(encodedProjectName);
            //Console.WriteLine(encodedActivityName);

            Console.WriteLine("http://companyserver.com/content/{0}/files/{1}/{1}Report.pdf", encodedProjectName, encodedActivityName);

        }

        static bool IsValid(string input)
        {
            char[] inputCharArray = input.ToCharArray();
            foreach (char character in inputCharArray)
            {
                if (character >= 0x00 && character <= 0x1f)
                {
                    return false;

                }
                else if (character == 0x7F)
                {
                    return false;
                }

            }
            return true;
        }

        static string Encode(string value)
        {
            // safe values A-Z, a-z, 0-9, -, ., _, ~, !, $, ', (, ), *, +, ,comma, %


            string[] encodeCharacters = { " ", "\"", "#", "&", "/", ":", ";", "<", "=", ">", "?", "@", "[", "\\", "]", "^", "`", "{", "|", "}" };
            string[] encodedCharacters = { "%20", "%22", "%23", "%26", "%2F", "%3A", "%3B", "%3C", "%3D", "%3E", "%3F", "%40", "%5B", "%5C", "%5D", "%5E", "%60", "%7B", "%7C", "%7D" };
            bool[] encodeBools = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };

            int x = 0;
            int length = encodeCharacters.Length;
            while (x < length)
            {
                while (encodeBools[x] == true)
                {
                    encodeBools[x] = value.Contains(encodeCharacters[x]);
                    if (encodeBools[x] == true)
                    {
                        int index = value.IndexOf(encodeCharacters[x], StringComparison.Ordinal);
                        if (index >= 0)
                        {
                            value = value.Remove(index, 1);
                            value = value.Insert(index, encodedCharacters[x]);
                        }

                    }
                    encodeBools[x] = value.Contains(encodeCharacters[x]);
                }
                x++;
            }


            //string space = " ";
            //bool spaceBool = true;

            //while (spaceBool == true)
            //{
            //    spaceBool = value.Contains(space);
            //    if (spaceBool == true)
            //    {
            //        int index = value.IndexOf(space, StringComparison.Ordinal);
            //        if (index >= 0)
            //        {
            //            value = value.Remove(index, 1);
            //            value = value.Insert(index, "%20");
            //        }
            //    }
            //    spaceBool = value.Contains(space);
            //}
            return value;
        }

    }
}
