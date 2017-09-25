using Msit115BestOne.Areas.MyMember.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Controllers
{
    public class HomeController : Controller
    {
        private ManPowerEntities db = new ManPowerEntities();
        // GET: Home
        public ActionResult Index()
        {
            //最新服務
            ViewBag.Case = (from c in db.Cases
                            join g in db.ManPower on c.CaseID equals g.CaseID
                            orderby c.StartDateTime descending
                            select c).Take(4).ToList();
            //最新贈物
            ViewBag.casess = (from c in db.Cases
                              join g in db.Goods on c.CaseID equals g.CaseID
                              orderby c.StartDateTime descending
                              select c).Take(4).ToList();
            //人氣服務
            ViewBag.CaseM = (from c in db.Cases
                             join g in db.ManPower on c.CaseID equals g.CaseID
                             orderby c.Recommendation descending
                             select c).Take(4).ToList();

            //人氣贈物
            ViewBag.CaseG = (from c in db.Cases
                             join g in db.Goods on c.CaseID equals g.CaseID
                             orderby c.Recommendation descending
                             select c).Take(4).ToList();



            skillmatch skm = new skillmatch();
            List<int> cas = new List<int>();
    List<Cases> ca = new List<Cases>();
            if (Session["MEMBERID"] != null)
            {
                var caselist = skm.Match((int)Session["MEMBERID"]);
                caselist.Sort();
                caselist.Reverse();
                if (caselist.Count >= 7)
                {
                   
                    cas.Add(caselist[0]);
                    cas.Add(caselist[1]);
                    cas.Add(caselist[2]);
                    cas.Add(caselist[3]);
                    cas.Add(caselist[4]);
                    cas.Add(caselist[5]);

                    foreach (var u in cas)
                    {

                        var k = db.Cases.Find(u);
                        ca.Add(k);

                    }


                    ViewBag.MatchCase = ca;



                }
                else
                {
    foreach (var u in caselist)
                {

                    var k = db.Cases.Find(u);
                    ca.Add(k);

                }
            

                ViewBag.MatchCase = ca;
                }



                //List<Cases> ca = new List<Cases>();
            
            }




            return View();

        }
    }
}