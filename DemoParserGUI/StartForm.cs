using Microsoft.Win32;
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

namespace DemoParserGUI
{
    public partial class StartForm : Form
    {
        private string steamDir = "";
        public StartForm()
        {
            InitializeComponent();
        }

        private void StartForm_Load(object sender, EventArgs e)
        {
            steamDir = getSteamDirectory();
            string totalDir = steamDir + "/SteamApps/common/Counter-Strike Global Offensive/csgo/replays";
            if (Directory.Exists(totalDir))
            {
                replayFolderTextBox.Text = totalDir;
            }
            else
            {
                replayFolderTextBox.Text = "Select CSGO replay directory";
            }
        }

        private string getSteamDirectory()
        {
            RegistryKey regKey = Registry.CurrentUser;
            regKey = regKey.OpenSubKey(@"Software\Valve\Steam");

            if (regKey != null)
            {
                return regKey.GetValue("SteamPath").ToString();
            }
            return "C:/program files (x86)/steam"; //yaay hardcoded
        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                replayFolderTextBox.Text = fbd.SelectedPath;
            }
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            string selectedPath = replayFolderTextBox.Text;
            if (!Directory.Exists(selectedPath))
            {
                MessageBox.Show("This folder does not exist!");
                return;
            }
            ParsedReplaysForm p = new ParsedReplaysForm(selectedPath);
            p.Show();
        }
    }
}
