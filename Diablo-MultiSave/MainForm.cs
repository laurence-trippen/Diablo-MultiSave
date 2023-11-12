using Diablo_MultiSave.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Diablo_MultiSave
{
    public partial class MainForm : Form
    {
        private ToolStripStatusLabel statusLabel = null;
        private SaveFileManager saveFileManager = null;

        public MainForm()
        {
            InitializeComponent();

            statusLabel = (ToolStripStatusLabel)statusStrip.Items[0];
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            string savedPath = Properties.Settings.Default.DiabloPath;

            if (savedPath != null && File.Exists(savedPath))
            {
                statusLabel.Text = $"Found path: {savedPath}";
                saveFileManager = new SaveFileManager(savedPath);
                return;
            }


            string diabloPath = DiabloPath.TryFindPath();

            if (diabloPath == null)
            {
                var fileDialog = new OpenFileDialog
                {
                    Title = "Locate Diablo inside (dx) folder",
                    InitialDirectory = @"c:\",
                    DefaultExt = "exe",

                    Filter = "Diablo Exe (exe)|*.exe",
                    FilterIndex = 1,

                    CheckFileExists = true,
                    CheckPathExists = true,
                };
                
                DialogResult result = fileDialog.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Console.WriteLine(fileDialog.FileName);

                    Properties.Settings.Default.DiabloPath = fileDialog.FileName;
                    Properties.Settings.Default.Save();

                    statusLabel.Text = $"Set Path: ${fileDialog.FileName}";

                    saveFileManager = new SaveFileManager(fileDialog.FileName);
                }
                else
                {
                    MessageBox.Show("You have to specify Diablo in-order to use the app.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    Application.Exit();
                }
            }
        }
    }
}
