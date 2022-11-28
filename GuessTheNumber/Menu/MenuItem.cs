namespace GuessTheNumber.Menu;

public delegate void OnItemSelected();

public class MenuItem
{
    public MenuItem(string title, OnItemSelected onSelected)
    {
        Title = title;
        OnSelected = onSelected;
    }

    public string Title { get; set; }
    public readonly OnItemSelected OnSelected;
}