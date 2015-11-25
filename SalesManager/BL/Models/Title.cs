using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.Models
{
    public class Title
    {
        public string ManagerLastName { get; protected set; }
        public DateTime Date { get; protected set; }

        public Title(string lastName, DateTime date)
        {
            this.ManagerLastName = lastName;
            this.Date = date;
        }
    }
}
