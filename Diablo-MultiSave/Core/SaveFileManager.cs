using System;
using System.IO;
using System.Security.Permissions;

namespace Diablo_MultiSave.Core
{
    /// <summary>
    /// Archives multiple save-files & handles current one.
    /// </summary>
    internal class SaveFileManager
    {
        private FileSystemWatcher fsWatcher;

        public SaveFileManager(string diabloPath)
        {
            // Extract folder from full exe path
            string diabloDirectory = new FileInfo(diabloPath).Directory.FullName;

            FindSavesAndWatch(diabloDirectory);
            ObserveForNewSaves(diabloDirectory);
        }

        private void FindSavesAndWatch(string diabloDirectory)
        {
            string[] saves = Directory.GetFiles(diabloDirectory, "*.sv");

            foreach (string save in saves)
            {
                Console.WriteLine(save);
            }

            // TODO: If no save file exists watch directory for creation
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private void ObserveForNewSaves(string diabloDirectory)
        {
            try
            {

                // Memory leak... Add using-block
                fsWatcher = new FileSystemWatcher(diabloDirectory);

                fsWatcher.IncludeSubdirectories = true;
                fsWatcher.NotifyFilter = NotifyFilters.FileName;
                fsWatcher.Filter = "*.sv";
                fsWatcher.Created += OnSaveFileCreated;
                fsWatcher.EnableRaisingEvents = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void OnSaveFileCreated(object sender, FileSystemEventArgs e)
        {
            string value = $"Created: {e.FullPath}";
            Console.WriteLine(value);
        }

        ~SaveFileManager()
        {
            Console.WriteLine("SaveFileManager Died");
        }
    }
}
