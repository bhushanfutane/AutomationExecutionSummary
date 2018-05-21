using AutomationExecutionSummary.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace AutomationExecutionSummary.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [HttpGet]
        public ActionResult Index(string buildRunId, string suiteName)
        {
            DashboardContext dashboardContext = new DashboardContext();
            List<Dashboard> dashboardItems = new List<Dashboard>();

            try
            {
                //if (string.IsNullOrEmpty(buildRunId))
                //{
                //    DateTime previousdate = DateTime.Today.AddDays(-1);
                //    buildRunId = previousdate.Month.ToString("00") + previousdate.Day.ToString("00") + previousdate.Year.ToString();
                //}

                //if (string.IsNullOrEmpty(suiteName))
                //{
                dashboardItems = dashboardContext.AllLogEntries.ToList();
                //}
                //else
                //{
                //    dashboardItems = dashboardContext.AllLogEntries.Where(dashboard => dashboard.BuildTestRunID == buildRunId && dashboard.TestSuiteName.Contains(suiteName)).ToList();
                //}

                //TempData["buildRunId"] = buildRunId;
                //TempData["suiteName"] = suiteName;
                return View(dashboardItems);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }

        /// <summary>
        /// It will get failure test case details
        /// </summary>
        /// <param name="logID"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestCaseFailureDetails(int logID)
        {
            DashboardContext dashboardContext = new DashboardContext();
            Dashboard logEntry = new Dashboard();

            if (logID != 0)
            {
                logEntry = dashboardContext.AllLogEntries.Where(log => log.LogId == logID).FirstOrDefault();
            }

            string viewContent = ConvertViewToString("_LogDetails", logEntry);

            return Json(new { PartialView = viewContent });
        }

        [HttpPost]
        public Boolean AddRecord(Dashboard testCaseData)
        {
            DashboardContext dashboardContext = new DashboardContext();
            Dashboard logEntry = new Dashboard();
            var vv = 0;
            try
            {
                if (testCaseData.LogId == 0)
                {
                    logEntry.LogId = Convert.ToInt32(dashboardContext.AllLogEntries.Max(x => x.LogId) + 1);
                }
                ////logEntry.LogId = vv;
                logEntry.BugID = testCaseData.BugID;
                logEntry.Comment = testCaseData.Comment;
                logEntry.Extra_One = testCaseData.Extra_One;
                logEntry.Extra_Two = testCaseData.Extra_Two;
                logEntry.OutCome = testCaseData.OutCome;

                dashboardContext.Entry(logEntry).State = System.Data.Entity.EntityState.Added;
                dashboardContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return false;
            }
        }

        [HttpPost]
        public Boolean DeleteRecord(Int32 logID)
        {
            try
            {
                DashboardContext dashboardContext = new DashboardContext();
                Dashboard logEntry = new Dashboard();

                logEntry = dashboardContext.AllLogEntries.Where(logs => logs.LogId == logID).FirstOrDefault();
                dashboardContext.Entry(logEntry).State = System.Data.Entity.EntityState.Deleted;
                dashboardContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// It will get previous test case results
        /// </summary>
        /// <param name="testCaseId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult TestCaseHistory(string testCaseId)
        {
            DashboardContext dashboardContext = new DashboardContext();
            List<Dashboard> testcaseHistory = new List<Dashboard>();

            testcaseHistory = dashboardContext.AllLogEntries.Where(log => log.TestCaseId == testCaseId).OrderByDescending(log => log.LogId).ToList();

            string viewContent = ConvertViewToString("_TestCaseHistoryDetails", testcaseHistory);

            return Json(new { PartialView = viewContent });
        }

        /// <summary>
        /// Convert View In to String to show update test suite panel
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }

        /// <summary>
        /// Update failure details
        /// </summary>
        /// <param name="testCaseData"></param>
        /// <returns></returns>
        [HttpPost]
        public Boolean UpdateLog(Dashboard testCaseData)
        {
            DashboardContext dashboardContext = new DashboardContext();
            Dashboard logEntry = new Dashboard();

            try
            {
                logEntry = dashboardContext.AllLogEntries.Where(logs => logs.LogId == testCaseData.LogId).FirstOrDefault();
                if (testCaseData.LogId == 0)
                {
                    logEntry.LogId = dashboardContext.AllLogEntries.Max(x => x.LogId) + 1;
                }
                logEntry.BugID = testCaseData.BugID;
                logEntry.Comment = testCaseData.Comment;
                logEntry.Extra_One = testCaseData.Extra_One;
                logEntry.Extra_Two = testCaseData.Extra_Two;
                logEntry.OutCome = testCaseData.OutCome;

                dashboardContext.Entry(logEntry).State = System.Data.Entity.EntityState.Modified;
                dashboardContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return false;
            }
        }
    }
}