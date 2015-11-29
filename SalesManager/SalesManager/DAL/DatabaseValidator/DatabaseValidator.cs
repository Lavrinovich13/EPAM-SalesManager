using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DatabaseValidator
{
    public class DatabaseValidator
    {
        public static void Validate()
        {
            var context = new SalesDB();
            if(!context.Database.Exists())
            {
                context.Database.Create();
            }
        }
    }
}
