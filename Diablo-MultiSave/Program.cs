using Diablo_MultiSave.Core;
using System;
using System.Windows.Forms;

namespace Diablo_MultiSave
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string diabloPath = DiabloPath.TryFindPath();

            if (diabloPath == null)
            {
                // TODO: Open Dialog for Path Input if not found
            }

            // var saveFileWatcher = new SaveFileWatcher(diabloPath);

            Console.WriteLine(diabloPath);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
