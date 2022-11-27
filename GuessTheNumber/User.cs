namespace GuessTheNumber;

public class User
{
    public User(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    
    public override String ToString()
    {
        return $"{Name}";
    }
}