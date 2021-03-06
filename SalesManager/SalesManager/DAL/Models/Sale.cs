﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Sale : IEquatable<Sale>
    {
        public int Id { get; set; }
        public System.DateTime Date { get; set; }
        public decimal Sum { get; set; }

        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Product Product { get; set; }

        public bool Equals(Sale other)
        {
            return Manager == other.Manager &&
                   Client == other.Client &&
                   Product == other.Product &&
                   Date == other.Date &&
                   Sum == other.Sum;
        }
    }
}
