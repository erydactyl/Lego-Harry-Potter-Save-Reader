using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Lego_Harry_Potter_Save_Reader
{
    class Functions
    {
        public byte[] GetRawBytes(string filename, int offset, int size)
        {
            using (BinaryReader br = new BinaryReader(File.Open(filename,FileMode.Open,FileAccess.Read,FileShare.ReadWrite)))
            {
                int length = (int)br.BaseStream.Length;
                br.BaseStream.Seek(offset, SeekOrigin.Begin);
                byte[] bytes = new byte[size];
                int i = 0;
                while (offset < length && i < size)
                {
                    bytes[i] = br.ReadByte();
                    offset++;
                    i++;
                }
                br.Close();
                return bytes;
            }
        }
        public string LoadTimer(string filename)
        {
            string contents = "";
            contents = File.ReadAllText(@filename);
            if (Regex.IsMatch(contents, @"(([0-9])\d+):([0-5][0-9]):([0-5][0-9])"))
            {
                return contents;
            }
            else
            {
                return "00:00:00";
            }
            
        }

    }
}
