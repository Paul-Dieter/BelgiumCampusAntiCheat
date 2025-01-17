public class UserLogin
{
    private string username;

    public string Username { get => username; set => username = value; }

    public UserLogin() { }

    public UserLogin(string username)
    {
        this.username = username;
    }

    public virtual void Login()
    {
        Console.WriteLine("Default login process for a user.");
    }

    public virtual void PerformAction()
    {
        Console.Write("Loading: [");

        int t = 50;

        for (int i = 0; i <= t; i++)
        {
            Thread.Sleep(10);
            Console.Write(".");
        }
        Console.WriteLine("] Done!");
        Thread.Sleep(2000);
    }
}

public class Admin : UserLogin
{
    public Admin(string username) : base(username)
    {
    }

    public override void Login()
    {
        Console.WriteLine($"Admin login process for {Username}.");
    }
}

public class Student : UserLogin
{
    public Student(string username) : base(username)
    {
    }

    public override void Login()
    {
        Console.WriteLine($"Student login process for {Username}.");
    }
}
