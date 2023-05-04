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
using DtxCS;
using DtxCS.DataTypes;
using LavaManager.Converters;
using LavaManager.Properties;

namespace LavaManager
{
    public partial class MainWindow : Form
    {
        string root;
        bool pending;

        public MainWindow(string rootDirectory)
        {
            root = rootDirectory;
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            Text = $"{root} - LavaManager";
            // make sure we have the directories we need
            Directory.CreateDirectory($"{root}lava/dta");
            Task.Run(LoadSongList);
        }

        private void ProgressUpdate(string progress, int done, int max)
        {
            Invoke((MethodInvoker)delegate
            {
                menuStrip.Enabled = false;
                statusLabel.Text = progress;
                progressBar.Style = max == 0 ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
                if (max != 0)
                {
                    progressBar.Value = done;
                    progressBar.Maximum = max;
                }
            });
        }
        private void ProgressDone(string done)
        {
            Invoke((MethodInvoker)delegate
            {
                menuStrip.Enabled = true;
                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Value = 0;
                progressBar.Maximum = 100;
                statusLabel.Text = done;
            });
        }

        private void UpdateSongList(List<ListViewItem> items)
        {
            Invoke((MethodInvoker)delegate
            {
                songList.Items.Clear();
                foreach (ListViewItem i in items) songList.Items.Add(i);
            });
        }

        private void LoadSongList()
        {
            ProgressUpdate("Loading song list...", 0, 0);
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            string[] dtaFiles = Directory.EnumerateFiles($"{root}lava/dta", "*.dta").ToArray();
            ProgressUpdate($"Loading song list (0/{dtaFiles.Length})...", 0, dtaFiles.Length);
            int doneFiles = 0;
            foreach (string file in dtaFiles)
            {
                string dta = File.ReadAllText(file);
                DataArray songs = DTX.FromDtaString(dta);
                for (int i = 0; i < songs.Count; i++)
                {
                    DataArray song = songs.Array(i);
                    //Console.WriteLine(song.ToString());
                    string shortname = song.Array("song").Array("name").String(1).Split('/')[1];
                    FileInfo moggInfo, bikInfo, wavInfo, midInfo, artInfo, miloInfo;
                    long songSize;
                    try
                    {
                        midInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.mid");
                        miloInfo = new FileInfo($"{root}songs/{shortname}/gen/{shortname}.milo_wii");
                        songSize = midInfo.Length + miloInfo.Length;
                    } catch (Exception)
                    {
                        continue; // if we don't have a mid or milo, ignore
                    }
                    try
                    {
                        moggInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.mogg");
                        songSize += moggInfo.Length;
                    }
                    catch (Exception)
                    {
                        try
                        {
                            bikInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.bik");
                            songSize += bikInfo.Length;
                        }
                        catch (Exception)
                        {
                            try
                            {
                                wavInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.wav");
                                songSize += wavInfo.Length;
                            }
                            catch (Exception) {
                                continue; // if we don't have a mogg, bik or wav, ignore
                            }
                        }
                    }

                    try
                    {
                        artInfo = new FileInfo($"{root}songs/{shortname}/gen/{shortname}_keep.png_wii");
                        songSize += artInfo.Length;
                    } catch (Exception) {} // we don't care if we have album art or not
                    
                    string songName = song.Array("name").String(1);
                    string songArtist = song.Array("artist").String(1);
                    listViewItems.Add(new ListViewItem(new string[] { songName, songArtist, shortname, Utilities.BytesToString(songSize) }));
                }
                doneFiles++;
                ProgressUpdate($"Loading song list ({doneFiles}/{dtaFiles.Length})...", doneFiles, dtaFiles.Length);
            }
            UpdateSongList(listViewItems);
            ProgressDone($"Loaded {listViewItems.Count} song{(listViewItems.Count > 1 ? "s" : "")}!");
        }

        private void reloadListingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(LoadSongList);
        }

