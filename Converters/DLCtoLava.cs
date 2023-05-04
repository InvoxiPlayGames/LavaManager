using DtxCS;
using GameArchives.STFS;
using GameArchives;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DtxCS.DataTypes;
using GameArchives.U8;

namespace LavaManager.Converters
{
    class DLCtoLava
    {
        private string metadata_filename;
        private string content_filename;
        private U8Package metadata_file;
        private U8Package content_file;

        // encryption for SD encrypted backup files
        private bool encrypted;
        private uint consoleID;
        private byte[] prngKey;

        // import metadata
        private string[] shortnames;
        private string tempPath;
        private string output;

        public DLCtoLava(string metadataFile, string contentFile, string outPath)
        {
            metadata_filename = metadataFile;
            content_filename = contentFile;
            output = outPath;
        }
    }
}
