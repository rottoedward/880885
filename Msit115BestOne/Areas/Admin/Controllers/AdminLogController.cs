using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Models;
using Msit115BestOne.Areas.Admin.Models;
using Msit115BestOne.Areas.Admin.Service;

namespace Msit115BestOne.Areas.Admin.Controllers
{
    public class AdminLogController : Controller
    {
        AdminGR<AdminLog> logDB = new AdminGR<AdminLog>();
        Interception IL = new Interception();

        // GET: Admin/AdminLog
        public ActionResult BrowseAdminLogAll()
        {
            return IL.InterceptionLogin(View(logDB.GetAll()), Session["AdminID"]);
        }
    }
}