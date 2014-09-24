using CSGODemoParser.Demo;
using CSGODemoParser.Demo.Parser;
using CSGODemoParser.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoParserGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<long> lengths = new List<long>();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

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
        }
    }
}
