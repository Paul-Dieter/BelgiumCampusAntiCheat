using BelgiumCampusAntiCheat.Operations;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace BelgiumCampusAntiCheat
{
    public static class CurrentUserInfo
    {
        public static string StudentUsername { get; set; }
    }
    internal class Program 
    {
        enum MenuAntiCheat
        {
            Admin = 1,
            Student,
            Exit = 99
        }

        static void Main(string[] args)
        {
            //Subscribing counter method to event.
            RefresherCounter.OnRefresh += RefresherCounter.UpdateCounter;
            Saved saved = new Saved();
            bool showMenu = true;
            while (showMenu)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine(@"██████  ███████ ██       ██████  ██ ██    ██ ███    ███      ██████  █████  ███    ███ ██████  ██    ██ ███████ 
██   ██ ██      ██      ██       ██ ██    ██ ████  ████     ██      ██   ██ ████  ████ ██   ██ ██    ██ ██");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"██████  █████   ██      ██   ███ ██ ██    ██ ██ ████ ██     ██      ███████ ██ ████ ██ ██████  ██    ██ ███████");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"██   ██ ██      ██      ██    ██ ██ ██    ██ ██  ██  ██     ██      ██   ██ ██  ██  ██ ██      ██    ██      ██ 
██████  ███████ ███████  ██████  ██  ██████  ██      ██      ██████ ██   ██ ██      ██ ██       ██████  ███████");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Choose an option below to continue:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("===============================================================================================================");
                Console.ForegroundColor = ConsoleColor.White;

                foreach (MenuAntiCheat MenuAntiCheat in Enum.GetValues(typeof(MenuAntiCheat)))
                {

                    Console.WriteLine("For {1} Enter {0}", (int)MenuAntiCheat, MenuAntiCheat.ToString());
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("===============================================================================================================");
                Console.ForegroundColor = ConsoleColor.White;
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Welcome Admin");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("Enter Admin Username: ");
                        string adminUsername = Console.ReadLine();
                        Console.Write("Enter Password: ");
                        string adminPassword = Console.ReadLine();
                        // ARMAND
                        UserLogin admin = new Admin(adminUsername);
                        List<UserLogin> users = new List<UserLogin> { admin };

                        foreach (var user in users)
                        {
                            user.Login();
                            user.PerformAction();
                        }

                        IUserValidator userValidatorAdmin = new UserValidator();

                        if (userValidatorAdmin.ValidateLoginAdmin(adminUsername, adminPassword))
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Login successful!");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("1. Add a new Admin");
                            Console.WriteLine("2. Add a new Student");
                            Console.WriteLine("3. Log a cheating incident");
                            Console.WriteLine("4. Back");

                            int adminChoice = int.Parse(Console.ReadLine());

                            switch (adminChoice)
                            {
                                case 1:
                                    Console.Clear();
                                    Console.Write("Enter new Admin Username: ");
                                    string newAdminUsername = Console.ReadLine();
                                    Console.Write("Enter Password: ");
                                    string newAdminPassword = Console.ReadLine();
                                    IAddUser userStudent = new User();
                                    userStudent.AddAdmin(newAdminUsername, newAdminPassword);
                                    Console.WriteLine("New Admin added successfully.");
                                    break;

                                case 2:
                                    Console.Clear();
                                    Console.Write("Enter new Student Username: ");
                                    string studentUsernameNew = Console.ReadLine();
                                    Console.Write("Enter Password: ");
                                    string studentPasswordNew = Console.ReadLine();
                                    IAddUser userAdmin = new User();
                                    userAdmin.AddStudent(studentUsernameNew, studentPasswordNew);
                                    Console.WriteLine("Student added successfully.");
                                    break;

                                case 3:
                                    Console.Clear();
                                    Console.Write("Enter Student Name: ");
                                    string cheatingStudentName = Console.ReadLine();
                                    Console.Write("Enter Cheating Apps: ");
                                    string cheatingApps = Console.ReadLine();

                                    try
                                    {
                                        saved.LogCheatingIncidentSeparate(cheatingStudentName, cheatingApps);
                                        saved.SaveStudentName(cheatingStudentName);
                                        Console.ForegroundColor = ConsoleColor.Red;

                                        Console.WriteLine("Cheating incident logged successfully in both textfiles");
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine($"An error occurred: {ex.Message}");
                                    }
                                    Console.WriteLine("Press any key to continue...");
                                    Console.ReadKey();

                                    Console.WriteLine("Cheating incident logged successfully.");
                                    break;

                                case 4:
                                    // Back to main menu
                                    break;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid login credentials.");
                        }
                        break;

                    case 2:
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Welcome Student");
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("Enter Username: ");
                            string studentUsername = Console.ReadLine();
                            Console.Write("Enter Password: ");
                            string studentPassword = Console.ReadLine();
                            //ARMAND
                            UserLogin student = new Student(studentUsername);
                            List<UserLogin> userss = new List<UserLogin> { student };

                            foreach (var user in userss)
                            {
                                user.Login();
                                user.PerformAction();
                            }

                            IUserValidator userValidatorStudent = new UserValidator();
                            if (userValidatorStudent.ValidateLoginStudent(studentUsername, studentPassword))
                            {
                                Console.WriteLine("Login successful!");

                                // Set the current student username
                                CurrentUserInfo.StudentUsername = studentUsername;

                                RefreshTempDB.RefreshTempDBLogic();
                            }
                            else
                            {
                                Console.WriteLine("Invalid login credentials.");
                            }
                            // Pause to allow the user to see the output
                            Console.WriteLine("Press any key to return to the main menu...");
                            Console.ReadKey();
                            break;
                        }

                    case 99:
                        // Exit
                        Environment.Exit(0);
                        break;
                }
            }

         
        }
    }
}
