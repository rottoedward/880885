using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.Admin.Service;

namespace Msit115BestOne.Areas.Admin.Service
{
    public class Interception : Controller
    {
        LogService log = new LogService();

        public ActionResult InterceptionLogin(ActionResult view, object AdminID)
        {
            if (AdminID == null)
                return RedirectToAction("Admin_Login", "Home");
            return view;
        }

        public ActionResult InterceptionLogin(ActionResult view, object AdminID, string SaveData, string Data_Action, string Orignal_Page)
        {
            if (AdminID == null)
            {
                Data_Action = "攔截";
                log.LogInfo("不明人士", SaveData, Data_Action, Orignal_Page);
                return RedirectToAction("Admin_Login", "Home");
            }
            log.LogInfo(AdminID.ToString(), SaveData, Data_Action, Orignal_Page);
            return view;
        }
    }
}