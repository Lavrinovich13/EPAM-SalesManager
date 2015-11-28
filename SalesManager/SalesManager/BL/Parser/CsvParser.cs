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
            if(Regex.IsMatch(title, _titlePattern))
            {
                var match = Regex.Match(title, _titlePattern);

                var d = match.Groups[2].Value;

                DateTime date;
                DateTime.TryParseExact(match.Groups[2].Value, "ddMMyyyy", null, DateTimeStyles.None, out date);

                return new Title(match.Groups[1].Value, date);
            }
            throw new ArgumentException(String.Format("Title {0} has incorrect format", title));
        }

        public Record ParseRecord(string record)
        {
            if (Regex.IsMatch(record, _recordPattern))
            {
                var match = Regex.Match(record, _recordPattern);

                DateTime date;
                DateTime.TryParse(match.Groups[1].Value, out date);

                Decimal sum;
                Decimal.TryParse(match.Groups[5].Value, out sum);

                if (date != null)
                {
                    return new Record(date, match.Groups[2].Value, match.Groups[3].Value, match.Groups[4].Value,  sum);
                }
            }
            throw new ArgumentException(String.Format("Record {0} has incorrect format", record));
        }
    }
}
