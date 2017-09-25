using Msit115BestOne.Areas.MyCaseCenter.Models.Partial;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;  //include用
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.MyCaseCenter.Models;
using System.Net.Mail;
using System.Net;
using Msit115BestOne.Areas.MyMember.Models;

namespace Msit115BestOne.Areas.MyCaseCenter.Controllers
{
    public class MyGoodController : Controller
    {

        private ManPowerEntities db = new ManPowerEntities();
        // GET: MyCaseCenter/MyGood
        public ActionResult Index()      //我的物品瀏覽
        {
            Session["CaseID"] = null;
            if (Session["MEMBERID"] == null)
            {
                Session["MEMBERID"] = 12;//先寫死
            }
            int memberid = (int)Session["MEMBERID"];
            List<CaseGood> cgs = new List<CaseGood>();
            var q = (from o in db.Cases
                     join c in db.Goods on o.CaseID equals c.CaseID
                     where o.MemberID == memberid
                     orderby o.StartDateTime
                     select o).ToList();
            List<OrderCount> oco = new List<OrderCount>();
            foreach (var a in q)
            {
                OrderCount ocou = new OrderCount();
                ocou.CaseID = a.CaseID;
                ocou.CaseTitle = a.CaseTitle;
                ocou.Cou = db.Orders.Where(o => o.CaseID == a.CaseID).Count();
                oco.Add(ocou);

                CaseGood cg = new CaseGood();
                cg.CaseID = a.CaseID;
                cg.CaseTitle = a.CaseTitle;
                cg.StartDateTime = a.StartDateTime;
                int caseid = a.CaseID;
                int stasid = a.StatusID;
                cg.StatusName = db.CaseStatus.Find(stasid).StatusName;
                var q2 = db.Goods.Where(m => m.CaseID == caseid).Select(m => m);
                foreach (var x in q2)
                {
                    cg.GdsName = x.GdsName;
                    cg.GdsPoint = x.GdsPoint;
                    cg.GdsCount = x.GdsCount;
                }

                cgs.Add(cg);
            }


            //MemberCaseCount mcc = new MemberCaseCount();
            //var t = mcc.casecount(memberid);
            int ccount = db.Cases.Where(o => o.MemberID == memberid).Count();
            ViewBag.allcase = ccount;
            ViewBag.GDcase = q.Count();

            ViewBag.Count = oco;

            return View(cgs);
        }

