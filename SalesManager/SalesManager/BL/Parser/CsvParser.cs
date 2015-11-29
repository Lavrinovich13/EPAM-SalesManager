using BL.Models;
using BL.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BL.Parser
{
    public class CsvParser : IParser
    {
        protected string _titlePattern = @"(\w+)_(\d{8})\b";
        protected string _recordPattern = @"(\d{2}\.\d{2}\.\d{4});(\w+);(\w+);(\w+);(\d+)";

        public Title ParseTitle(string title)
        {
            Match match;
            if ((match = Regex.Match(title, _titlePattern)).Success)
            {
                var date  = DateTime.ParseExact(match.Groups[2].Value, "ddMMyyyy", null);

                return new Title(match.Groups[1].Value, date);
            }
            throw new ArgumentException(String.Format("Title {0} has incorrect format", title));
        }

        public Record ParseRecord(string record)
        {
            Match match;
            if ((match = Regex.Match(record, _recordPattern)).Success)
            {
                var date = DateTime.Parse(match.Groups[1].Value);
                var sum = Decimal.Parse(match.Groups[5].Value);
                
                return new Record(date, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value,  sum);
            }
            throw new ArgumentException(String.Format("Record {0} has incorrect format", record));
        }
    }
}
