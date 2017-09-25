using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Models;
using Msit115BestOne.Areas.TotalCase.Models;
using Msit115BestOne.Areas.TotalCase.Models.GoodsGroupView;
using Msit115BestOne.Areas.ShoppingCart.Models;
using Msit115BestOne.Areas.MyMember.Models;

namespace Msit115BestOne.Areas.TotalCase.Controllers
{
    public class GoodsController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        TotalCasesGR<Goods> GoodsDB = new TotalCasesGR<Goods>();

        //TotalCasesGR<GoodsClass> GoodsClassDB = new TotalCasesGR<GoodsClass>();
        //TotalCasesGR<Cases> CasesDB = new TotalCasesGR<Cases>();
        //TotalCasesGR<ManPower> ManPowerDB = new TotalCasesGR<ManPower>();
        //TotalCasesGR<Picture> PictureDB = new TotalCasesGR<Picture>();
        //Picture PictureByCaseID = new Picture();

        // GET: TotalCase/Cases

        //public ActionResult BrowseManPower()
        //{
        //    ManPowerViewModel mpvm = new ManPowerViewModel();
        //    mpvm.ManPowerInfo = ManPowerDB.GetAll();
        //    return View(mpvm);

        //    //vm.CasesInfo = CasesDB.GetAll();
        //    //vm.PictureInfo = PictureDB.GetAll();
        //}

        public ActionResult GoodsIndex()
        {
            return View();
        }

        public ActionResult BrowseGoodsAll()
        {
            GoodsGroupViewModel ggvm = new GoodsGroupViewModel();
            ggvm.Goods = new List<GoodsViewModel>();
            ggvm.GoodsClass = new List<GoodsClassViewModel>();

            foreach (Goods Gdb in (db.Goods.Where(o => o.GdsCount != 0).OrderByDescending(o => o.GdsID).ToList()))
            {
                GoodsViewModel gvm = new GoodsViewModel()
                {
                    CaseID = Gdb.CaseID,
                    GdsName = Gdb.GdsName,
                    MemberID = Gdb.Cases.MemberID,
                    GdsCount = Gdb.GdsCount,
                    NickName = Gdb.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(Gdb.CaseID).StatusID).StatusName,
                    CaseTitle = Gdb.Cases.CaseTitle,
                    GdsClass = Gdb.GdsSubClass.GoodsClass.GdsClass
                };
                ggvm.Goods.Add(gvm);
            }

            IQueryable<string> gc = db.GoodsClass.Select(x => x.GdsClass).Distinct();
            foreach (string GCdb in gc)
            {
                GoodsClassViewModel gcvm = new GoodsClassViewModel();
                gcvm.GdsClass = GCdb;
                ggvm.GoodsClass.Add(gcvm);
            }

