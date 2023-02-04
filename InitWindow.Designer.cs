namespace LavaManager
{
    partial class InitWindow
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
            this.githubLink = new System.Windows.Forms.LinkLabel();
            this.loadSDButton = new System.Windows.Forms.Button();
            this.sdCardBox = new System.Windows.Forms.ComboBox();
            this.deviceLabel = new System.Windows.Forms.Label();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.refreshDriveListButton = new System.Windows.Forms.Button();
            this.useDolphinButton = new System.Windows.Forms.Button();
            this.detectedDolphinDir = new System.Windows.Forms.Label();
            this.fullPathTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // githubLink
            // 
            this.githubLink.AutoSize = true;
            this.githubLink.Location = new System.Drawing.Point(11, 134);
            this.githubLink.Name = "githubLink";
            this.githubLink.Size = new System.Drawing.Size(252, 13);
            this.githubLink.TabIndex = 4;
            this.githubLink.TabStop = true;
            this.githubLink.Text = "https://github.com/InvoxiPlayGames/LavaManager";
            this.githubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.githubLink_LinkClicked);
            // 
            // loadSDButton
            // 
            this.loadSDButton.Location = new System.Drawing.Point(12, 36);
            this.loadSDButton.Name = "loadSDButton";
            this.loadSDButton.Size = new System.Drawing.Size(295, 34);
            this.loadSDButton.TabIndex = 5;
            this.loadSDButton.Text = "Load SD Card";
            this.loadSDButton.UseVisualStyleBackColor = true;
            this.loadSDButton.Click += new System.EventHandler(this.loadSDButton_Click);
            // 
            // sdCardBox
            // 
            this.sdCardBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sdCardBox.FormattingEnabled = true;
            this.sdCardBox.Location = new System.Drawing.Point(67, 9);
            this.sdCardBox.Name = "sdCardBox";
            this.sdCardBox.Size = new System.Drawing.Size(178, 21);
            this.sdCardBox.TabIndex = 8;
            // 
            // deviceLabel
            // 
            this.deviceLabel.AutoSize = true;
            this.deviceLabel.Location = new System.Drawing.Point(11, 12);
            this.deviceLabel.Name = "deviceLabel";
            this.deviceLabel.Size = new System.Drawing.Size(50, 13);
            this.deviceLabel.TabIndex = 9;
            this.deviceLabel.Text = "SD Card:";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.Location = new System.Drawing.Point(12, 75);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(295, 23);
            this.selectFolderButton.TabIndex = 10;
            this.selectFolderButton.Text = "Select Folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // refreshDriveListButton
            // 
            this.refreshDriveListButton.Location = new System.Drawing.Point(251, 8);
            this.refreshDriveListButton.Name = "refreshDriveListButton";
            this.refreshDriveListButton.Size = new System.Drawing.Size(56, 23);
            this.refreshDriveListButton.TabIndex = 11;
            this.refreshDriveListButton.Text = "Refresh";
            this.refreshDriveListButton.UseVisualStyleBackColor = true;
            this.refreshDriveListButton.Click += new System.EventHandler(this.refreshDriveListButton_Click);
            // 
            // useDolphinButton
            // 
            this.useDolphinButton.Enabled = false;
            this.useDolphinButton.Location = new System.Drawing.Point(12, 104);
            this.useDolphinButton.Name = "useDolphinButton";
            this.useDolphinButton.Size = new System.Drawing.Size(78, 23);
            this.useDolphinButton.TabIndex = 12;
            this.useDolphinButton.Text = "Use Dolphin";
            this.useDolphinButton.UseVisualStyleBackColor = true;
            this.useDolphinButton.Click += new System.EventHandler(this.useDolphinButton_Click);
            // 
            // detectedDolphinDir
            // 
            this.detectedDolphinDir.AutoSize = true;
            this.detectedDolphinDir.Enabled = false;
            this.detectedDolphinDir.Location = new System.Drawing.Point(96, 109);
            this.detectedDolphinDir.Name = "detectedDolphinDir";
            this.detectedDolphinDir.Size = new System.Drawing.Size(141, 13);
            this.detectedDolphinDir.TabIndex = 13;
            this.detectedDolphinDir.Text = "Couldn\'t detect Dolphin path";
            // 
            // InitWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 156);
            this.Controls.Add(this.detectedDolphinDir);
            this.Controls.Add(this.useDolphinButton);
            this.Controls.Add(this.refreshDriveListButton);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.deviceLabel);
            this.Controls.Add(this.sdCardBox);
            this.Controls.Add(this.loadSDButton);
            this.Controls.Add(this.githubLink);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InitWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LavaManager";
            this.Load += new System.EventHandler(this.InitWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.LinkLabel githubLink;
        private System.Windows.Forms.Button loadSDButton;
        private System.Windows.Forms.ComboBox sdCardBox;
        private System.Windows.Forms.Label deviceLabel;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.Button refreshDriveListButton;
        private System.Windows.Forms.Button useDolphinButton;
        private System.Windows.Forms.Label detectedDolphinDir;
        private System.Windows.Forms.ToolTip fullPathTooltip;
    }
}

