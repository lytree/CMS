using CMS.Data.Models;
using FreeSql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Context
{
    public class ConfigContext : DbContext
    {
        public DbSet<ConfigItem> item { set; get; }
        public DbSet<ConfigGroup> group { set; get; }


    }
}
