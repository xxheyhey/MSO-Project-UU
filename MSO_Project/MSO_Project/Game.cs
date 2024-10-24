using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MSO_Project
{
    public class Game
    {
        private string _name { get; }
        private Character _character { get; }
        private List<Command> _commands { get; }

        public Game(string name, Character character, List<Command> commands)
        {
            _name = name;
            _character = character;
            _commands = commands;
        }

        public static readonly List<Game> Examples = new List<Game>
        {
            new Game("beginner", new Character(), new List<Command>
            {
                new Move(10),
                new Turn("right"),
                new Move(10),
                new Turn("right"),
                new Move(3),
                new Move(10),
                new Turn("right")
            }),

            new Game("intermediate", new Character(), new List<Command>
            {
                new Repeat(4, new List<Command>
                {
                    new Move(10),
                    new Turn("left"),
                    new Turn("left"),
                    new Move(7),
                    new Turn("right")
                })
            }),

            new Game("advanced", new Character(), new List<Command>
            {
                new Move(5),
                new Turn("left"),
                new Turn("left"),
                new Move(3),
                new Turn("right"),
                new Repeat(3, new List<Command>
                {
                    new Move(1),
                    new Turn("right"),
                    new Repeat(5, new List<Command> { new Move(2) })
                }),
                new Turn("left"),
            })
        };

        // Private helper methods:

        private int GetMaxNestingLevel()
        {
            int maxNestingLevel = 1; // Minimum of 1 for programs without repeats

            foreach (var command in _commands)
            {
                maxNestingLevel = Math.Max(maxNestingLevel, command.MaxNestingLevel());
            }

            return maxNestingLevel;
        }

        private static int CountRepeatCommands(List<Command> commands)
        {
            int repeatCount = 0;

            foreach (var command in commands)
            {
                if (command is not Repeat repeatCommand) continue;
                repeatCount++;
                repeatCount += CountRepeatCommands(repeatCommand.Commands);
            }

            return repeatCount;
        }

        private static List<Command> ParseCommands(List<string> lines, int indentationLevel)
        {
            List<Command> commands = new List<Command>();

            for (int i = 0; i < lines.Count; i++)
            {
                string line = lines[i];

                int currentIndentation = line.TakeWhile(c => c == '\t').Count();

                // Skip lines that do not match the current indentation level
                if (currentIndentation != indentationLevel)
                    continue;

                line = line.TrimStart('\t');
                string[] words = line.Split();

                switch (words[0].ToLower())
                {
                    case "move":
                        commands.Add(new Move(int.Parse(words[1])));
                        break;
                    case "turn":
                        commands.Add(new Turn(words[1]));
                        break;
                    case "repeat":
                    {
                        int iterations = int.Parse(words[1]);

                        List<string> nestedLines = lines.Skip(i + 1).ToList(); // This collects all the commands within a repeat block

                        // This recursively parses the nested commands
                        List<Command> nestedCommands = ParseCommands(nestedLines, indentationLevel + 1);

                        commands.Add(new Repeat(iterations, nestedCommands));

                        i += nestedCommands.Count; // Continuing the parsing after the repeat block
                        break;
                    }
                }
            }

            return commands;
        }

        public void Execute()
        {
            foreach (var c in _commands)
            {
                c.Execute(_character);
            }

            Console.WriteLine(this);
        }

        public void CalculateMetrics()
        {
            int maxNestingLevel = GetMaxNestingLevel();
            int numberOfRepeats = CountRepeatCommands(_commands);
            int numberOfCommands = 0;
            foreach (Command c in _commands)
            {
                numberOfCommands += c.NumberOfCommands();
            }

            Console.WriteLine($"Program: {_name}\n"
                              + $"Number of commands: {numberOfCommands}\n"
                              + $"Maximum nesting level: {maxNestingLevel}\n"
                              + $"Number of repeats: {numberOfRepeats}");
        }

        public static Game Import(string file)
        {
            Game game = new Game(file, new Character(), new List<Command>());

            List<string> lines = File.ReadLines(file).ToList();
            List<Command> commands = ParseCommands(lines, 0);

            game._commands.AddRange(commands);

            return game;
        }

        public override string ToString()
        {
            var cmds = from com in _commands select com.ToString();
            return $"Program: {_name}\n"
                   + $"{string.Join(", ", cmds)}.\n"
                   + $"End state {_character.Position} facing {_character.Orientation}.";
        }
    }
}
