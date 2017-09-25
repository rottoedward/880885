using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.TotalCase.Controllers
{
    public class CommonController : Controller
    {
        // GET: TotalCase/Common
        public ActionResult CRUD_Button(string categoryName)
        {
            ViewBag.categoryName = categoryName;
            return PartialView();
        }
    }
}