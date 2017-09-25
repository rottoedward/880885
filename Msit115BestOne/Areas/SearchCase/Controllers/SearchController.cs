using Msit115BestOne.Areas.SearchCase.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.SearchCase.Controllers
{
    public class SearchController : Controller
    {
        // GET: SearchCase/Search
        private ManPowerEntities db = new ManPowerEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SearchByCity(string CityID)
        {
            string qryCityID = "";
            string[] strCityID = CityID.Split(new char[] { ',' });
            for (int i = 0; i < strCityID.Length; i++)
            {
                if (i != strCityID.Length - 1)
                {
                    qryCityID += "CityID=" + strCityID[i] + " Or ";
                }
                else
                {
                    qryCityID += "CityID=" + strCityID[i];
                }
            }
            var _Region = db.Region.Where(qryCityID);
            var query = db.Cases.Join(_Region, c => c.RegionID, r => r.RegionID, (c, r) => c);
            var q = from c in query join r in db.Region on c.RegionID equals r.RegionID where c.StatusID != 6 select new CaseResultViewModel { CaseID = c.CaseID, Condition = c.StatusID, CaseTitle = c.CaseTitle, CaseContent = c.CaseContent, Location = r.City.CityName + r.RegionName + c.Location };
            return View(q);
        }

        public ActionResult SearchGoods(CaseViewModel _case, int[] gCityID, int[] gRegionID)
        {
            var query = from c in db.Cases join g in db.Goods on c.CaseID equals g.CaseID select c;
            if (_case.KeyWord != null)
            {
                string keyword = _case.KeyWord.Trim();
                if (keyword != "")
                {
                    query = from c in query where (c.CaseTitle.Contains(keyword) || c.CaseContent.Contains(keyword) || c.Location.Contains(keyword)) select c;
                }
            }
            if (gCityID[0] != 0)
            {
                if (gRegionID[0] == 0)
                {
                    string qryCityID = "";
                    for (int i = 0; i < gCityID.Length; i++)
                    {
                        if (i != gCityID.Length - 1)
                        {
                            qryCityID += "CityID=" + gCityID[i] + " Or ";
                        }
                        else
                        {
                            qryCityID += "CityID=" + gCityID[i];
                        }
                    }
                    var _Region = db.Region.Where(qryCityID);
                    query = query.Join(_Region, c => c.RegionID, r => r.RegionID, (c, r) => c);
                }
                else
                {
                    string qryRegionID = "";
                    for (int i = 0; i < gRegionID.Length; i++)
                    {
                        if (i != gRegionID.Length - 1)
                        {
                            qryRegionID += "RegionID=" + gRegionID[i] + " Or ";
                        }
                        else
                        {
                            qryRegionID += "RegionID=" + gRegionID[i];
                        }
                    }
                    query = query.Where(qryRegionID);
                }
            }
            if (_case.gClassID != 0)
            {
                if (_case.gSubClassID == 0)
                {
                    query = from c in query join g in db.Goods on c.CaseID equals g.CaseID where g.GdsSubClass.GdsClassID == _case.gClassID select c;
                }
                else
                {
                    query = from c in query join g in db.Goods on c.CaseID equals g.CaseID where g.GdsSubClassID == _case.gSubClassID select c;
                }
            }
            if (_case.CaseStatus != 0)
            {
                query = query.Where(c => c.StatusID == _case.CaseStatus);
            }
            else
            {
                query = query.Where(c => c.StatusID != 6);
            }
            if (_case.EndDate != null)
            {
                DateTime endDate = Convert.ToDateTime(_case.EndDate);
                query = from c in query join g in db.Goods on c.CaseID equals g.CaseID where DateTime.Compare(g.GdsDeadline, endDate) >= 0 select c;
            }
            var q = from c in query join r in db.Region on c.RegionID equals r.RegionID select new CaseResultViewModel { CaseID = c.CaseID, GiveOrNeed = (c.StatusID == 4) ? "提供" : "徵求", CaseTitle = c.CaseTitle, CaseContent = c.CaseContent, Location = r.City.CityName + r.RegionName + c.Location };
            return PartialView(q);
        }

        public ActionResult SearchManPowers(CaseViewModel _case, int[] mCityID, int[] mRegionID)
        {
            var query = from c in db.Cases join m in db.ManPower on c.CaseID equals m.CaseID select c;
            if (_case.KeyWord != null)
            {
                string keyword = _case.KeyWord.Trim();
                if (keyword != "")
                {
                    query = from c in query where (c.CaseTitle.Contains(keyword) || c.CaseContent.Contains(keyword) || c.Location.Contains(keyword)) select c;
                }
            }
            if (mCityID[0] != 0)
            {
                if (mRegionID[0] == 0)
                {
                    string qryCityID = "";
                    for (int i = 0; i < mCityID.Length; i++)
                    {
                        if (i != mCityID.Length - 1)
                        {
                            qryCityID += "CityID=" + mCityID[i] + " Or ";
                        }
                        else
                        {
                            qryCityID += "CityID=" + mCityID[i];
                        }
                    }
                    var _Region = db.Region.Where(qryCityID);
                    query = query.Join(_Region, c => c.RegionID, r => r.RegionID, (c, r) => c);
                }
                else
                {
                    string qryRegionID = "";
                    for (int i = 0; i < mRegionID.Length; i++)
                    {
                        if (i != mRegionID.Length - 1)
                        {
                            qryRegionID += "RegionID=" + mRegionID[i] + " Or ";
                        }
                        else
                        {
                            qryRegionID += "RegionID=" + mRegionID[i];
                        }
                    }
                    query = query.Where(qryRegionID);
                }
            }
            if (_case.mClassID != 0)
            {
                if (_case.mSubClassID == 0)
                {
                    query = from c in query join m in db.ManPower on c.CaseID equals m.CaseID where m.MPSubClass.MPClassID == _case.mClassID select c;
                }
                else
                {
                    query = from c in query join m in db.ManPower on c.CaseID equals m.CaseID where m.MPSubClassID == _case.mSubClassID select c;
                }
            }
            if (_case.CaseStatus != 0)
            {
                query = query.Where(c => c.StatusID == _case.CaseStatus);
            }
            else
            {
                query = query.Where(c => c.StatusID != 6);
            }
            if (_case.EndDate != null)
            {
                DateTime endDate = Convert.ToDateTime(_case.EndDate);
                query = from c in query join m in db.ManPower on c.CaseID equals m.CaseID where DateTime.Compare(m.MPDate, endDate) >= 0 select c;
            }
            var q = from c in query join r in db.Region on c.RegionID equals r.RegionID select new CaseResultViewModel { CaseID = c.CaseID, GiveOrNeed = (c.StatusID == 8) ? "提供" : "徵求", CaseTitle = c.CaseTitle, CaseContent = c.CaseContent, Location = r.City.CityName + r.RegionName + c.Location };
            return PartialView(q);
        }

        public ActionResult GetCity()
        {
            var city = db.City.Select(c => new { CityID = c.CityID, CityName = c.CityName });
            return Json(city, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetRegion(string CityID)
        {
            string id = "";
            string[] strCityID = CityID.Split(new char[] { ',' });
            for (int i = 0; i < strCityID.Length; i++)
            {
                if (i != strCityID.Length - 1)
                {
                    id += "CityID=" + strCityID[i] + " Or ";
                }
                else
                {
                    id += "CityID=" + strCityID[i];
                }
            }
            var region = db.Region.Where(id).Select(r => new { RegionID = r.RegionID, RegionName = r.RegionName }).OrderBy(r=>r.RegionID);
            return Json(region, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClass(int Condition)
        {
            if (Condition == 1)
            {
                var goods = db.GoodsClass.Select(g => new { ClassID = g.GdsClassID, ClassName = g.GdsClass });
                return Json(goods, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var manpower = db.MPClass.Select(m => new { ClassID = m.MPClassID, ClassName = m.MPClass1 });
                return Json(manpower, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetSubClass(int Condition,int ClassID)
        {
            if (Condition == 1)
            {
                var goodssub = db.GdsSubClass.Where(g => g.GdsClassID == ClassID).Select(g => new { SubClassID = g.GdsSubClassID, SubClassName = g.GdsSubClass1 });
                return Json(goodssub, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var manpowersub = db.MPSubClass.Where(m => m.MPClassID == ClassID).Select(m => new { SubClassID = m.MPSubClassID, SubClassName = m.MPSubClass1 });
                return Json(manpowersub, JsonRequestBehavior.AllowGet);
            }
        }
    }
}