using System;
using System.Security.Cryptography.X509Certificates;

namespace CMD_lang
{
    public class Program
    {
        public static void Main()
        {
            try
            {
                string[] args = Environment.GetCommandLineArgs();
                string file;
                if (args.Length < 2)
                {
                    Console.WriteLine("TIP: you can run files directly in this application! Then this text will not appear");
                    Console.Write("Enter the absolute path to the file to run: ");
                    string input = Console.ReadLine();
                    file = input;
                }
                else
                {
                    file = args[1];
                }
                if (File.Exists(file))
                {
                    eval(File.ReadAllText(file));
                }
                else
                {
                    Console.WriteLine("Error: no such file found");
                }
            } catch(Exception ex)
            {
                Console.WriteLine(ex.Message + "\n \n" + ex.StackTrace + "\n \n" + ex.Source);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Runs all the code in the <paramref name="lines"/> parameter
        /// </summary>
        /// <param name="lines">The lines to run</param>
        public static void eval(string lines)
        {
            string[] split = lines.Split("\n");

            foreach(string s in split)
            {
                try
                {
                    // Make sure that the line is not a comment
                    if (!s.StartsWith("//"))
                    {
                        // You can define your classes/namespaces here
                        if (s.StartsWith("Console."))
                        {
                            string function = s.Replace("Console.", "");
                            if (function.StartsWith("textColour"))
                            {
                                // Handle assigning a new text colour (Console.ForegroundColor)
                                if (function.Contains(" = "))
                                {
                                    string[] splitLine = function.Split(" = ");
                                    Console.ForegroundColor = Enum.Parse<ConsoleColor>(splitLine[1]);
                                }
                            }
                            if (function.StartsWith("backgroundColour"))
                            {
                                // Handle assigning a new background colour (Console.BackgroundColor)
                                if (function.Contains(" = "))
                                {
                                    string[] splitLine = function.Split(" = ");
                                    Console.BackgroundColor = Enum.Parse<ConsoleColor>(splitLine[1]);
                                }
                            }
                            if (function.StartsWith("title"))
                            {
                                // Handle assigning a new title
                                if (function.Contains(" = "))
                                {
                                    string[] splitLine = function.Split(" = ");
                                    splitLine[1] = splitLine[1].Replace("\"", "");
                                    Console.Title = splitLine[1];
                                }
                            }
                            if (function.StartsWith("write"))
                            {
                                // Writes to the window
                                string[] splitLine = function.Split("(");
                                string splitLine1Og = splitLine[1];
                                splitLine[1] = splitLine[1].Replace(")", "");
                                splitLine[1] = splitLine[1].Replace("\"", "");
                                //ConsoleKeyInfo keyInfo;
                                //// referencing the "currKey" variable causes the script to wait until a key has been pressed
                                //if (splitLine1Og == "currKey")
                                //{
                                //    keyInfo = Console.ReadKey(true);
                                //    Console.WriteLine(keyInfo.KeyChar);
                                //} else
                                //{
                                    Console.WriteLine(splitLine[1]);
                                //}
                            }
                            if (function.StartsWith("waitForKey"))
                            {
                                // Suspends the script until the user presses a key, a boolean can be provided to the "intercept" parameter
                                string[] splitLine = function.Split("(");
                                if (splitLine.Length > 1)
                                {
                                    splitLine[1] = splitLine[1].Replace(")", "");
                                    splitLine[1] = splitLine[1].Replace("\"", "");
                                    Console.ReadKey(bool.Parse(splitLine[1]));
                                } else
                                {
                                    Console.ReadKey();
                                }
                            }
                            if (function.StartsWith("resetColours"))
                            {
                                // Resets the console's colours
                                Console.ResetColor();
                            }
                        }
                    }
                } catch(Exception ex)
                {
                    Console.WriteLine("Error on script! " + ex.Message);
                    Console.WriteLine("Stack trace: \n" + ex.StackTrace);
                    return;
                }
            }
            Console.WriteLine("The code has finished, press any key to exit");
            Console.ReadKey(true);
        }
    }
}