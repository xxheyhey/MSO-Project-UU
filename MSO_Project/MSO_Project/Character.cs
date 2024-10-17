namespace MSO_Project;

public class Character(string name, (int, int) position, string orientation)
{
    private string _name = name;
    public (int, int) Position { get; set; } = position;
    public string Orientation { get; set; } = orientation;
}
