using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomationExecutionSummary.Models.Dashboard
{
    [Table("tbl_GoogleTestResults")]
    public class Dashboard
    {
        [Key]
        public Int64 LogId { get; set; }

        public string TestCaseId { get; set; }
        public string OutCome { get; set; }
        public TimeSpan Duration { get; set; }
        public string TestOwner { get; set; }
        public string BuildTestRunID { get; set; }
        public string TestSuiteName { get; set; }

        public string BugID { get; set; }

        public string Extra_One { get; set; }
        public string Extra_Two { get; set; }
        public string Comment { get; set; }
        public string InstanceURL { get; set; }
        public string ICMBuildNo { get; set; }
        public string ResultDirectory { get; set; }
        public DateTime ExecutionDate { get; set; }
    }
}