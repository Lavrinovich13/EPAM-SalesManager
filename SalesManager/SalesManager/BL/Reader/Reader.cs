using BL.Reader.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Reader
{
    public class LineByLineReader : IReader
    {
        public IEnumerable<string> Read(string path)
        {
            using (var reader = new StreamReader(path, Encoding.GetEncoding("windows-1251")))
            {
                string currentString;
                while ((currentString = reader.ReadLine()) != null)
                {
                    yield return currentString;
                }
            }
        }
    }
}
