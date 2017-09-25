using Msit115BestOne.Areas.Admin.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.Admin.Service;

namespace Msit115BestOne.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        AdminGR<Administrator> AdminDB = new AdminGR<Administrator>();
        Interception IL = new Interception();

        // GET: Admin/Admin
        public ActionResult BrowseAdminAll()
        {
            //var test = Session["AdminID"];
            return IL.InterceptionLogin(View(AdminDB.GetAll()),Session["AdminID"]);
        }

        public string CheckAdminID(string AdminID)
        {
            if (AdminID == "")
                return "";
            else if (db.Administrator.Where(x => x.AdminID == AdminID).Count() == 1)
                return "已經有此帳號";
            return "";
        }

        [HttpGet]
        public ActionResult CreateAdmin()
        {
            return IL.InterceptionLogin(View(),Session["AdminID"], "", "進行新增", "瀏覽管理員頁面");
        }

        [HttpPost]
        public ActionResult CreateAdmin(Administrator admin)
        {
            admin.Authority = 1;
            AdminDB.Create(admin, Session["AdminID"].ToString(), "新增管理員『" + admin.AdminID + "』", "新增管理員頁面");
            return RedirectToAction("BrowseAdminAll", "Admin");
        }

        [HttpGet]
        public ActionResult EditAdmin(string AdminID)
        {
            //var test = Session["AdminID"];
            //if (Session["AdminID"] == null)
            //    return RedirectToAction("Admin_Login","Home");
            if (AdminID.ToUpper() == "SYS" || (Session["AdminID"].ToString() != "SYS" && AdminID.ToUpper() == "ADMIN"))
            {
                return IL.InterceptionLogin(RedirectToAction("BrowseAdminAll", "Admin"), Session["AdminID"], "準備修改管理員『" + AdminID + "』", "攔截", "瀏覽管理員頁面");
            }
            else
            {
                List<SelectListItem> mySelectItemList = new List<SelectListItem>();

                mySelectItemList.Add(new SelectListItem()
                {
                    Text = "最高權限",
                    Value = "0",
                    Selected = false
                });

                mySelectItemList.Add(new SelectListItem()
                {
                    Text = "一般權限",
                    Value = "1",
                    Selected = true
                });

                ViewBag.mySelectItemList = mySelectItemList;

                //Administrator admin = db.Administrator.Where(x => x.AdminID == AdminID).First();
                return IL.InterceptionLogin(View(AdminDB.GetById(AdminID)), Session["AdminID"], "準備修改管理員『" + AdminID + "』", "進行修改", "瀏覽管理員頁面");
            }
        }

        [HttpPost]
        public ActionResult EditAdmin(Administrator admin)
        {
            AdminDB.Update(admin, Session["AdminID"].ToString(), "修改管理員『" + admin.AdminID + "』", "修改管理員頁面");
            return RedirectToAction("BrowseAdminAll", "Admin");
        }

        [HttpPost]
        public ActionResult DeleteAdmin(string AdminID)
        {
            AdminDB.Delete(AdminDB.GetById(AdminID), Session["AdminID"].ToString(), "刪除管理員『" + AdminID + "』", "瀏覽管理員資料頁面");
            //db.Administrator.Remove(db.Administrator.Where(x => x.AdminID == AdminID).First());
            //db.SaveChanges();
            return RedirectToAction("BrowseAdminAll", "Admin");
             
            //Administrator admin = db.Administrator.Where(x => x.AdminID == AdminID).First();
            //return View(admin);
        }

        //public ActionResult DeleteAdminS(Administrator admin)
        //{
        //    AdminDB.Delete(admin);
        //    return RedirectToAction("BrowseAdminAll", "Admin");
        //}
    }
}