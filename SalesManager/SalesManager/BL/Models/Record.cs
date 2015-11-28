using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Models
{
    public class Record
    {
        public DateTime Date { get; protected set; }
        public string ClientLastName { get; protected set; }
        public string ClientFirstName { get; protected set; }
        public string ProductName { get; protected set; }
        public decimal Sum { get; protected set; }

        public Record(DateTime date, string clientLastName, string clientFirstName, string productName, decimal sum)
        {
            this.Date = date;
            this.ClientLastName = clientLastName;
            this.ClientFirstName = clientFirstName;
            this.ProductName = productName;
            this.Sum = sum;
        }
    }
}
