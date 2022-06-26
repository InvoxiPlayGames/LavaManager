using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LavaManager
{
    internal static class Program
    {
        static public string[] Args;
        static public PleaseWaitDialog pw;
        static public string TempDir;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Args = args;
            string rootDirectory = null;
            TempDir = Path.GetTempPath() + "LavaManager\\";
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            for(int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--root") rootDirectory = args[++i];
                if (args[i] == "--portable") TempDir = "temp\\";
            }

            if (Directory.Exists(TempDir))
                Directory.Delete(TempDir, true);
            Directory.CreateDirectory(TempDir);
            pw = new PleaseWaitDialog();
            if (rootDirectory != null)
                Application.Run(new MainWindow(rootDirectory));
            else
                Application.Run(new InitWindow());

            Directory.Delete(TempDir, true);
        }
    }
}
