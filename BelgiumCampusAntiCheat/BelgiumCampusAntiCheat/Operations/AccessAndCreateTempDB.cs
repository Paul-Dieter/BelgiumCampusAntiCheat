using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BelgiumCampusAntiCheat.Operations
{
    internal class AccessAndCreateTempDB
    {
        private static string _historyFilePath = GetBrowserHistoryLocation();
        private static string _tempHistoryFilePath = Path.Combine(Path.GetTempPath(), "UserHistoryAccessCopy");

        public static string HistoryFilePath { get => _historyFilePath;}
        public static string TempHistoryFilePath { get => _tempHistoryFilePath;}

        //Function to get BrowserHistory file location
        private static string GetBrowserHistoryLocation()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string browserPath = Path.Combine(appDataPath, "Microsoft", "Edge", "User Data", "Default", "History");
            return File.Exists(browserPath) ? browserPath : null;
        }

        //copies the database file to a temp location to ensure it can be read and not locked by the process "browser" using the file.
        public static string CopyDataBaseTempFile()
        {
          
            try
            {
                //Overwrites copy if exists or creates new.
               
                File.Copy(HistoryFilePath, TempHistoryFilePath, true);
                return TempHistoryFilePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Copying file to temp folder: {ex.Message}");
                return null;
            }

        }
        

        //Deletes the tempDB file if it exists.
        //NOT IN USE: Currently new copy overides OLD copy leaving method for possible future USE.
        public static void DeleteDataBaseTempFile()
        {
            try
            {
                if (File.Exists(TempHistoryFilePath))
                {
                    File.Delete(TempHistoryFilePath);
                }

            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error deleting the TempBD file:{ex.Message}");
            }
           
        }

    }
}
