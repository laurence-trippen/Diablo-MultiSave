﻿using System;
using System.Collections.Generic;
using System.IO;

namespace Diablo_MultiSave.Core
{
    /// <summary>
    /// Stay & Listen for a while... (For SaveGame changes)
    /// </summary>
    internal class SaveFileWatcher
    {
        public SaveFileWatcher(string diabloPath)
        {
            using (var fileWatcher = new FileSystemWatcher(diabloPath))
            {

            }
        }
    }
}
