using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace EasyAuthApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Who are you?";
            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;
            var userName = principal?.Identity?.Name;
            if (!String.IsNullOrEmpty(userName))
            {
                Console.WriteLine(userName);
                ViewBag.Message = userName;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Headers";

            ClaimsPrincipal principal = HttpContext.User as ClaimsPrincipal;

            DataTable headersTable = new DataTable();
            headersTable.Columns.Add(new DataColumn("Key", typeof(string)));
            headersTable.Columns.Add(new DataColumn("Value", typeof(string)));

            foreach (var header in this.HttpContext.Request.Headers)
            {
                DataRow row = headersTable.NewRow();
                row["Key"] = header.ToString();
                row["Value"] = this.HttpContext.Request.Headers[header.ToString()];
                headersTable.Rows.Add(row);
            }
            ViewBag.Table = headersTable;

            DataTable principalsTable = new DataTable();
            principalsTable.Columns.Add(new DataColumn("Key", typeof(string)));
            principalsTable.Columns.Add(new DataColumn("Value", typeof(string)));

            foreach (var claim in principal.Claims)
            {
                DataRow row = principalsTable.NewRow();
                row["Key"] = claim.Type;
                row["Value"] = claim.Value;
                principalsTable.Rows.Add(row);
            }
            ViewBag.UserPrincipalTable = principalsTable;


            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}