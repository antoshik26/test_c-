using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace WinFormsApp1
{
    class Sqllite : DbContext
    {
        public DbSet<baracod> baracods_values { get; set; }

        public string DbPath { get; set; }

        public Sqllite() : base("DefaultConnection")
        {
        }
    }
}
