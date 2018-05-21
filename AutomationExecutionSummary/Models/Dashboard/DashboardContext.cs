using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace AutomationExecutionSummary.Models.Dashboard
{
    public class DashboardContext : DbContext
    {
        public DbSet<Dashboard> AllLogEntries { get; set; }
    }
}