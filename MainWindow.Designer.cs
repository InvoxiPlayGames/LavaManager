namespace LavaManager
{
    partial class MainWindow
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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reloadListingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveSongCacheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.finaliseAndExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.songList = new System.Windows.Forms.ListView();
            this.songNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.artistNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.shortNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sizeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.openSTFSDialog = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(625, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importToolStripMenuItem,
            this.reloadListingToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveSongCacheToolStripMenuItem,
            this.finaliseAndExitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.fileToolStripMenuItem.Text = "Manage";
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importToolStripMenuItem.Text = "Import Song";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // reloadListingToolStripMenuItem
            // 
            this.reloadListingToolStripMenuItem.Name = "reloadListingToolStripMenuItem";
            this.reloadListingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reloadListingToolStripMenuItem.Text = "Reload Song List";
            this.reloadListingToolStripMenuItem.Click += new System.EventHandler(this.reloadListingToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // saveSongCacheToolStripMenuItem
            // 
            this.saveSongCacheToolStripMenuItem.Name = "saveSongCacheToolStripMenuItem";
            this.saveSongCacheToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.saveSongCacheToolStripMenuItem.Text = "Finalise";
            this.saveSongCacheToolStripMenuItem.Click += new System.EventHandler(this.saveSongCacheToolStripMenuItem_Click);
            // 
            // finaliseAndExitToolStripMenuItem
            // 
            this.finaliseAndExitToolStripMenuItem.Enabled = false;
            this.finaliseAndExitToolStripMenuItem.Name = "finaliseAndExitToolStripMenuItem";
            this.finaliseAndExitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.finaliseAndExitToolStripMenuItem.Text = "Finalise and Exit";
            this.finaliseAndExitToolStripMenuItem.Visible = false;
            this.finaliseAndExitToolStripMenuItem.Click += new System.EventHandler(this.finaliseAndExitToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar,
            this.statusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 347);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(625, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(39, 17);
            this.statusLabel.Text = "Status";
            // 
            // songList
            // 
            this.songList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.songNameHeader,
            this.artistNameHeader,
            this.shortNameHeader,
            this.sizeHeader});
            this.songList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.songList.HideSelection = false;
            this.songList.Location = new System.Drawing.Point(0, 24);
            this.songList.Name = "songList";
            this.songList.Size = new System.Drawing.Size(625, 323);
            this.songList.TabIndex = 2;
            this.songList.UseCompatibleStateImageBehavior = false;
            this.songList.View = System.Windows.Forms.View.Details;
            // 
            // songNameHeader
            // 
            this.songNameHeader.Text = "Song Name";
            this.songNameHeader.Width = 258;
            // 
            // artistNameHeader
            // 
            this.artistNameHeader.Text = "Artist";
            this.artistNameHeader.Width = 135;
            // 
            // shortNameHeader
            // 
            this.shortNameHeader.Text = "Song ID";
            this.shortNameHeader.Width = 88;
            // 
            // sizeHeader
            // 
            this.sizeHeader.Text = "Size";
            this.sizeHeader.Width = 61;
            // 
            // openSTFSDialog
            // 
            this.openSTFSDialog.Filter = "STFS (CON/LIVE) Packages|*";
            this.openSTFSDialog.Multiselect = true;
            this.openSTFSDialog.Title = "Select a CON/LIVE file to import";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 369);
            this.Controls.Add(this.songList);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MainWindow";
            this.Text = "LavaManager";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reloadListingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem saveSongCacheToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem finaliseAndExitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.ListView songList;
        private System.Windows.Forms.ColumnHeader songNameHeader;
        private System.Windows.Forms.ColumnHeader artistNameHeader;
        private System.Windows.Forms.ColumnHeader shortNameHeader;
        private System.Windows.Forms.ColumnHeader sizeHeader;
        private System.Windows.Forms.OpenFileDialog openSTFSDialog;
    }
}