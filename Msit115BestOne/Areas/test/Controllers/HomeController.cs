using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.test.Controllers
{
    public class HomeController : Controller
    {
        // GET: test/Home
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult up(HttpPostedFileBase upfile)
        {
            //檢查是否有選擇檔案
            if (upfile != null)
            {
                //檢查檔案大小要限制也可以在這裡做
                if (upfile.ContentLength > 0)
                {
                    string savedName = Path.Combine(Server.MapPath("~/"), upfile.FileName);
                    upfile.SaveAs(savedName);
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult down()
        {
            //我要下載的檔案位置
            string filepath = Server.MapPath("~/download/八月份電子報.html");
            //取得檔案名稱
            string filename = System.IO.Path.GetFileName(filepath);
            //讀成串流
            Stream iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
            //回傳出檔案
            return File(filepath, "application/html", filename);
        }
    }
}