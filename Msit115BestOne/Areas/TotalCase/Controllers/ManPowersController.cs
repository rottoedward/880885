using Msit115BestOne.Areas.MyMember.Models;
using Msit115BestOne.Areas.ShoppingCart.Models;
using Msit115BestOne.Areas.TotalCase.Models;
using Msit115BestOne.Areas.TotalCase.Models.ManPowersGroupView;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.TotalCase.Controllers
{


    public class ManPowersController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        TotalCasesGR<ManPower> ManPowerDB = new TotalCasesGR<ManPower>();

        // GET: TotalCase/ManPowers
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManPowerAll()
        {
            ManPowerGroupViewModel mpgvm = new ManPowerGroupViewModel();
            mpgvm.ManPower = new List<ManPowersViewModel>();
            mpgvm.ManPowerClass = new List<ManPowersClassViewModel>();
            //List<ManPowersViewModel> caseall = new List<ManPowersViewModel>();

            ////挑選人力分享案件
            //var cas = db.Cases.Where(p => p.StatusID == 8).ToList();
            //List<int> items=new List<int>();
            //foreach(var a in cas)
            //{
            //    items.Add(a.CaseID);
            //}
            //int len = items.Count();
            //List<ManPower> man = new List<ManPower>();

            //for(int i=0;i<len;i++)
            //{
            //    int t = items[i];
            //   var qwe= db.ManPower.Where(o => o.CaseID == t).First();
            //    man.Add(qwe);
            //}

            ////人力分享案件數量
            //ViewBag.give= man.Count();

            ////挑選人力需求案件
            //var ncas = db.Cases.Where(p => p.StatusID == 7).ToList();
            //List<int> nitems = new List<int>();
            //foreach (var a in ncas)
            //{
            //    nitems.Add(a.CaseID);
            //}
            //int nlen = nitems.Count();
            //List<ManPower> nman = new List<ManPower>();

            //for (int i = 0; i < nlen; i++)
            //{
            //    int t = nitems[i];
            //    var qwe = db.ManPower.Where(o => o.CaseID == t).First();
            //    nman.Add(qwe);
            //}
            ////人力需求案件數量
            //ViewBag.need =nman.Count();


            foreach (ManPower mp in db.ManPower.Where(o => o.MPActuralCount < o.MPNeedCount).OrderByDescending(o => o.MPID).ToList())
            {
                ManPowersViewModel mpvm = new ManPowersViewModel()
                {
                    CaseID = mp.CaseID,
                    MPName = mp.MPName,
                    MemberID = mp.Cases.MemberID,
                    MPNeedCount = mp.MPNeedCount,
                    MPActuralCount = mp.MPActuralCount,
                    NickName = mp.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(mp.CaseID).StatusID).StatusName,
                    CaseTitle = mp.Cases.CaseTitle,
                    MPClass1 = mp.MPSubClass.MPClass.MPClass1
                };

                mpgvm.ManPower.Add(mpvm);
            }
            #region
            //foreach (ManPower mp in (ManPowerDB.GetAll()).ToList())
            //{
            //    ManPowersViewModel mpvm = new ManPowersViewModel()
            //    {
            //        CaseID = mp.CaseID,
            //        MPName = mp.MPName,
            //        MemberID = mp.Cases.MemberID,
            //        MPNeedCount = mp.MPNeedCount,
            //        MPActuralCount = mp.MPActuralCount,
            //        NickName = mp.Cases.Member.NickName,
            //        StatusName = db.CaseStatus.Find(db.Cases.Find(mp.CaseID).StatusID).StatusName,
            //        CaseTitle = mp.Cases.CaseTitle,
            //        MPClass1 = "全部案件"
            //    };

            //    mpgvm.ManPower.Add(mpvm);
            //    caseall.Add(mpvm);

            //}
            //ViewBag.allcase = caseall;

            ////總案件
            //ViewBag.all = ViewBag.give+ ViewBag.need;

            ////把類別加進去人力分享案件內
            //foreach (var q in man)
            //{
            //    ManPowersViewModel mpvmm = new ManPowersViewModel()
            //    {
            //        CaseID = q.CaseID,
            //        MPName = q.MPName,
            //        MemberID = q.Cases.MemberID,
            //        MPNeedCount = q.MPNeedCount,
            //        MPActuralCount = q.MPActuralCount,
            //        NickName = q.Cases.Member.NickName,
            //        StatusName = db.CaseStatus.Find(db.Cases.Find(q.CaseID).StatusID).StatusName,
            //        CaseTitle = q.Cases.CaseTitle,
            //        MPClass1 = "人力分享"
            //    };

            //    mpgvm.ManPower.Add(mpvmm);


            //}
            ////類別加進去人力需求案件內
            //foreach (var q in nman)
            //{
            //    ManPowersViewModel mpvmm = new ManPowersViewModel()
            //    {
            //        CaseID = q.CaseID,
            //        MPName = q.MPName,
            //        MemberID = q.Cases.MemberID,
            //        MPNeedCount = q.MPNeedCount,
            //        MPActuralCount = q.MPActuralCount,
            //        NickName = q.Cases.Member.NickName,
            //        StatusName = db.CaseStatus.Find(db.Cases.Find(q.CaseID).StatusID).StatusName,
            //        CaseTitle = q.Cases.CaseTitle,
            //        MPClass1 = "人力需求"
            //    };

            //    mpgvm.ManPower.Add(mpvmm);


            //}

            #endregion
            IQueryable<string> gc = db.MPClass.Select(x => x.MPClass1).Distinct();
            foreach (string GCdb in gc)
            {
                ManPowersClassViewModel mpcvm = new ManPowersClassViewModel();
                mpcvm.MPClass1 = GCdb;
               
               mpgvm.ManPowerClass.Add(mpcvm);
              }

            return View(mpgvm);

        }
        public ActionResult ManPowerneed()
        {
            ManPowerGroupViewModel mpgvm = new ManPowerGroupViewModel();
            mpgvm.ManPower = new List<ManPowersViewModel>();
            mpgvm.ManPowerClass = new List<ManPowersClassViewModel>();
            var q = (from o in db.Cases
                     join g in db.ManPower on o.CaseID equals g.CaseID
                     where o.StatusID == 7
                     orderby g.MPID descending
                     select g).ToList();
            foreach (ManPower mp in q)
            {
                ManPowersViewModel mpvm = new ManPowersViewModel()
                {
                    CaseID = mp.CaseID,
                    MPName = mp.MPName,
                    MemberID = mp.Cases.MemberID,
                    MPNeedCount = mp.MPNeedCount,
                    MPActuralCount = mp.MPActuralCount,
                    NickName = mp.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(mp.CaseID).StatusID).StatusName,
                    CaseTitle = mp.Cases.CaseTitle,
                    MPClass1 = mp.MPSubClass.MPClass.MPClass1
                };

                mpgvm.ManPower.Add(mpvm);
            }

            IQueryable<string> gc = db.MPClass.Select(x => x.MPClass1).Distinct();
            foreach (string GCdb in gc)
            {
                ManPowersClassViewModel mpcvm = new ManPowersClassViewModel();
                mpcvm.MPClass1 = GCdb;

                mpgvm.ManPowerClass.Add(mpcvm);
            }

            return View(mpgvm);
        }
        public ActionResult ManPowergive()
        {
            ManPowerGroupViewModel mpgvm = new ManPowerGroupViewModel();
            mpgvm.ManPower = new List<ManPowersViewModel>();
            mpgvm.ManPowerClass = new List<ManPowersClassViewModel>();
            var q = (from o in db.Cases
                     join g in db.ManPower on o.CaseID equals g.CaseID
                     where o.StatusID == 8
                     orderby g.MPID descending
                     select g).ToList();
            foreach (ManPower mp in q)
            {
                ManPowersViewModel mpvm = new ManPowersViewModel()
                {
                    CaseID = mp.CaseID,
                    MPName = mp.MPName,
                    MemberID = mp.Cases.MemberID,
                    MPNeedCount = mp.MPNeedCount,
                    MPActuralCount = mp.MPActuralCount,
                    NickName = mp.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(mp.CaseID).StatusID).StatusName,
                    CaseTitle = mp.Cases.CaseTitle,
                    MPClass1 = mp.MPSubClass.MPClass.MPClass1
                };

                mpgvm.ManPower.Add(mpvm);
            }

            IQueryable<string> gc = db.MPClass.Select(x => x.MPClass1).Distinct();
            foreach (string GCdb in gc)
            {
                ManPowersClassViewModel mpcvm = new ManPowersClassViewModel();
                mpcvm.MPClass1 = GCdb;

                mpgvm.ManPowerClass.Add(mpcvm);
            }

            return View(mpgvm);
        }

        public ActionResult ManPowerRead(int id = 0)
        {
            CaseManPowers cg = new CaseManPowers();
            if (id == 0)
            {
                id = (int)Session["CaseID"];
            }

            var q = db.Cases.Find(id);
            if (Session["MEMBERID"] != null)
            {
                ViewBag.mid = q.MemberID;
            }
            cg.NickName = db.Member.Find(q.MemberID).NickName;
            cg.CaseID = q.CaseID;
            cg.StatusID = q.StatusID;
            Session["CaseID"] = q.CaseID;
            cg.CaseTitle = q.CaseTitle;

            cg.CaseContent = q.CaseContent;
            cg.StartDateTime = q.StartDateTime;
            cg.Location = q.Location; //地址還不完整!!!!!!!
            cg.Contact = q.Contact;
            cg.StatusName = db.CaseStatus.Find(q.StatusID).StatusName; //狀態
            var g = db.ManPower.Where(m => m.CaseID == q.CaseID).First();
            cg.MPName = g.MPName;
            cg.MPNeedCount = g.MPNeedCount;
            cg.MPPoint = g.MPPoint;
            cg.MPActuralCount = g.MPActuralCount;
            cg.MPDate = g.MPDate;
            cg.MPTime = g.MPTime;
            var sub = db.MPSubClass.Find(g.MPSubClassID);
            cg.MPSubClass1 = sub.MPSubClass1; //小分類
            cg.MPClass1 = db.MPClass.Find(sub.MPClassID).MPClass1;//大分類
            var re = db.Region.Find(q.RegionID);
            cg.RegionName = re.RegionName;                         //區
            cg.CityName = db.City.Find(re.CityID).CityName;   //縣市
            int pcount = db.Picture.Where(m => m.CaseID == id).Count();
            if (pcount > 0)
            {
                var pimg = db.Picture.Where(m => m.CaseID == id).FirstOrDefault();
                cg.ImageID = pimg.ImageID;

            }

            //===============讀出這個case所有留言的會員資料 留言視窗使用
            List<ContentViewModel> cv = new List<ContentViewModel>();
            var allmember = (from m in db.Content
                             where m.CaseID == q.CaseID
                             orderby m.MessageDateTime descending
                             select m).ToList();

            foreach (var c in allmember)
            {
                ContentViewModel cvm = new ContentViewModel();
                cvm.CaseID = q.CaseID;
                cvm.MemberID = c.MemberID;
                cvm.NickName = db.Member.Find(c.MemberID).NickName;
                cvm.Photo = db.Member.Find(c.MemberID).Photo;
                cvm.MessageContent = c.MessageContent;
                cvm.MessageDateTime = c.MessageDateTime;
                cvm.AuthorRepeat = c.AuthorRepeat;

                cv.Add(cvm);

            }
            ViewBag.ContentData = cv;


            //dialog 彈出會員資訊視窗

            MemberCaseCount mcc = new MemberCaseCount();

        
            MemberSimpleProfile msp = new MemberSimpleProfile();
            int memberid =  q.MemberID;

            msp.CaseID = q.CaseID;
            msp.MemberID = q.MemberID;
            msp.Email = db.Member.Find(q.MemberID).Email;
            msp.NickName = db.Member.Find(q.MemberID).NickName;
            msp.Photo = db.Member.Find(q.MemberID).Photo;
            msp.sumpoint = mcc.casecount(memberid)[0];
            

            ViewBag.MSP = msp;

            var msk = db.MPSCList.Where(p => p.MemberID == memberid).Select(p => p.MPSubClassID).ToList(); //轉中文
            List<string> myskillname = new List<string>();
            foreach (var a in msk)
            {
                var mk = db.MPSubClass.Where(p => p.MPSubClassID == a).Select(p => p.MPSubClass1).First();
                myskillname.Add(mk);
            }

            ViewBag.Myskill = myskillname;






            return View(cg);

        }
        public ActionResult MPMemberContent(int id, string Content)
        {


            //========存值
            if (Content != null)
            {
                Content cont = new Content();
                cont.CaseID = id;
                cont.MemberID = (int)Session["MEMBERID"];
                cont.MessageContent = Content;
                cont.MessageDateTime = DateTime.Now;

                db.Content.Add(cont);
                db.SaveChanges();
            }
        
            //========即刻讀值
            var lastc = db.Content.Where(p => p.CaseID == id).OrderByDescending(p => p.MessageDateTime).First();
            ContentViewModel cvm = new ContentViewModel();

            cvm.CaseID = id;
            cvm.MemberID = lastc.MemberID;
            var m = db.Member.Find(lastc.MemberID);
            cvm.NickName = m.NickName;
            cvm.Photo = m.Photo;
            cvm.MessageContent = lastc.MessageContent;
            cvm.MessageDateTime = lastc.MessageDateTime;
            cvm.AuthorRepeat = lastc.AuthorRepeat;

            ViewBag.mem = lastc.MemberID;
            ViewBag.pho = m.Photo;
            ViewBag.data = cvm;


            return Json(cvm, JsonRequestBehavior.AllowGet);

        }

        buybutton b = new buybutton();
        public ActionResult MPDetailtoCart(int caseid)
        {


            int scid;
            int mid;

            if (Session["MEMBERID"] == null)
            {

                return RedirectToAction("Login", "Member", new { area = "MyMember" });

            }
            else
            {
                mid = (int)Session["MEMBERID"];
           




            if (Session["shoppingcardID"] == null)
            {
                scid = b.buycheck(mid);
                Session["shoppingcardID"] = scid;

            }
            else
            {
                scid = (int)Session["shoppingcardID"];

            }

            b.SaveinCart(mid, caseid, 1, 0, scid);

            return RedirectToAction("ManPowerAll", "ManPowers", new { area = "TotalCase" }); }
        }

        public ActionResult ManPowerCreate(IEnumerable<HttpPostedFileBase> files)
        {
            int mid = (int)Session["MEMBERID"];
            #region
            List<MPClass> gc = new List<MPClass>();
            MPClass first = new MPClass();
            first.MPClassID = 10000;
            first.MPClass1 = "請選擇類別";
            gc.Add(first);
            var q = db.MPClass;
            foreach (var a in q)
            {
                gc.Add(a);

            }
            List<MPSubClass> gsc = new List<MPSubClass>();
            MPSubClass sbfirst = new MPSubClass();
            sbfirst.MPSubClassID = 10000;
            sbfirst.MPSubClass1 = "請先選擇大類別";
            gsc.Add(sbfirst);
            ViewBag.MPClassID = new SelectList(gc, "MPClassID", "MPClass1", first.MPClassID);
            ViewBag.MPSubClassID = new SelectList(gsc, "MPSubClassID", "MPSubClass1", sbfirst.MPSubClassID);

            List<City> ct = new List<City>();
            City ctfirst = new City();
            ctfirst.CityID = 10000;
            ctfirst.CityName = "請選擇縣市";
            ct.Add(ctfirst);
            var z = db.City;
            foreach (var a in z)
            {
                ct.Add(a);
            }
            List<Region> rg = new List<Region>();
            Region rgfirst = new Region();
            rgfirst.RegionID = 10000;
            rgfirst.RegionName = "請先選擇縣市";
            rg.Add(rgfirst);
            ViewBag.CityID = new SelectList(ct, "CityID", "CityName", ctfirst.CityID);
            ViewBag.RegionID = new SelectList(rg, "RegionID", "RegionName", rgfirst.RegionID);
            #endregion
            if (Request.Form.Count > 0)
            {

                db.Cases.Add(new Cases
                {
                    CaseTitle = Request.Form["CaseTitle"],
                    CaseContent = Request.Form["CaseContent"],
                    StartDateTime = DateTime.Now,
                    MemberID = mid,
                    Recommendation=0,
                    Location = Request.Form["Location"],
                    RegionID = Convert.ToInt32(Request.Form["RegionID"]),
                    Contact = Request.Form["Contact"],
                    StatusID = Convert.ToInt32(Request.Form["StatusID"]),

                });
                db.SaveChanges();

                int cid = db.Cases.Where(c => c.MemberID == mid).OrderByDescending(c => c.CaseID).First().CaseID;
                db.ManPower.Add(new ManPower
                {
                    CaseID = cid,
                    MPPoint = 0,
                    MPName = Request.Form["CaseContent"],
                    MPNeedCount = Convert.ToInt32(Request.Form["MPNeedCount"]),
                    MPSubClassID = Convert.ToInt32(Request.Form["MPSubClassID"]),

                    MPDate = Convert.ToDateTime(Request.Form["MPDate"]),
                    MPTime = Convert.ToDateTime(Request.Form["MPTime"]),



                });

                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file != null)
                        {
                            var imagesSize = file.ContentLength;
                            byte[] imageByte = new byte[imagesSize];
                            file.InputStream.Read(imageByte, 0, imagesSize);

                            db.Picture.Add(new Picture { CaseID = cid, Images = imageByte });
                            db.SaveChanges();
                            TempData["message"] = "上傳成功";
                        }
                        else
                        {
                            TempData["message"] = "請先選檔案";
                        }
                    }
                }
                db.SaveChanges();
                return RedirectToAction("ManPowerAll");
            }
            else
            {
                return View();
            }


           



          






        }
    }
}