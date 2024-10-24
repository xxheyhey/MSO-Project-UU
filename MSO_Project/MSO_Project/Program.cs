using System;
using System.Collections.Generic;
using System.IO;

namespace MSO_Project
{
    public static class Program
    {
        // This class will change drastically once we change from a CLI to a GUI platform.

        private static void Main()
        {
            Run();
        }

        private static string? _readValue(List<string> expectedInputs)
        {
            string? input = Console.ReadLine();
            foreach (string s in expectedInputs)
            {
                if (input == s)
                {
                    return input;
                }
            }

            Console.Clear();
            Console.WriteLine("Invalid input. Please try again.\n");
            return null;
        }

        private static void Run()
        {
            Game? game;
            string? input1 = null;
            while (input1 == null)
            {
                Console.WriteLine("Do you want to choose an example program or load a program file from your Documents directory?");
                Console.WriteLine("1. Choose example program\n2. Load file");
                Console.Write("Type 1 or 2: ");
                input1 = _readValue(["1", "2"]);
            }

            Console.Clear();

            if (input1 == "1")
            {
                string? input2 = null;
                while (input2 == null)
                {
                    Console.WriteLine("Choose one of the following example programs:");
                    Console.WriteLine("1. Beginner\n2. Intermediate\n3. Advanced");
                    Console.Write("Type 1, 2 or 3: ");
                    input2 = _readValue(["1", "2", "3"]);
                }

                game = Game.Examples[int.Parse(input2) - 1];
                Console.Clear();
            }
            else
            {
                Console.WriteLine("Specify a file in your Documents directory:");
                string file = GetFile();
                game = Game.Import(file);
            }

            string? input3 = null;
            while (input3 == null)
            {
                Console.WriteLine("Do you want to execute the program or analyze its metrics?");
                Console.WriteLine("1. Execute\n2. Analyze metrics");
                Console.Write("Type 1 or 2: ");
                input3 = _readValue(["1", "2"]);
            }

            Console.Clear();
            if (input3 == "1")
            {
                game.Execute();
                Console.ReadLine();
            }
            else
            {
                game.CalculateMetrics();
                Console.ReadLine();
            }
        }

        public static string GetFile()
        {
            // Get the user's Documents directory:
            string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string documentspath = Path.Combine(homePath, "Documents");

            Console.WriteLine($"The Documents directory is: {documentspath}");

            string? filePath;
            while (true)
            {
                Console.Write("Please enter the file name (in the Documents folder): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                filePath = Path.Combine(documentspath, fileName);
                if (File.Exists(filePath))
                {
                    Console.Clear();
                    Console.WriteLine("File selected: " + filePath);
                    break;
                }

                Console.WriteLine("File does not exist in the Documents folder. Please try again.");
            }

            return filePath;
        }
    }
}