using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BelgiumCampusAntiCheat.Operations
{
    internal class Saved
    {

        private string studentFilePath = @"G:\My Drive\Student\AllCheatingStudents\students.txt";
        private string cheatLogFilePath = @"G:\My Drive\Student\StudentCheatingLogs\cheatingLog.txt";
        private string loginFilePathAdmin = @"G:\My Drive\Admin\AdminLogin\loginInfoAdmin.txt";
        private string loginFilePathStudent = @"G:\My Drive\Student\StudentLoginInfo\loginInfoStudent.txt";

        // Constructor to create login file with predefined admins
        public Saved()
        {
            if (!File.Exists(loginFilePathAdmin))
            {
                using (StreamWriter sw = File.CreateText(loginFilePathAdmin))
                {
                    sw.WriteLine("admin1,password1");
                }
            }
        }

        //method to write each student own textfile
        public void LogCheatingIncidentSeparate(string studentName, string cheatingApps)
        {
            try
            {
                string directoryPath = @"G:\My Drive\Student\CheatingIncident";
                string fileName = $"{studentName}.txt";

                string fullPath = Path.Combine(directoryPath, fileName);
                using (StreamWriter sw = new StreamWriter(fullPath, true))
                {
                    sw.WriteLine($"Cheating incident logged on {DateTime.Now}: ");
                    sw.WriteLine($"{cheatingApps}");
                }
                Console.WriteLine("File created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public string ReadStudentNameSeparate(string studentName)
        {
            string studentFilePathSeparateread = $"{studentName}.txt";
            if (File.Exists(studentFilePathSeparateread))
            {
                using (StreamReader sr = File.OpenText(studentFilePathSeparateread))
                {
                    // Read the entire content of the file
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return "Student file does not exist.";
            }
        }

        // Method to save the student's name
        public void SaveStudentName(string studentName)
        {
            using (StreamWriter sw = new StreamWriter(studentFilePath, true))
            {
                sw.WriteLine(studentName);
            }
        }

        // Method to read the student's name from the file
        public string ReadStudentName()
        {
            if (File.Exists(studentFilePath))
            {
                using (StreamReader sr = File.OpenText(studentFilePath))
                {
                    // Read the entire content of the file
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return "Student file does not exist.";
            }
        }

        // Method to log cheating incidents
        public void LogCheatingIncident(string studentName, string cheatingApps)
        {
            using (StreamWriter sw = new StreamWriter(cheatLogFilePath, true))
            {
                sw.WriteLine($"{studentName} was caught cheating. Used apps: {cheatingApps}");
            }
        }
    }
}
