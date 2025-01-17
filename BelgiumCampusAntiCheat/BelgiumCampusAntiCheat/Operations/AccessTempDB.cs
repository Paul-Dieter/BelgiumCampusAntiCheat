using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace BelgiumCampusAntiCheat.Operations
{
    internal class AccessTempDB
    {
        public static DateTime _launchTime = DateTime.UtcNow;
        public static void AccessDB()
        {
            try
            {
                var connection = new SQLiteConnection($"Data Source={AccessAndCreateTempDB.TempHistoryFilePath};Version=3;Read Only=True;");
                connection.Open();
                try
                {
                    PrintInfo(connection, CurrentUserInfo.StudentUsername); // path where to save data
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Access DB error: {ex.Message}");
            }
        }

        public static void PrintInfo(SQLiteConnection connection, string studentName)
        {
            string directoryPath = @"G:\My Drive\Student\StudentHistoryLogs";
            string fileName = $"{studentName}_loggedWebs.txt";

            string fullPath = Path.Combine(directoryPath, fileName);

            string query = "SELECT id, url, title, last_visit_time FROM urls WHERE last_visit_time > @launchTime AND url NOT LIKE '%&ia=web%'";
            try
            {
                var command = new SQLiteCommand(query, connection);
                long launchTimeWebKit = (long)((_launchTime - new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds * 1000);
                command.Parameters.AddWithValue("@launchTime", launchTimeWebKit);
                var reader = command.ExecuteReader();
                var results = new List<(int id, string url, string title, DateTime lastVisitTime)>();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["id"]);
                    string url = reader["url"].ToString();
                    string title = reader["title"].ToString();
                    long lastVisitTimeWebKit = Convert.ToInt64(reader["last_visit_time"]);
                    DateTime lastVisitTime = new DateTime(1601, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(lastVisitTimeWebKit / 1000);
                    results.Add((id, url, title, lastVisitTime));
                }
                reader.Close();
                command.Dispose();

                // Open a StreamWriter to write the results to the file named after the student
                using (StreamWriter sw = new StreamWriter(fullPath, false)) 
                {
                    foreach (var result in results)
                    {
                        string output = $"ID: {result.id}\nURL: {result.url}\nWhat was searched: {result.title}\nLast Visit Time (UTC): {result.lastVisitTime:yyyy/MM/dd HH:mm:ss}\n";
                        // Write to the console
                        Console.WriteLine(output);
                        // Write to the file
                        sw.WriteLine(output);
                        
                    }
                    if (results.Count == 0)
                    {
                        string noEntriesMessage = "No new entries since the application launch.";
                        Console.WriteLine(noEntriesMessage);
                        sw.WriteLine(noEntriesMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving data: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
            }
        }

    }
}