        public ActionResult BytesImage(int id = 0)//CaseID   秀圖
        {

            byte[] img = null;
            var p = db.Picture.Where(m => m.CaseID == id).OrderByDescending(m => m.ImageID);
            foreach (var a in p)
            {
                img = a.Images;
            }

            return File(img, "image/jpeg");
        }
        public ActionResult LoadGood(int id = 0)//CaseID  完整資訊  單筆  //還需要部份檢視  秀多張圖
        {
            CaseGood cg = new CaseGood();
            if (id == 0)
            {
                id = (int)Session["CaseID"];
            }
            var q = db.Cases.Find(id);
            
            cg.CaseID = q.CaseID;
            Session["CaseID"] = q.CaseID;
            cg.CaseTitle = q.CaseTitle;
            cg.CaseContent = q.CaseContent;
            cg.StartDateTime = q.StartDateTime;
            cg.Location = q.Location; //地址還不完整!!!!!!!
            cg.Contact = q.Contact;
            cg.StatusID = q.StatusID;
            cg.StatusName = db.CaseStatus.Find(q.StatusID).StatusName; //狀態
            var g = db.Goods.Where(m => m.CaseID == q.CaseID).First();
            cg.GdsName = g.GdsName;
            cg.GdsCount = g.GdsCount;
            cg.GdsPoint = g.GdsPoint;
            var sub = db.GdsSubClass.Find(g.GdsSubClassID);
            cg.GdsSubClass1 = sub.GdsSubClass1; //小分類
            cg.GdsClass = db.GoodsClass.Find(sub.GdsClassID).GdsClass;//大分類
            var re = db.Region.Find(q.RegionID);
            cg.RegionName = re.RegionName;                         //區
            cg.CityName = db.City.Find(re.CityID).CityName;   //縣市
            int count = db.Picture.Where(o => o.CaseID == id).Count();
            if (count > 0)
            {
                var pimg = db.Picture.Where(m => m.CaseID == id).FirstOrDefault();
                cg.ImageID = pimg.ImageID;
            }

            List<ContentViewModel> che = new List<ContentViewModel>();
            var q3 = db.Content.Where(o => o.CaseID == id).OrderByDescending(o => o.MessageDateTime).ToList();
            foreach (var a in q3)
            {
                ContentViewModel _order = new ContentViewModel();
                int mem = a.MemberID;
                var mb = db.Member.Find(mem);
                _order.MessageContent = a.MessageContent;
                _order.ContentID = a.ContentID;
                _order.MessageDateTime = a.MessageDateTime;
                _order.AuthorRepeat = a.AuthorRepeat;
                _order.NickName = mb.NickName;
                _order.MemberID = mem;
                _order.Birthday = mb.Birthday;
                _order.Phone = mb.Phone;
                _order.Address = mb.Address;
                var reg = db.Region.Find(mb.RegionID);
                _order.RegionName = reg.RegionName;
                _order.CityName = db.City.Where(o => o.CityID == reg.CityID).First().CityName;
                string mpclassname="";
                var mpl = db.MPSCList.Where(o => o.MemberID == mem).ToList();  //每個會員的多個小專長ID
                foreach(var b in mpl)
                {
                    string MPSub = db.MPSubClass.Find(b.MPSubClassID).MPSubClass1;
                    mpclassname = mpclassname+ MPSub+" ";
                }
                _order.MPSubClass1 = mpclassname;

                che.Add(_order);
            }
            ViewBag.ContentMember = che;

            return View(cg);
        }
        public ActionResult ManyImages() //部份檢視  秀多張圖
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
        public ActionResult EditGood(int id) //CaseID 修改的頁面 先讀取單筆
        {
            CaseGood cg = new CaseGood();
            var q = db.Cases.Find(id);
            cg.CaseID = q.CaseID;
            Session["CaseID"] = q.CaseID;
            cg.CaseTitle = q.CaseTitle;
            cg.CaseContent = q.CaseContent;
            cg.StartDateTime = q.StartDateTime;
            cg.Location = q.Location; //地址還不完整!!!!!!!
            cg.Contact = q.Contact;
            cg.StatusName = db.CaseStatus.Find(q.StatusID).StatusName; //狀態
            var g = db.Goods.Where(m => m.CaseID == q.CaseID).First();
            cg.GdsID = g.GdsID;
            cg.GdsName = g.GdsName;
            cg.GdsCount = g.GdsCount;
            cg.GdsPoint = g.GdsPoint;
            var sub = db.GdsSubClass.Find(g.GdsSubClassID);
            cg.GdsSubClass1 = sub.GdsSubClass1; //小分類
            cg.GdsClass = db.GoodsClass.Find(sub.GdsClassID).GdsClass;//大分類
            var re = db.Region.Find(q.RegionID);
            cg.RegionName = re.RegionName;                         //區
            cg.CityName = db.City.Find(re.CityID).CityName;   //縣市

            int count = db.Picture.Where(o => o.CaseID == id).Count();
            if (count > 0)
            {
                var pimg = db.Picture.Where(m => m.CaseID == id).FirstOrDefault();
                cg.ImageID = pimg.ImageID;
            }


            Goods goods = db.Goods.Find(g.GdsID);
            GdsSubClass subclass = db.GdsSubClass.Find(g.GdsSubClassID);
            ViewBag.GdsSubClassID = new SelectList(db.GdsSubClass.Where(o => o.GdsClassID == subclass.GdsClassID), "GdsSubClassID", "GdsSubClass1", goods.GdsSubClassID);
            ViewBag.GdsClassID = new SelectList(db.GoodsClass, "GdsClassID", "GdsClass", subclass.GdsClassID);
            Cases _case = db.Cases.Find(id);
            Region region = db.Region.Find(q.RegionID);
            ViewBag.RegionID = new SelectList(db.Region.Where(o => o.CityID == region.CityID), "RegionID", "RegionName", _case.RegionID);
            ViewBag.CityID = new SelectList(db.City, "CityID", "CityName", region.CityID);

            ViewBag.count = count;

            return View(cg);
        }
        public ActionResult SubClass(int id)
        {
            return Json(db.GdsSubClass.Where(c => c.GdsClassID == id).Select(r => new { GdsSubClassID = r.GdsSubClassID, GdsSubClass1 = r.GdsSubClass1 }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegionC(int id)
        {
            return Json(db.Region.Where(c => c.CityID == id).Select(r => new { RegionID = r.RegionID, RegionName = r.RegionName }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveAllPicture(int id)//CaseID  清除這案件的圖
        {
            var q = db.Picture.Where(o => o.CaseID == id);
            foreach (var a in q)
            {
                db.Picture.Remove(a);
            }
            db.SaveChanges();
            return Json(db.Region.Where(c => c.CityID == id).Select(r => new { RegionID = r.RegionID, RegionName = r.RegionName }), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult EditGood(CaseGood goodv, IEnumerable<HttpPostedFileBase> files)
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

                        db.Picture.Add(new Picture { CaseID = goodv.CaseID, Images = imageByte });
                        db.SaveChanges();
                        TempData["message"] = "上傳成功";
                    }
                    else
                    {
                        TempData["message"] = "請先選檔案";
                    }
                }
            }
            var c = db.Cases.Find(goodv.CaseID);
            c.CaseTitle = goodv.CaseTitle;
            c.CaseContent = goodv.CaseContent;
            c.Contact = goodv.Contact;
            c.Location = goodv.Location;
            c.RegionID = goodv.RegionID;
            var g = db.Goods.Find(goodv.GdsID);
            //g.GdsName = goodv.GdsName;
            g.GdsCount = goodv.GdsCount;
            g.GdsPoint = 0;
            g.GdsSubClassID = goodv.GdsSubClassID;

            db.SaveChanges();
            return RedirectToAction("LoadGood");
        }
        public ActionResult GoodOrder(int? id)//CaseID 這CASE的所有訂購單
        {
            //var orders123 = db.Orders.Where(a=>a.CaseID==id).Include(o => o.Cases).Include(o => o.OrderStatus);
            //return View(orders123.ToList());
            List<OrderViewModel> orders = new List<OrderViewModel>();
            var q = (db.Orders.Where(c => c.CaseID == id)).ToList();  //多筆
            foreach (var a in q)
            {
                OrderViewModel _order = new OrderViewModel();
                _order.OrderID = a.OrderID;
                _order.CaseID = (int)a.CaseID;
                _order.Quantity = (int)a.Quantity;
                _order.Point = a.Point;
                _order.OrderDate = a.OrderDate;
                _order.FinishedDate = a.FinishedDate;//結束日期 可能出錯
                _order.OrderStatusID = a.OrderStatusID;
                _order.MemberID = a.MemberID;
                _order.NickName = db.Member.Find(a.MemberID).NickName;
                _order.CaseTitle = db.Cases.Find(id).CaseTitle;
                var goo = db.Goods.Where(c => c.CaseID == id).First();
                _order.GdsName = goo.GdsName;
                _order.GdsCount = goo.GdsCount;
                _order.OrderStatus1 = db.OrderStatus.Find(a.OrderStatusID).OrderStatus1;
                orders.Add(_order);
            }
            return View(orders);
        }
        public void EmailEmail(int memberid, string mess, int caseid, int ord)//出貨 發送電子郵件給會員  方法
        {
            var q = db.Member.Find(memberid);
            string E = q.Email;
            string nick = q.NickName;
            var q2 = db.Cases.Find(caseid);
            string casetile = q2.CaseTitle;
            var q3 = db.Goods.Where(o => o.CaseID == caseid).First();
            int count = (int)q3.GdsCount;

            string smtpAddress = "smtp.gmail.com";
            //設定Port
            int portNumber = 587;
            bool enableSSL = true;
            //填入寄送方email和密碼
            string emailFrom = "MSIT115bestone@gmail.com"; //網站信箱  不要動
            string password = "!QAZ3edc5tgb";
            //收信方email
            string emailTo = E;
            //主旨
            string subject = "幫幫你幫幫我 公益互助網 ----> 物流通知 編號" + ord;
            //內容
            string body = "✽此郵件是系統自動發送，請勿直接回覆此郵件！<br>   這個訊息是發自 幫幫你幫幫我公益互助網<br><br>親愛的會員 " + nick + "您好：<br>   " +
                casetile + "<br>   數量： " + count + "<br>" + mess + " <br><br>   需求編號：" + ord + "<br><br>✽如有任何問題，請您至客服中心與我們聯絡。<br>   感謝您對本網站的支持，敬祝您　平安順心！";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(emailFrom);
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                var message = new System.Net.Mail.MailMessage();
                message.Body = body;
                mail.Body = message.Body;
                using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                {
                    smtp.Credentials = new NetworkCredential(emailFrom, password);
                    smtp.EnableSsl = enableSSL;
                    smtp.Send(mail);
                }
            }
        }
        public ActionResult OrderOK(int id, int Q)  //orderid 購買數量  X購買總金額  
        {
            var ord = db.Orders.Find(id);
            if (ord.OrderStatusID == 1)
            {
                string message = " 貨物已送出 !! ";
                EmailEmail(ord.MemberID, message, (int)ord.CaseID, id);//送信方法
                ord.OrderStatusID = 1006;            //物品等待PO文者捐贈給我---->物品已捐贈
            }
            else if (ord.OrderStatusID == 2)
            {
                string message = " 貨物已收到 !! ";
                EmailEmail(ord.MemberID, message, (int)ord.CaseID, id);//送信方法
                ord.OrderStatusID = 1007;            //物品等待PO文者接收---->物品已接收
            }
            ord.FinishedDate = DateTime.Now;//訂單完成時間

            int casid = (int)ord.CaseID;
            var cas = db.Cases.Find(casid);
            var goo = db.Goods.Where(g => g.CaseID == casid).First();
            int gdsc = (int)goo.GdsCount;
            if ((gdsc - Q) <= 0)
            {
                cas.StatusID = 6;                         //如果庫存0  結案
                cas.EndDateTime = DateTime.Now;  //且紀錄結案時間
            }
            goo.GdsCount = gdsc - Q; //庫存-購買數量

            int memid = cas.MemberID;
            var mem = db.Member.Find(memid);
            int poi = mem.PointCount;
            mem.PointCount = 0 + poi;  //加賣家錢XX
            db.SaveChanges();
            //-------------------------------------------------------------
            //要傳一些資訊到view

            return Json(db.Orders.Where(r => r.OrderID == id).Select(r => new { OrderStatusID = r.OrderStatusID, FinishedDate = r.FinishedDate }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderNO(int id, int Q)  //orderid 購買數量  X購買總金額
        {
            var ord = db.Orders.Find(id);
            ord.OrderStatusID = 1008;            //待出貨---->已取消
            ord.FinishedDate = DateTime.Now; //訂單取消時間

            //int casid = (int)ord.CaseID;
            //var cas = db.Cases.Find(casid);
            //var goo = db.Goods.Where(g => g.CaseID == casid).First();
            //int gdsc = (int)goo.GdsCount;
            //if (gdsc == 0)
            //{
            //    cas.StatusID = 4;                         //如果庫存0 加數量 開啟案件

            //}
            //goo.GdsCount = gdsc + Q; //庫存+購買數量
            db.SaveChanges();

            return Json(db.Orders.Where(r => r.OrderID == id).Select(r => new { OrderStatusID = r.OrderStatusID, FinishedDate = r.FinishedDate }), JsonRequestBehavior.AllowGet);/////
        }
        public ActionResult Answerg(int id, string ans, int con) //會員ID  回覆字串  ContentID
        {
            var q = db.Content.Where(o => o.MemberID == id && o.ContentID == con).First();
            q.AuthorRepeat = ans;
            db.SaveChanges();
            return Json(Url.Action("LoadMan", "MyManPower", new { Area = "MyCaseCenter" }));
        }
    }
}