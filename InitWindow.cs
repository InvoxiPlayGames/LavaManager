using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavaManager
{
    public partial class InitWindow : Form
    {
        List<DriveInfo> foundDrives = new List<DriveInfo>();
        string detectedDolphinPath = "";

        public InitWindow()
        {
            InitializeComponent();
        }

        private void InitWindow_Load(object sender, EventArgs e)
        {
            RefreshDriveList();
        }

        private void refreshDriveListButton_Click(object sender, EventArgs e)
        {
            RefreshDriveList();
        }

        private void DetectDolphinPath()
        {
            // TODO: check dolphin config files to see if Wii SD path is set to another place
            string checkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Dolphin Emulator\\Load\\WiiSDSync\\";
            if (Directory.Exists(checkingFolder))
            {
                detectedDolphinPath = checkingFolder;
                useDolphinButton.Enabled = true;
                detectedDolphinDir.Text = "Dolphin detected!";
                detectedDolphinDir.Enabled = true;
                fullPathTooltip.SetToolTip(detectedDolphinDir, detectedDolphinPath);
            }
        }

        private void RefreshDriveList()
        {
            Program.pw.ShowReal();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                try
                {
                    Console.WriteLine(drive);
                    if (drive.IsReady && drive.DriveFormat == "FAT32" &&
                        (Directory.Exists($"{drive.Name}apps") ||
                        Directory.Exists($"{drive.Name}rb3")))
                    {
                        foundDrives.Add(drive);
                        sdCardBox.Items.Add($"{drive.Name} - {(drive.VolumeLabel == "" ? "NO NAME" : drive.VolumeLabel)} ({Utilities.BytesToString(drive.AvailableFreeSpace, 2)} free)");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Could not check {drive.Name}");
                }
            }
            if (foundDrives.Count == 0)
            {
                sdCardBox.Enabled = false;
                loadSDButton.Text = "No SD cards found!";
                loadSDButton.Enabled = false;
            } else
            {
                sdCardBox.SelectedIndex = 0;
            }
            DetectDolphinPath();
            Program.pw.Hide();
        }

        private void loadSDButton_Click(object sender, EventArgs e)
        {
            if (sdCardBox.SelectedIndex == -1)
            {
                MessageBox.Show("Please select an SD card.");
                return;
            }
            Hide();
            string driveLetter = foundDrives[sdCardBox.SelectedIndex].Name;
            Directory.CreateDirectory(driveLetter + "rb3\\");
            new MainWindow(driveLetter + "rb3\\").ShowDialog();
            Application.Exit();
        }

        private void githubLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/InvoxiPlayGames/LavaManager");
        }

        private void selectFolderButton_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = "Select the \"rb3\" folder to use";
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                if (!dialog.FileName.ToLower().EndsWith("\\rb3"))
                {
                    MessageBox.Show("The folder you have selected is not named 'rb3'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } else
                {
                    Hide();
                    new MainWindow(dialog.FileName + "\\").ShowDialog();
                    Application.Exit();
                }
            }
        }

        private void useDolphinButton_Click(object sender, EventArgs e)
        {
            Hide();
            Directory.CreateDirectory(detectedDolphinPath + "rb3\\");
            new MainWindow(detectedDolphinPath + "rb3\\").ShowDialog();
            Application.Exit();
        }
    }
}
