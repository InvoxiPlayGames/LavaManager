using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LavaManager
{
    class WiiCryptoManager
    {
        static public Dictionary<uint, byte[]> ConsolePRNGKeys = new Dictionary<uint, byte[]>();

        static public CryptoStream DecryptContent(Stream content, byte[] key, ushort index)
        {
            Rijndael aes_alg = Rijndael.Create();
            byte[] iv = new byte[16];
            iv[0] = (byte)((index >> 8) & 0xFF);
            iv[1] = (byte)(index & 0xFF);
            ICryptoTransform encryptor = aes_alg.CreateEncryptor(key, iv);
            return new CryptoStream(content, encryptor, CryptoStreamMode.Read);
        }
    }
}
