using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProcessedReport : IEquatable<ProcessedReport>
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public bool Equals(ProcessedReport other)
        {
            return FileName == other.FileName;
        }
    }
}
