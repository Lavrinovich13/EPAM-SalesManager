using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Reader.Interfaces
{
    public interface IReader
    {
        IEnumerable<string> Read(string path);
    }
}
