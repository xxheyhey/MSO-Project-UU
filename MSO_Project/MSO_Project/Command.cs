using System.Collections.Generic;


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
                "east" => "south",
                "west" => "north",
                "north" => "east",
                "south" => "west",
                _ => character.Orientation
            };
        }
        else if (direction == "left")
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
            "east" => (x + steps, y),
            "west" => (x - steps, y),
            "north" => (x, y + steps),
            "south" => (x, y - steps),
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
        List<string> comms = new List<string>();
        for (int i = 0; i < iterations; i++)
        {
            foreach (Command c in commands)
            {
                comms.Add(c.ToString());
            }
        }
        return $"{string.Join(", ", comms)}";
    }
}
