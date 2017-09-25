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
    public class GoodsController : Controller
    {
        //ManPowerEntities db = new ManPowerEntities();
        //AdminGR<Cases> CasesDB = new AdminGR<Cases>();
        AdminGR<Goods> GoodsDB = new AdminGR<Goods>();
        Interception IL = new Interception();

        // GET: Admin/Goods
        public ActionResult BrowseGoodsAll()
        {
            List<GoodsViewModel> gvmList = new List<GoodsViewModel>();
            
            foreach(var gs in (GoodsDB.GetAll()).ToList())
            {
                GoodsViewModel gvm = new GoodsViewModel() {
                    CaseID = gs.CaseID,
                    CaseTitle = gs.Cases.CaseTitle,
                    MemberName = gs.Cases.Member.LastName + gs.Cases.Member.FirstName,
                    StartDateTime = gs.Cases.StartDateTime,
                    EndDateTime = gs.Cases.EndDateTime,
                    GdsCount = gs.GdsCount,
                    StatusName = gs.Cases.CaseStatus.StatusName
                };
                gvmList.Add(gvm);
            }
            return IL.InterceptionLogin(View(gvmList), Session["AdminID"]);
        }

        //[HttpGet]
        //public ActionResult CreateGoods()
        //{
        //    return IL.InterceptionLogin(View(), Session["AdminID"]);
        //}

        //[HttpPost]
        //public ActionResult CreateGoods(Cases cs, Goods gd)
        //{
        //    return RedirectToAction("BrowseGoodsAll","Goods");
        //}

    }
}