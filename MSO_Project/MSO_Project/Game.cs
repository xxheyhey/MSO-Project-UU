using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace MSO_Project;

public class Game(string name, Character character, List<Command> commands)
{
    private string _name { get; } = name;
    private Character _character { get; } = character;
    private List<Command> _commands { get; } = commands;

    public static List<Game> Examples = new List<Game>
    {
        new Game("beginner", new Character(), [
            new Move(10),
            new Turn("right"),
            new Move(10),
            new Turn("right"),
            new Move(3),
            new Move(10),
            new Turn("right")
        ]),
        new Game("intermediate", new Character(), [
            new Repeat(4, [
                new Move(10),
                new Turn("left"),
                new Turn("left"),
                new Move(7),
                new Turn("right")
            ])
        ]),
        new Game("advanced", new Character(), [
            new Move(5),
            new Turn("left"),
            new Turn("left"),
            new Move(3),
            new Turn("right"),
            new Repeat(3, [new Move(1), new Turn("right"), new Repeat(5, [new Move(2)])]),
            new Turn("left"),
        ])
    };

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
        int GetMaxNestingLevel(List<Command> commands)
        {
            int maxNestingLevel = 1; // Minimum of 1 for programs without repeats

            foreach (var command in commands)
            {
                maxNestingLevel = Math.Max(maxNestingLevel, command.MaxNestingLevel());
            }

            return maxNestingLevel;
        }

        int CountRepeatCommands(List<Command> commands)
        {
            int repeatCount = 0;

            foreach (var command in commands)
            {
                if (command is Repeat repeatCommand)
                {
                    repeatCount++;
                    repeatCount += CountRepeatCommands(repeatCommand.Commands);
                }
            }

            return repeatCount;
        }

        int maxNestingLevel = GetMaxNestingLevel(_commands);
        int numberofRepeats = CountRepeatCommands(_commands);
        int numberOfCommands = 0;
        foreach (Command c in _commands)
        {
            numberOfCommands += c.NumberOfCommands();
        }

        Console.WriteLine($"Program: {_name}\n"
                          + $"Number of commands: {numberOfCommands}\n"
                          + $"Maximum nesting level: {maxNestingLevel}\n"
                          + $"Number of repeats: {numberofRepeats}");
    }

    public static Game Import(string file)
    {
        Game game = new Game(file, new Character(), new List<Command>());

        foreach (string line in File.ReadLines(file))
        {
            string[] words = line.Split();

            if (words[0] == "Move")
                game._commands.Add(new Move(int.Parse(words[1])));
            else if (words[0] == "Turn")
                game._commands.Add(new Turn(words[1]));
        }

        return game;
    }

    public override string ToString()
    {
        var comms = from com in _commands select com.ToString();
        return $"Program: {_name}\n"
               + $"{string.Join(", ", comms)}.\n"
               + $"End state {_character.Position} facing {_character.Orientation}.";
    }
}