        private int ImportSTFS(string file)
        {
            ProgressUpdate($"Importing {Path.GetFileName(file)}...", 0, 0);
            STFStoWii converter = new STFStoWii(file, $"{root}");
            int songCount = converter.SongCount();
            int importedSongs = 0;
            converter.CopyDTA();
            for (int i = 0; i < songCount; i++)
            {
                try
                {
                    string shortname = converter.Shortname(i);
                    ProgressUpdate($"Importing {Path.GetFileName(file)} ({i}/{songCount})... {shortname} - MIDI", (importedSongs * 4) + 0, 4 * songCount);
                    converter.ConvertMID(i);
                    ProgressUpdate($"Importing {Path.GetFileName(file)} ({i}/{songCount})... {shortname} - MILO", (importedSongs * 4) + 1, 4 * songCount);
                    converter.ConvertMILO(i);
                    ProgressUpdate($"Importing {Path.GetFileName(file)} ({i}/{songCount})... {shortname} - PNG", (importedSongs * 4) + 2, 4 * songCount);
                    converter.ConvertPNG(i);
                    ProgressUpdate($"Importing {Path.GetFileName(file)} ({i}/{songCount})... {shortname} - MOGG", (importedSongs * 4) + 3, 4 * songCount);
                    converter.ConvertMOGG(i);
                    importedSongs++;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    continue;
                }
            }
            ProgressUpdate($"Finalising import of {importedSongs} song{(importedSongs > 1 ? "s" : "")} from {Path.GetFileName(file)}...", 0, 0);
            converter.Finalise();
            return importedSongs;
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openSTFSDialog.ShowDialog() == DialogResult.OK)
            {
                if (openSTFSDialog.FileNames.Length == 0) return;
                pending = true;
                Task.Run(() =>
                {
                    int totalImports = 0;
                    foreach (string file in openSTFSDialog.FileNames)
                    {
                        totalImports += ImportSTFS(file);
                    }
                    LoadSongList();
                    if (openSTFSDialog.FileNames.Length > 1)
                        ProgressDone($"Imported {totalImports} song{(totalImports > 1 ? "s" : "")} from {openSTFSDialog.FileNames.Length} files!");
                    else
                        ProgressDone($"Imported {totalImports} song{(totalImports > 1 ? "s" : "")} from {Path.GetFileName(openSTFSDialog.FileNames[0])}!");
                });
            }
        }

        private void Finalise()
        {
            ProgressUpdate("Reading song list...", 0, 0);
            string[] dtaFiles = Directory.EnumerateFiles($"{root}lava/dta", "*.dta").ToArray();
            ProgressUpdate($"Reading song list (0/{dtaFiles.Length})...", 0, dtaFiles.Length);
            DataArray fullsonglist = new DataArray();
            int doneFiles = 0;
            int addedSongs = 0;
            foreach (string file in dtaFiles)
            {
                string dta = File.ReadAllText(file);
                DataArray songs = DTX.FromDtaString(dta);
                for (int i = 0; i < songs.Count; i++)
                {
                    DataArray song = songs.Array(i);
                    string shortname = song.Array("song").Array("name").String(1).Split('/')[1];
                    FileInfo moggInfo, midInfo, miloInfo;
                    long songSize;
                    try
                    {
                        moggInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.mogg");
                        midInfo = new FileInfo($"{root}songs/{shortname}/{shortname}.mid");
                        miloInfo = new FileInfo($"{root}songs/{shortname}/gen/{shortname}.milo_wii");
                        songSize = moggInfo.Length + midInfo.Length + miloInfo.Length;
                    }
                    catch (Exception)
                    {
                        continue; // if we don't have a mogg, mid or milo, ignore
                    }
                    addedSongs++;
                    fullsonglist.AddNode(song);
                }
                doneFiles++;
                ProgressUpdate($"Reading song list ({doneFiles}/{dtaFiles.Length})...", doneFiles, dtaFiles.Length);
            }
            if (!File.Exists($"{root}songs/gen/songs.dtb"))
            {
                // this is hacky, but it works
                Directory.CreateDirectory($"{root}songs/gen");
                File.WriteAllBytes($"{root}songs/gen/songs.dtb", Resources.songs);
            }
            else
            {
                FileStream songsfile = File.Open($"{root}songs/gen/songs.dtb", FileMode.Open);
                DataArray songsdta = DTX.FromDtb(songsfile);
                bool foundMerge = false;
                foreach (DataNode n in songsdta.Children)
                {
                    if (n.Type == DataType.MERGE && n.ToString().Contains("customs.dta"))
                        foundMerge = true;
                }
                if (!foundMerge)
                {
                    MessageBox.Show("You have a custom songs/gen/songs.dtb that does not include '#merge customs.dta'.\n" +
                        "Custom songs may not show up in-game.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                songsfile.Close();
            }
            ProgressUpdate($"Writing song cache...", 0, 0);
            using (FileStream customsfile = File.OpenWrite($"{root}songs/gen/customs.dtb"))
            {
                DTX.ToDtb(fullsonglist, customsfile, 1, true);
            }
            ProgressDone($"Wrote {addedSongs} song{(addedSongs > 1 ? "s" : "")} to song cache!");
            pending = false;
        }

        private void saveSongCacheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(Finalise);
        }

        private void finaliseAndExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // seems to softlock the application
            Task.Run(Finalise).Wait();
            Application.Exit();
        }
    }
}
