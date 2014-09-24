namespace DemoParserGUI
{
    partial class StartForm
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
            this.replayFolderTextBox = new System.Windows.Forms.TextBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.goButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // replayFolderTextBox
            // 
            this.replayFolderTextBox.Location = new System.Drawing.Point(13, 4);
            this.replayFolderTextBox.Name = "replayFolderTextBox";
            this.replayFolderTextBox.Size = new System.Drawing.Size(389, 20);
            this.replayFolderTextBox.TabIndex = 0;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(408, 4);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(62, 20);
            this.browseButton.TabIndex = 1;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(13, 30);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(454, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Gooooooooooooo";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 55);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.replayFolderTextBox);
            this.Name = "StartForm";
            this.Text = "Select CSGO replay directory";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox replayFolderTextBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button goButton;
    }
}