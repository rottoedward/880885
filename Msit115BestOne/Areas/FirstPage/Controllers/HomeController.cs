using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.FirstPage.Models;
using Msit115BestOne.Areas.ShoppingCart.Models;
using System.IO;
using Msit115BestOne.Models;

namespace Msit115BestOne.Areas.FirstPage.Controllers
{
    public class HomeController : Controller
    {
        // GET: FirstPage/Home
        buybutton b = new buybutton();
        ManPowerEntities db = new ManPowerEntities();
        public ActionResult Index2()
        {
            int scid;
            int mid = (int)Session["MEMBERID"];

            if (Session["shoppingcardID"] == null) { 
           scid= b.buycheck(mid);
          Session["shoppingcardID"]=scid;

            }
            else
            {
                 scid=(int)Session["shoppingcardID"] ;

            }



            b.SaveinCart(mid, 1, 2, 0, scid);

            return RedirectToAction("CartDatainOrders", "Recipient", new { area = "ShoppingCart" });



            //return View();
        }

        public ActionResult HOME()
        {


            return View();

        }
        public ActionResult testnoaction()
        {


            return View();

        }
        [HttpPost]
        public ActionResult HOME(IEnumerable<HttpPostedFileBase> files)
        {
          if (files != null)
            {

                foreach (var file in files)
                {
                    if (file != null)
                    {

                        var imagesSize = file.ContentLength;
                        byte[] imageByte = new byte[imagesSize];
                        file.InputStream.Read(imageByte, 0, imagesSize);

                        db.Picture.Add(new Picture { CaseID = 1, Images = imageByte });
                        db.SaveChanges();
                        TempData["message"] = "上傳成功";
                    }
                    else
                    {
                        TempData["message"] = "請先選檔案";
                    }
                }
            }
            return RedirectToAction("HOME");
        }
        public ActionResult testdialogsortable()
        {

            return View();
        }
    }
}