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
    public class HomeController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        LogService logService = new LogService();
        
        // GET: Admin/Home
        public ActionResult Index()
        {
            if (Session["AdminID"] == null)
            {
                logService.LogInfo("不明人士", "", "攔截", "首頁");
                return RedirectToAction("Admin_Login", "Home");
            }

            return View();
        }

        [HttpGet]
        public ActionResult Admin_Login()
        {
            if (Session["Authority"] != null && Session["AdminID"] != null)
            {
                string adminName = Session["AdminID"].ToString();
                Session["Authority"] = null;
                Session["AdminID"] = null;
                logService.LogInfo(adminName, "", "登出", "系統頁面");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Admin_Login(Administrator admin)
        {
            Administrator adminS = db.Administrator.Where(x => x.AdminID == admin.AdminID).FirstOrDefault();
            if (adminS != null && adminS.Apassword == admin.Apassword)
            {
                Session["Authority"] = adminS.Authority;
                Session["AdminID"] = adminS.AdminID;
                logService.LogInfo(Session["AdminID"].ToString(),"","登入", "登入頁面");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.admin_login_error = "帳號密碼有誤，請重新輸入！";
            logService.LogInfo(admin.AdminID, "登入失敗，輸入密碼為" + admin.Apassword, "登入", "登入頁面");
            return View();
        }
    }
}