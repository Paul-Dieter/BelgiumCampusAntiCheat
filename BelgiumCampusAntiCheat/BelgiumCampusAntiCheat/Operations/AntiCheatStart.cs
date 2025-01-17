using System;

namespace BelgiumCampusAntiCheat.Operations
{
    internal class AntiCheatStart
    {
        // Program start temp text.
        public static void AntiCheatProgramStart()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(@"
███    ███  ██████  ███    ██ ██ ████████  ██████  ██████  ██ ███    ██  ██████      ██    ██ ███████ ███████ ██████  
████  ████ ██    ██ ████   ██ ██    ██    ██    ██ ██   ██ ██ ████   ██ ██           ██    ██ ██      ██      ██   ██");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(@"██ ████ ██ ██    ██ ██ ██  ██ ██    ██    ██    ██ ██████  ██ ██ ██  ██ ██   ███     ██    ██ ███████ █████   ██████");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(@"██  ██  ██ ██    ██ ██  ██ ██ ██    ██    ██    ██ ██   ██ ██ ██  ██ ██ ██    ██     ██    ██      ██ ██      ██   ██ 
██      ██  ██████  ██   ████ ██    ██     ██████  ██   ██ ██ ██   ████  ██████       ██████  ███████ ███████ ██   ██");





            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"Once started, this program will actively monitor Visual Studio Community background applications and user web traffic.");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Important:");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. Ensure this program remains running while completing your test.");
            Console.WriteLine("2. Closing the program during the test or failing to launch it properly will result in an immediate score of 0.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;

            
            Console.ForegroundColor = ConsoleColor.White;

            // Check for history file path
            if (string.IsNullOrEmpty(AccessAndCreateTempDB.HistoryFilePath))
            {
                Console.WriteLine("Browser history file has not been found.");
                return;
            }
        }
    }
}
