using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;

namespace MSO_Project;

public class Game(string name, Character character, List<Command> commands)
{
    private string _name { get; set; } = name;
    private Character _character { get; set; } = character;
    private List<Command> _commands { get; set; } = commands;
    public static List<Game> Examples = new List<Game>
    {
        new Game("beginner", new Character("Tester", (0,0), "right"), new List<Command>{
            new Move(10),
            new Turn("right"),
            new Move(10),
            new Turn("right"),
            new Move(10),
            new Turn("right"),
            new Move(10),
            new Turn("right")
            })
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
        return;
    }
    
    /*
    public static Game Import(string file)
    {
        return new NotImplementedException();
    }
    */
    
    public override string ToString()
    {
        var comms = from com in _commands select com.ToString();
        return $"{string.Join(", ", comms)}.\nEnd state {_character.Position} facing {_character.Orientation}.";
    }
}