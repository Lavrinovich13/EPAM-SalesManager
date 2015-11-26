using BL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Models.Interfaces
{
    public interface IParser
    {
        Title ParseTitle(string title);
        Record ParseRecord(string record);
    }
}
