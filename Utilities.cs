using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavaManager
{
    class Utilities
    {
        public static string BytesToString(double bytes, int round)
        {
            if (bytes > Math.Pow(1000, 4)) return $"{Math.Round(bytes / Math.Pow(1000, 4), round)} TB";
            if (bytes > Math.Pow(1000, 3)) return $"{Math.Round(bytes / Math.Pow(1000, 3), round)} GB";
            if (bytes > Math.Pow(1000, 2)) return $"{Math.Round(bytes / Math.Pow(1000, 2), round)} MB";
            if (bytes > Math.Pow(1000, 1)) return $"{Math.Round(bytes / Math.Pow(1000, 1), round)} KB";
            return $"{bytes} B";
        }
        public static string BytesToString(double bytes)
        {
            return BytesToString(bytes, 2);
        }
    }
}
