using BelgiumCampusAntiCheat.Operations;
using System;
using System.Threading;

internal class RefreshTempDB : RefresherCounter
{
    private static readonly TimeSpan refreshInterval = TimeSpan.FromSeconds(4);

    private static Thread refreshThread;

    public static void RefreshTempDBLogic()
    {
        refreshThread = new Thread(RefreshLogic)
        {
            IsBackground = true // Make the thread a background thread so it doesn't prevent application exit
        };
        refreshThread.Start();
    }

    // Logic that the thread will run
    private static void RefreshLogic()
    {
        while (true) // Run indefinitely (or use a condition to break out of the loop if needed)
        {
            TimerCallback(); // Call the method that contains the refresh logic
            Thread.Sleep(refreshInterval); // Sleep for the specified interval
        }
    }

    // The logic to be executed at each interval
    private static void TimerCallback()
    {
        Console.Clear();
        Console.WriteLine("\x1b[3J");

        AntiCheatStart.AntiCheatProgramStart();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine("");
        Console.WriteLine("ADMIN VIEW FOR DEMO PURPOSES ONLY");
        Console.WriteLine("");
        Console.ForegroundColor = ConsoleColor.White;
        RefresherCounter.InvokeEventHandler();
        Console.WriteLine($"Application launch time (UTC): {AccessTempDB._launchTime}");

        AccessAndCreateTempDB.CopyDataBaseTempFile();
        AccessTempDB.AccessDB();
        CheatCheck.TextReadForCheater();
        
    }
}