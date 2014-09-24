namespace DemoParserGUI
{
    partial class ParsedReplaysForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.playerListStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openSteamProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewDemoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parsingProgressBar = new System.Windows.Forms.ProgressBar();
            this.progressLabel = new System.Windows.Forms.Label();
            this.playerListStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.ContextMenuStrip = this.playerListStrip;
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new System.Drawing.Point(-1, 22);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(667, 258);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "AccountID";
            this.columnHeader1.Width = 140;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "ConvertedID";
            this.columnHeader2.Width = 160;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Wins";
            this.columnHeader3.Width = 80;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Rank";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-1, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Players you recently played with:";
            // 
            // playerListStrip
            // 
            this.playerListStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSteamProfileToolStripMenuItem,
            this.viewDemoToolStripMenuItem});
            this.playerListStrip.Name = "playerListStrip";
            this.playerListStrip.Size = new System.Drawing.Size(176, 48);
            // 
            // openSteamProfileToolStripMenuItem
            // 
            this.openSteamProfileToolStripMenuItem.Name = "openSteamProfileToolStripMenuItem";
            this.openSteamProfileToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.openSteamProfileToolStripMenuItem.Text = "Open steam profile";
            // 
            // viewDemoToolStripMenuItem
            // 
            this.viewDemoToolStripMenuItem.Name = "viewDemoToolStripMenuItem";
            this.viewDemoToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.viewDemoToolStripMenuItem.Text = "View demo";
            // 
            // parsingProgressBar
            // 
            this.parsingProgressBar.Location = new System.Drawing.Point(12, 286);
            this.parsingProgressBar.Name = "parsingProgressBar";
            this.parsingProgressBar.Size = new System.Drawing.Size(643, 23);
            this.parsingProgressBar.TabIndex = 3;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Location = new System.Drawing.Point(13, 316);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(58, 13);
            this.progressLabel.TabIndex = 4;
            this.progressLabel.Text = "Initializing..";
            // 
            // ParsedReplaysForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 357);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.parsingProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Name = "ParsedReplaysForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.playerListStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip playerListStrip;
        private System.Windows.Forms.ToolStripMenuItem openSteamProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewDemoToolStripMenuItem;
        private System.Windows.Forms.ProgressBar parsingProgressBar;
        private System.Windows.Forms.Label progressLabel;

    }
}

