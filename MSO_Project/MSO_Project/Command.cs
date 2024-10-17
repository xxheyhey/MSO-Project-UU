using System;
using System.Collections.Generic;
using System.Linq;


namespace MSO_Project;

public abstract class Command
{
    public abstract void Execute(Character character);
    public abstract override string ToString();
}

public class Turn(string direction) : Command
{
    private string direction { get; set; } = direction;
    
    public override void Execute(Character character)
    {
        if (direction == "right")
        {
            character.Orientation = character.Orientation switch
            {
                "right" => "down",
                "left" => "up",
                "up" => "right",
                "down" => "left",
                _ => character.Orientation
            };
        }
        else if (direction == "left")
        {
            character.Orientation = character.Orientation switch
            {
                "right" => "up",
                "left" => "down",
                "up" => "left",
                "down" => "right",
                _ => character.Orientation
            };
        }
    }

    public override string ToString()
    {
        return $"Turn {direction}";
    }
}

public class Move(int steps) : Command
{
    private int steps { get; set; } = steps;

    public override void Execute(Character character)
    {
        (int x, int y) = character.Position;

        character.Position = character.Orientation switch
        {
            "right" => (x + steps, y),
            "left" => (x - steps, y),
            "up" => (x, y + steps),
            "down" => (x, y - steps),
            _ => character.Position
        };
    }
    
    public override string ToString()
    {
        return $"Move {steps}";
    }
}

public class Repeat(int iterations, List<Command> commands) : Command
{
    private int iterations { get; set; } = iterations;
    private List<Command> commands { get; set; } = commands;
    
    public override void Execute(Character character)
    {
        for (int i = 0; i < iterations; i++)
        {
            foreach (Command c in commands)
            {
                c.Execute(character);
            }
        }
    }
    
    public override string ToString()
    {
        var comms = from com in commands select com.ToString();
        return $"Repeat {iterations} ({string.Join(", ", comms)})";
    }
}