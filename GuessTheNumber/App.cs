using GuessTheNumber.Pages;

namespace GuessTheNumber;

public class App
{
    public User? User { get; set; }
    public void ChangePage(Page page)
    {
        Thread.Sleep(100);
        
        page.App = this;
        page.Display();
    }
    
    public void Start()
    {
        ChangePage(new StartPage());
    }
}