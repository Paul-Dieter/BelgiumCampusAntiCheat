using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgiumCampusAntiCheat.Operations
{
    internal class CheatCheck
    {
        private static string _studentName = CurrentUserInfo.StudentUsername;


        public static void TextReadForCheater()
        {
            string directoryPath = @"G:\My Drive\Student\StudentHistoryLogs";
            string fileName = $"{_studentName}_loggedWebs.txt";
            string fullPath = Path.Combine(directoryPath, fileName);


            // List of words to detect in our studentsLog
            List<string> wordsToDetect = new List<string> { "chatgpt", "whatsapp", "ai" };



            // Detect the words in the file
            try
            {
                using (StreamReader reader = new StreamReader(fullPath))
                {
                    string lineVal; // value of our readline
                    int lineNumber = 0;

                    while ((lineVal = reader.ReadLine()) != null)
                    {
                        lineNumber++;

                        // Split the line into words
                        string[] words = lineVal.Split(new[] { ' ', ',', '.', ';', ':', '-', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

                        foreach (string word in words)
                        {
                            // Check if the word is in the list of words to detect
                            if (wordsToDetect.Contains(word.ToLower()))
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("You have cheated. Test Result = 0");
                                Saved SaveCheaterName = new Saved();
                                SaveCheaterName.SaveStudentName(_studentName);
                                Console.ForegroundColor = ConsoleColor.White;
                                Environment.Exit(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}

