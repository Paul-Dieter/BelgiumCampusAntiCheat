using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelgiumCampusAntiCheat.Operations
{
    internal interface IAddUser
    {
        void AddAdmin(string adminName, string password);
        void AddStudent(string studentName, string studentPassword);
    }

    public class User : IAddUser
    {
        private string loginFilePathAdmin = @"G:\My Drive\Admin\AdminLogin\loginInfoAdmin.txt"; // Example file path
        private string loginFilePathStudent = @"G:\My Drive\Student\StudentLoginInfo\loginInfoStudent.txt"; // Example file path

        public void AddAdmin(string adminName, string password)
        {
            using (StreamWriter sw = new StreamWriter(loginFilePathAdmin, true))
            {
                sw.WriteLine($"{adminName},{password}");
            }
        }

        public void AddStudent(string studentName, string studentPassword)
        {
            using (StreamWriter sw = new StreamWriter(loginFilePathStudent, true))
            {
                sw.WriteLine($"{studentName},{studentPassword}");
            }
        }
    }
}
