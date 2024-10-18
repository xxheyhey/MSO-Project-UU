using System;
using System.Collections.Generic;
using System.IO;

namespace MSO_Project
{
    internal static class Program
    {
        private static void Main()
        {
            Run();
        }

        private static string? readValue(List<string> expected_inputs)
        {
            string? input = Console.ReadLine();
            foreach (string s in expected_inputs)
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
            Game? game = null;
            string? input1 = null;
            while (input1 == null)
            {
                Console.WriteLine("Do you want to choose an example program or load a program file from your Downloads directory?");
                Console.WriteLine("1. Choose example program\n2. Load file");
                Console.Write("Type 1 or 2: ");
                input1 = readValue(["1", "2"]);
            }

            Console.Clear();

            if (input1 == "1")
            {
                string? input2 = null;
                while (input2 == null)
                {
                    Console.WriteLine("Choose one of the following example programs:");
                    Console.WriteLine("1. Beginner\n2. Intemediate\n3. Advanced");
                    Console.Write("Type 1, 2 or 3: ");
                    input2 = readValue(["1", "2", "3"]);
                }

                game = Game.Examples[int.Parse(input2) - 1];
            }
            else
            {
                Console.WriteLine("Specify a file in your Downloads directory:");
                string file = GetFile();
                game = Game.Import(file);
            }

            string? input3 = null;
            while (input3 == null)
            {
                Console.WriteLine("Do you want to execute the program or analyze its metrics?");
                Console.WriteLine("1. Execute\n2. Analyze metrics");
                Console.Write("Type 1 or 2: ");
                input3 = readValue(["1", "2"]);
            }

            Console.Clear();
            if (input3 == "1")
            {
                game.Execute();
            }
            else
            {
                game.CalculateMetrics();
            }
        }

        private static string GetFile()
        {
            // Get the user's Downloads directory:
            string homePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            string downloadsPath = Path.Combine(homePath, "Downloads");

            Console.WriteLine($"The Downloads directory is: {downloadsPath}");

            string? filePath = null;
            while (true)
            {
                Console.Write("Please enter the file name (in the Downloads folder): ");
                string? fileName = Console.ReadLine();

                if (string.IsNullOrEmpty(fileName))
                {
                    Console.WriteLine("Invalid input. Please try again.");
                    continue;
                }

                filePath = Path.Combine(downloadsPath, fileName);
                if (File.Exists(filePath))
                {
                    Console.Clear();
                    Console.WriteLine("File selected: " + filePath);
                    break;
                }

                Console.WriteLine("File does not exist in the Downloads folder. Please try again.");
            }

            return filePath;
        }
    }
}