using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Manager : IEquatable<Manager>
    {
        public Manager()
        {
            this.Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string LastName { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }

        public bool Equals(Manager other)
        {
            return LastName == other.LastName;
        }
    }
}
