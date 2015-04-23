using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSQLiteDemo.data
{
    class DataContext : DbContext
    {
        public DataContext()
            : base("demoName")
        {
            //Configuration.ProxyCreationEnabled = true;
            //Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<Customer> Customers { get; set; }

    }
}
