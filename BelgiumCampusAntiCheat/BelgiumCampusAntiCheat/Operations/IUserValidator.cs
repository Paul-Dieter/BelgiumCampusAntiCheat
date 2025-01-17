using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgiumCampusAntiCheat.Operations
{
    internal interface IUserValidator
    {
        bool ValidateLoginAdmin(string username, string password);
        bool ValidateLoginStudent(string username, string password);
    }
    public class UserValidator : IUserValidator
    {
        private string loginFilePathAdmin = @"G:\My Drive\Admin\AdminLogin\loginInfoAdmin.txt"; // Example file path
        private string loginFilePathStudent = @"G:\My Drive\Student\StudentLoginInfo\loginInfoStudent.txt"; // Example file path
        
        public bool ValidateLoginAdmin(string username, string password)
        {
            if (File.Exists(loginFilePathAdmin))
            {
                string[] lines = File.ReadAllLines(loginFilePathAdmin);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts[0] == username && parts[1] == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ValidateLoginStudent(string username, string password)
        {
            if (File.Exists(loginFilePathStudent))
            {
                string[] lines = File.ReadAllLines(loginFilePathStudent);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts[0] == username && parts[1] == password)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
