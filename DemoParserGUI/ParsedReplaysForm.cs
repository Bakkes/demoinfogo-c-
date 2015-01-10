using CSGODemoParser.Demo;
using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoParserGUI
{
    public partial class ParsedReplaysForm : Form
    {
        ToolStripMenuItem toolStrip;

        int filesParsed = 0;
        private string[] demoFiles;
        private string replayFolder;
        private Dictionary<int, SteamUser> users = new Dictionary<int, SteamUser>();
        public ParsedReplaysForm(string replayFolder)
        {
            InitializeComponent();
            this.replayFolder = replayFolder;
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
            playerListStrip.Opening += playerListStrip_Opening;
            toolStrip = new ToolStripMenuItem("TEST");
            toolStrip.Text = "TEST";
            playerListStrip.Items.Add(toolStrip);
            //toolStrip.Click += new EventHandler(Item_Click);
            //toolStrip.CheckOnClick = true;
            demoFiles = Directory.GetFiles(replayFolder, "*.dem");
            refresh();
        }

        void playerListStrip_Opening(object sender, CancelEventArgs e)
        {
            toolStrip.DropDownItems.Clear();
            foreach (String demoFile in users[listView1.SelectedIndices[0]].DemoFiles)
            {
                toolStrip.DropDownItems.Add(demoFile);
            }
        }

        private void RankUpdateThread()
        {
            foreach (string file in demoFiles)
            {
                QuickParser qp = new QuickParser(new CSGODemoReader(file));
                DemoFile d = new DemoFile(qp);
                d.Parse();
                if (qp.RankUpdate != null && qp.RankUpdate.RankUpdateList != null)
                {
                    foreach (var update in qp.RankUpdate.RankUpdateList)
                    {
                        if (!users.ContainsKey(update.AccountId))
                        {
                            users.Add(update.AccountId, new SteamUser()
                            {
                                AccountID32 = update.AccountId,
                                Rank = update.RankNew,
                                WinCount = update.NumWins
                            });
                        }
                        else if (update.NumWins > users[update.AccountId].WinCount
                            || update.RankNew < users[update.AccountId].Rank) //played with person again in a later match (higher wincount or derank)
                        {
                            users[update.AccountId].WinCount = update.NumWins;
                            users[update.AccountId].Rank = update.RankNew;
                        } //otherwise its an older match

                        users[update.AccountId].DemoFiles.Add(file);
                    }
                }
                filesParsed++;
                progressLabel.Invoke(() => {
                    progressLabel.Text = "Parsing demo " + filesParsed + "/" + demoFiles.Length;
                });
                parsingProgressBar.Invoke(() =>
                {
                    parsingProgressBar.Value = filesParsed;
                });

            }

            parsingProgressBar.Invoke(() =>
            {
                progressLabel.Text = "Done parsing!";
            });

            listView1.Invoke(() =>
                {
                    foreach (SteamUser user in users.Values)
                    {
                        ListViewItem lvi = new ListViewItem(new string[] { "-1", user.AccountID32.ToString(), user.AccountID64.ToString(), user.WinCount.ToString(), user.Rank.ToString(), "UNKNOWN" });
                        listView1.Items.Add(lvi);
                    }
                });
            
        }

        private void refresh()
        {
            parsingProgressBar.Value = 0;
            parsingProgressBar.Maximum = demoFiles.Length;
            Thread parseThread = new Thread(new ThreadStart(RankUpdateThread));
            parseThread.Start();
        }

        //speedtest
        /*
         *         List<long> lengths = new List<long>();
        private void button1_Click(object sender, EventArgs e)
        {
            lengths.Clear();
            int iterations = 0;
            long lowest = Int32.MaxValue, highest = -Int32.MaxValue;
            long totalmselapsed = 0;
            for (int i = 0; i < 50; i++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                DemoFile d = new DemoFile(new QuickParser(new CSGODemoReader("C:\\Program Files (x86)\\Steam\\SteamApps\\common\\Counter-Strike Global Offensive\\csgo\\replays\\match730_003031143853852721165_1454902835_152.dem")));
                
                sw.Stop();
                iterations++;
                totalmselapsed += sw.ElapsedMilliseconds;
                if (sw.ElapsedMilliseconds > highest)
                {
                    highest = sw.ElapsedMilliseconds;
                }

                if (sw.ElapsedMilliseconds < lowest)
                {
                    lowest = sw.ElapsedMilliseconds;
                }
                lengths.Add(sw.ElapsedMilliseconds);
            }
            listBox1.DataSource = lengths;
            listBox1.Update();
            MessageBox.Show(iterations + " iterations, avg: " + (totalmselapsed / iterations) + ", high: " + highest + ", low: " + lowest);
        }*/

        
    }
    //http://stackoverflow.com/questions/8971561/c-sharp-how-to-change-value-of-a-progress-bar-in-a-secondary-thread
    public static class ControlExtensions
    {
        public static void Invoke(this Control control, Action action)
        {
            if (control.InvokeRequired) control.Invoke(new MethodInvoker(action), null);
            else action.Invoke();
        }
    }
    
}
