using System;
using System.Diagnostics;
using System.IO;

namespace Diablo_MultiSave.Core
{
    /// <summary>
    /// Finding Diablos Install Location
    /// </summary>
    internal static class DiabloPath
    {
        public const string DiabloExe = "Diablo.exe";

        public const string GogDefaultPath = @"c:/GOG Games/Diablo/dx";

        public static readonly string GogGalaxyDefaultPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), @"GOG Galaxy/Games/Diablo/dx");

        public static string TryFindPath()
        {
            string path = GetPathByRunningGame();

            if (path != null) return path;

            path = GetPathByKnownLocations();

            if (path != null) return path;

            // Couldn't find the path :-(
            return null;
        }

        private static string GetPathByRunningGame()
        {
            Process[] processes = Process.GetProcessesByName("diablo");

            if (processes.Length <= 0) return null;

#if DEBUG
            foreach (Process process in processes)
            {
                Console.WriteLine(process.MainModule.FileName);
            }
#endif

            return processes[0].MainModule.FileName;
        }

        private static string GetPathByKnownLocations()
        {
            string path = Path.Combine(GogDefaultPath, DiabloExe);

            if (File.Exists(path)) return path;

            path = Path.Combine(GogGalaxyDefaultPath, DiabloExe);

            if (File.Exists(path)) return path;

            // Couldn't find the path :-(
            return null;
        }
    }
}
