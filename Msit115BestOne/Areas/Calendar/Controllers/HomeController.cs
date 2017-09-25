using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.Calendar.Models;

namespace Msit115BestOne.Areas.Calendar.Controllers
{
    public class HomeController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        // GET: Calendar/Home
        public ActionResult Index()
        {
            return PartialView();
        }
 
        public ActionResult GetEvents()
        {
            var calendarCases = db.Cases.Select(v => new
            {
                CaseID=v.CaseID,
                title = v.CaseTitle,
                description = v.CaseContent,
                start =v.StartDateTime,                
                end = v.EndDateTime,
                color = "plum",

            });
            //DateTimeOffset start1 = new DateTimeOffset(start,
            //                        TimeZoneInfo.Local.GetUtcOffset(start));
            var calList = calendarCases.ToList();
            //ViewBag.CaseID= calendarCases.Select(Cases.Equals(calList.))

            var gdc = db.Goods.Select(p => p).ToList();
            ViewBag.GoodCase = gdc;

            return Json(calList, JsonRequestBehavior.AllowGet);     
                 
        }
        public ActionResult ChooseCase(int id) {

            var Goodcases = db.Goods.Where(p => p.CaseID == id).Count();
            var ManCases = db.ManPower.Where(p => p.CaseID == id).Count();



            if(Goodcases>ManCases)
            {
                return RedirectToAction("GoodsRead", "Goods", new { area = "TotalCase",id=id });
            }
            else
            {

                return RedirectToAction("ManPowerRead", "ManPowers", new { area = "TotalCase", id = id });
            }

         
        }
   
           
    }
}