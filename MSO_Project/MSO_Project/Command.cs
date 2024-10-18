using System;
using System.Collections.Generic;


namespace MSO_Project;

public abstract class Command
{
    public abstract void Execute(Character character);
    public abstract int NumberOfCommands(); // Needed to calculate the total number of commands of a game
    public abstract int MaxNestingLevel(); // Needed to calculate the maximum nesting level of a game
    public abstract override string ToString();
}

public class Turn(string direction) : Command
{
    private string _direction { get; } = direction;

    public override void Execute(Character character)
    {
        if (_direction == "right")
        {
            character.Orientation = character.Orientation switch
            {
                "east" => "south",
                "west" => "north",
                "north" => "east",
                "south" => "west",
                _ => character.Orientation
            };
        }
        else if (_direction == "left")
        {
            character.Orientation = character.Orientation switch
            {
                "east" => "north",
                "west" => "south",
                "north" => "west",
                "south" => "east",
                _ => character.Orientation
            };
        }
    }

    public override int NumberOfCommands() => 1;
    public override int MaxNestingLevel() => 1;

    public override string ToString()
    {
        return $"Turn {_direction}";
    }
}

public class Move(int steps) : Command
{
    private int _steps { get; } = steps;

    public override void Execute(Character character)
    {
        (int x, int y) = character.Position;

        character.Position = character.Orientation switch
        {
            "east" => (x + _steps, y),
            "west" => (x - _steps, y),
            "north" => (x, y + _steps),
            "south" => (x, y - _steps),
            _ => character.Position
        };
    }

    public override int NumberOfCommands() => 1;
    public override int MaxNestingLevel() => 1;

    public override string ToString()
    {
        return $"Move {_steps}";
    }
}

public class Repeat(int iterations, List<Command> commands) : Command
{
    private int _iterations { get; } = iterations;
    public List<Command> Commands { get; } = commands;

    public override void Execute(Character character)
    {
        for (int i = 0; i < _iterations; i++)
        {
            foreach (Command c in Commands)
            {
                c.Execute(character);
            }
        }
    }

    public override int NumberOfCommands()
    {
        int totalCommands = 0;
        foreach (var command in Commands)
        {
            totalCommands += command.NumberOfCommands();
        }

        return totalCommands * _iterations;
    }

    public override int MaxNestingLevel()
    {
        int maxNesting = 1;
        foreach (var command in Commands)
        {
            maxNesting = Math.Max(maxNesting, command.MaxNestingLevel() + 1);
        }

        return maxNesting;
    }

    public override string ToString()
    {
        List<string> comms = new List<string>();
        for (int i = 0; i < _iterations; i++)
        {
            foreach (Command c in Commands)
            {
                comms.Add(c.ToString());
            }
        }

        return $"{string.Join(", ", comms)}";
    }
}