            return View(ggvm);
        }
        public ActionResult Goodsneed()
        {
            GoodsGroupViewModel ggvm = new GoodsGroupViewModel();
            ggvm.Goods = new List<GoodsViewModel>();
            ggvm.GoodsClass = new List<GoodsClassViewModel>();
            var q = (from o in db.Cases
                     join g in db.Goods on o.CaseID equals g.CaseID
                     where o.StatusID == 5
                     orderby g.GdsID descending
                     select g).ToList();
            foreach (Goods Gdb in (q))
            {
                GoodsViewModel gvm = new GoodsViewModel()
                {
                    CaseID = Gdb.CaseID,
                    GdsName = Gdb.GdsName,
                    MemberID = Gdb.Cases.MemberID,
                    GdsCount = Gdb.GdsCount,
                    NickName = Gdb.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(Gdb.CaseID).StatusID).StatusName,
                    CaseTitle = Gdb.Cases.CaseTitle,
                    GdsClass = Gdb.GdsSubClass.GoodsClass.GdsClass
                };
                ggvm.Goods.Add(gvm);
            }

            IQueryable<string> gc = db.GoodsClass.Select(x => x.GdsClass).Distinct();
            foreach (string GCdb in gc)
            {
                GoodsClassViewModel gcvm = new GoodsClassViewModel();
                gcvm.GdsClass = GCdb;
                ggvm.GoodsClass.Add(gcvm);
            }

            return View(ggvm);
        }
        public ActionResult Goodsgive()
        {
            GoodsGroupViewModel ggvm = new GoodsGroupViewModel();
            ggvm.Goods = new List<GoodsViewModel>();
            ggvm.GoodsClass = new List<GoodsClassViewModel>();
            var q = (from o in db.Cases
                     join g in db.Goods on o.CaseID equals g.CaseID
                     where o.StatusID == 4
                     orderby g.GdsID descending
                     select g).ToList();
            foreach (Goods Gdb in (q))
            {
                GoodsViewModel gvm = new GoodsViewModel()
                {
                    CaseID = Gdb.CaseID,
                    GdsName = Gdb.GdsName,
                    MemberID = Gdb.Cases.MemberID,
                    GdsCount = Gdb.GdsCount,
                    NickName = Gdb.Cases.Member.NickName,
                    StatusName = db.CaseStatus.Find(db.Cases.Find(Gdb.CaseID).StatusID).StatusName,
                    CaseTitle = Gdb.Cases.CaseTitle,
                    GdsClass = Gdb.GdsSubClass.GoodsClass.GdsClass
                };
                ggvm.Goods.Add(gvm);
            }

            IQueryable<string> gc = db.GoodsClass.Select(x => x.GdsClass).Distinct();
            foreach (string GCdb in gc)
            {
                GoodsClassViewModel gcvm = new GoodsClassViewModel();
                gcvm.GdsClass = GCdb;
                ggvm.GoodsClass.Add(gcvm);
            }

            return View(ggvm);
        }

        public ActionResult BytesImage(int id) // 案件是否一定要圖片?
        {
            byte[] img = db.Picture.Where(x => x.CaseID == id).Select(x => x.Images).FirstOrDefault();
            if (img != null)
                return File(img, "image/jpeg");
            return Redirect("~/Areas/TotalCase/piccoloUI/img/gallery/gallery-img-1-4col.jpg");
        }

        
        public ActionResult GoodsCreate(IEnumerable<HttpPostedFileBase> files)
        {
            //int mid = Convert.ToInt32(Request.Cookies["MEMBERID"].Value);

            int mid = (int)Session["MEMBERID"];
            #region
            List<GoodsClass> gc = new List<GoodsClass>();
            GoodsClass first = new GoodsClass();
            first.GdsClassID = 10000;
            first.GdsClass = "請選擇類別";
            gc.Add(first);
            var q = db.GoodsClass;
            foreach (var a in q)
            {
                gc.Add(a);

            }
            List<GdsSubClass> gsc = new List<GdsSubClass>();
            GdsSubClass sbfirst = new GdsSubClass();
            sbfirst.GdsSubClassID = 10000;
            sbfirst.GdsSubClass1 = "請先選擇大類別";
            gsc.Add(sbfirst);
            ViewBag.GdsClassID = new SelectList(gc, "GdsClassID", "GdsClass", first.GdsClassID);
            ViewBag.GdsSubClassID = new SelectList(gsc, "GdsSubClassID", "GdsSubClass1", sbfirst.GdsSubClassID);

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
                    Recommendation = 0,
                    Location = Request.Form["Location"],
                    RegionID = Convert.ToInt32(Request.Form["RegionID"]),
                    Contact = Request.Form["Contact"],
                    StatusID = Convert.ToInt32(Request.Form["StatusID"]),
                });
                db.SaveChanges();

                int cid = db.Cases.Where(c => c.MemberID == mid).OrderByDescending(c => c.CaseID).First().CaseID;
                db.Goods.Add(new Goods
                {
                    CaseID = cid,
                    GdsPoint = 0,
                    GdsName = Request.Form["CaseContent"],
                    GdsCount = Convert.ToInt32(Request.Form["GdsCount"]),
                    GdsDeadline = Convert.ToDateTime(Request.Form["GdsDeadline"]),
                    GdsSubClassID = Convert.ToInt32(Request.Form["GdsSubClassID"]),



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

                return RedirectToAction("BrowseGoodsAll");
            }
            else
            {
                return View();
            }












        }

        public ActionResult GoodsRead(int id )//caseid
        {
            CaseGoods cg = new CaseGoods();

            if (id == 0)
            {
                id = (int)Session["CaseID"];
            }
            var q = db.Cases.Find(id);
            if (Session["MEMBERID"]!=null)
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
            var g = db.Goods.Where(m => m.CaseID == q.CaseID).First();
            cg.GdsName = g.GdsName;
            cg.GdsCount = g.GdsCount;
            cg.GdsPoint = g.GdsPoint;
            cg.GdsDeadline = g.GdsDeadline;
            var sub = db.GdsSubClass.Find(g.GdsSubClassID);
            cg.GdsSubClass1 = sub.GdsSubClass1; //小分類
            cg.GdsClass = db.GoodsClass.Find(sub.GdsClassID).GdsClass;//大分類
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
                cvm.Photo= db.Member.Find(c.MemberID).Photo;
                cvm.MessageContent = c.MessageContent;
                cvm.MessageDateTime = c.MessageDateTime;
                cvm.AuthorRepeat = c.AuthorRepeat;

                cv.Add(cvm);

            }
            ViewBag.ContentData = cv;


            MemberCaseCount mcc = new MemberCaseCount();


            MemberSimpleProfile msp = new MemberSimpleProfile();
            int memberid = q.MemberID;

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
        public ActionResult MemberContent(int id, string Content )
        {
           

            //========存值
            if (Content!=null)
            {
                Content cont = new Content();
                cont.CaseID = id;
                cont.MemberID = (int)Session["MEMBERID"];
                cont.MessageContent = Content;
                cont.MessageDateTime = DateTime.Now;

                db.Content.Add(cont);
                db.SaveChanges();
            }

            //return RedirectToAction("ajaxcontent", )
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
        
        public ActionResult TotalCaseManyImages() //部份檢視  秀多張圖
        {
            List<Picture> _pictures = new List<Picture>();
            int cas = (int)Session["CaseID"];
            var q = db.Picture.Where(m => m.CaseID == cas);
            foreach (var a in q)
            {
                Picture picture = new Picture();
                picture.ImageID = a.ImageID;
                _pictures.Add(picture);
            }
            return PartialView(_pictures);
        }

    
        public ActionResult BytesImage2(int id = 0)//ImageID   秀圖
        {
            var q = db.Picture.Find(id);
            byte[] img = q.Images;

            return File(img, "image/jpeg");
        }

        buybutton b = new buybutton();
        public ActionResult DetailtoCart(int caseid, int quantity)
        {


            int scid;
            int mid;
            if (Session["MEMBERID"] == null)
            {

                return RedirectToAction("Login", "Member", new { area = "MyMember" });

            }
            else
            {
                mid =(int) Session["MEMBERID"];
            }





            if (Session["shoppingcardID"] == null)
            {
                scid = b.buycheck(mid);
                Session["shoppingcardID"] = scid;

            }
            else
            {
                scid = (int)Session["shoppingcardID"];

            }

            b.SaveinCart(mid, caseid, quantity, 0, scid);
            return RedirectToAction("BrowseGoodsAll", "Goods", new { area = "TotalCase" });
            //return RedirectToAction("CartDatainOrders", "Recipient", new { area = "ShoppingCart" });

        }

        //public ActionResult MyClass(int id)
        //{
        //    int scid = db.GdsSubClass.Find(id).GdsClassID;
        //    return Json(db.GoodsClass.Where(c => c.GdsClassID == scid).Select(r => new { GdsClassID = r.GdsClassID, GdsClass = r.GdsClass }), JsonRequestBehavior.AllowGet);
        //}

    }
}