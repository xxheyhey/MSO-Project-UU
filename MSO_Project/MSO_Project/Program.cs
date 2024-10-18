using System;
using System.Collections.Generic;

namespace MSO_Project
{
    internal static class Program
    {
        private static void Main()
        {
            string? readValue(List<string> expected_inputs)
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

            string? input1 = null;
            while (input1 == null)
            {
                Console.WriteLine("Choose one of the following example programs:");
                Console.WriteLine("1. Beginner\n2. Intemediate\n3. Advanced");
                Console.Write("Type 1, 2 or 3: ");
                input1 = readValue(["1", "2", "3"]);

            }

            Game game = Game.Examples[int.Parse(input1) - 1];

            string? input2 = null;
            while (input2 == null)
            {
                Console.WriteLine("Do you want to execute the program or analyze its metrics?");
                Console.WriteLine("1. Execute\n2. Analyze Metrics");
                Console.Write("Type 1 or 2: ");
                input2 = readValue(["1", "2"]);
            }

            Console.Clear();
            if (input2 == "1")
            {
                game.Execute();
            }
            else
            {
                game.CalculateMetrics();
            }

            // Game importedGame = Game.Import("../../../test.txt");
            // importedGame.Execute();
            // importedGame.CalculateMetrics();

        }
    }
}
