using Msit115BestOne.Areas.MyCaseCenter.Models.Partial;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Msit115BestOne.Areas.MyCaseCenter.Models;
using System.Net.Mail;
using System.Net;
using Msit115BestOne.Areas.MyMember.Models;

namespace Msit115BestOne.Areas.MyCaseCenter.Controllers
{
    public class MyManPowerController : Controller
    {
        private ManPowerEntities db = new ManPowerEntities();
        // GET: MyCaseCenter/MyManPower
        public ActionResult Index()  //討論區用的樣板  view
        {
            //Session["CaseIDD"] = null;
            //if (Session["MEMBERID"] == null)
            //{
            //    Session["MEMBERID"] = 1;//先寫死
            //}
            //int memberid = (int)Session["MEMBERID"];
            //List<CaseManPower> cms = new List<CaseManPower>();
            //var q = from o in db.Cases
            //        join m in db.ManPower on o.CaseID equals m.CaseID
            //        where o.MemberID == memberid
            //        orderby o.StartDateTime
            //        select o;
            //foreach (var a in q)
            //{
            //    CaseManPower cm = new CaseManPower();
            //    cm.CaseID = a.CaseID;
            //    cm.CaseTitle = a.CaseTitle;
            //    cm.StartDateTime = a.StartDateTime;
            //    int caseid = a.CaseID;
            //    int stasid = a.StatusID;
            //    cm.StatusName = db.CaseStatus.Find(stasid).StatusName;
            //    var q2 = db.ManPower.Where(m => m.CaseID == caseid).Select(m => m);
            //    foreach (var x in q2)
            //    {
            //        cm.MPName = x.MPName;
            //        cm.MPPoint = x.MPPoint;
            //        cm.MPNeedCount = x.MPNeedCount;        //需要的人數
            //        cm.MPActuralCount = x.MPActuralCount;  //實際徵求到的人數
            //        cm.MPTime = x.MPTime;              //徵求時間
            //        cm.MPDate = x.MPDate;              //徵求日期
            //    }
            //    cms.Add(cm);
            //}
            //return View(cms);
            return View();
        }
        public ActionResult Indexx()  //我的人力服務瀏覽
        {
            Session["CaseIDD"] = null;
            if (Session["MEMBERID"] == null)
            {
                Session["MEMBERID"] = 3;//先寫死
            }
            int memberid = (int)Session["MEMBERID"];

            List<CaseManPower> cms = new List<CaseManPower>();
            var q = (from o in db.Cases
                     join m in db.ManPower on o.CaseID equals m.CaseID
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

                CaseManPower cm = new CaseManPower();
                cm.CaseID = a.CaseID;
                cm.CaseTitle = a.CaseTitle;
                cm.StartDateTime = a.StartDateTime;
                int caseid = a.CaseID;
                int stasid = a.StatusID;
                cm.StatusName = db.CaseStatus.Find(stasid).StatusName;
                var q2 = db.ManPower.Where(m => m.CaseID == caseid).Select(m => m);
                foreach (var x in q2)
                {
                    cm.MPName = x.MPName;
                    cm.MPPoint = x.MPPoint;
                    cm.MPNeedCount = x.MPNeedCount;        //需要的人數
                    cm.MPActuralCount = x.MPActuralCount;  //實際徵求到的人數
                    cm.MPTime = x.MPTime;              //徵求時間
                    cm.MPDate = x.MPDate;              //徵求日期
                }
                cms.Add(cm);
            }

            //MemberCaseCount mcc = new MemberCaseCount();
            //var t = mcc.casecount(memberid);
            int ccount = db.Cases.Where(o => o.MemberID == memberid).Count();
            ViewBag.allcase = ccount;
            ViewBag.MPcase = q.Count();

            ViewBag.Count = oco;

            return View(cms);
        }
        public ActionResult BytesImage(int id = 0)//CaseID   秀圖
        {
            byte[] img = null;
            var p = db.Picture.Where(m => m.CaseID == id);
            foreach (var a in p)
            {
                img = a.Images;
            }

            return File(img, "image/jpeg");
        }
        public ActionResult BytesImage2(int id = 0)//ImageID   秀圖
        {
            var q = db.Picture.Find(id);
            byte[] img = q.Images;

            return File(img, "image/jpeg");
        }
        public ActionResult LoadMan(int id = 0)//CaseID  完整資訊  單筆 單圖
        {
            CaseManPower cm = new CaseManPower();
            if (id == 0)
            {
                id = (int)Session["CaseIDD"];
            }
            var q = db.Cases.Find(id);

            cm.CaseID = q.CaseID;
            Session["CaseIDD"] = q.CaseID;
            cm.CaseTitle = q.CaseTitle;
            cm.CaseContent = q.CaseContent;
            cm.StartDateTime = q.StartDateTime;
            cm.Location = q.Location; //地址還不完整!!!!!!!
            cm.Contact = q.Contact;
            cm.StatusID = q.StatusID;
            cm.StatusName = db.CaseStatus.Find(q.StatusID).StatusName; //狀態
            var g = db.ManPower.Where(m => m.CaseID == q.CaseID).First();
            cm.MPName = g.MPName;
            cm.MPTime = g.MPTime;
            cm.MPDate = g.MPDate;
            cm.MPNeedCount = g.MPNeedCount;     //需要的人數
            cm.MPActuralCount = g.MPActuralCount;  //實際徵求到的人數
            var sub = db.MPSubClass.Find(g.MPSubClassID);
            cm.MPSubClass1 = sub.MPSubClass1; //小分類
            cm.MPClass1 = db.MPClass.Find(sub.MPClassID).MPClass1;//大分類
            var re = db.Region.Find(q.RegionID);
            cm.RegionName = re.RegionName;                         //區
            cm.CityName = db.City.Find(re.CityID).CityName;   //縣市
            int count = db.Picture.Where(o => o.CaseID == id).Count();
            if (count > 0)
            {
                var pimg = db.Picture.Where(m => m.CaseID == id).First();
                cm.ImageID = pimg.ImageID;
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

                string mpclassname = "";
                var mpl = db.MPSCList.Where(o => o.MemberID == mem).ToList();  //每個會員的多個小專長ID
                foreach (var b in mpl)
                {
                    string MPSub = db.MPSubClass.Find(b.MPSubClassID).MPSubClass1;
                    mpclassname = mpclassname + MPSub + " ";
                }
                _order.MPSubClass1 = mpclassname;

                che.Add(_order);
            }
            ViewBag.ContentMember = che;

            return View(cm);
        }

        public ActionResult EditMan(int id) //CaseID 修改的頁面 先讀取單筆
        {
            CaseManPower cg = new CaseManPower();
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
            var g = db.ManPower.Where(m => m.CaseID == q.CaseID).First();
            cg.MPID = g.MPID;
            cg.MPName = g.MPName;
            cg.MPNeedCount = g.MPNeedCount;
            cg.MPActuralCount = g.MPActuralCount;
            cg.MPTime = g.MPTime;
            cg.MPDate = g.MPDate;
            var sub = db.MPSubClass.Find(g.MPSubClassID);
            cg.MPSubClass1 = sub.MPSubClass1; //小分類
            cg.MPClass1 = db.MPClass.Find(sub.MPClassID).MPClass1;//大分類
            var re = db.Region.Find(q.RegionID);
            cg.RegionName = re.RegionName;                         //區
            cg.CityName = db.City.Find(re.CityID).CityName;   //縣市
            int count = db.Picture.Where(o => o.CaseID == id).Count();
            if (count > 0)
            {
                var pimg = db.Picture.Where(m => m.CaseID == id).FirstOrDefault();
                cg.ImageID = pimg.ImageID;
            }

            Cases _case = db.Cases.Find(id);
            Region region = db.Region.Find(q.RegionID);
            ViewBag.RegionID = new SelectList(db.Region.Where(o => o.CityID == region.CityID), "RegionID", "RegionName", _case.RegionID);
            ViewBag.CityID = new SelectList(db.City, "CityID", "CityName", region.CityID);
            ManPower mans = db.ManPower.Find(g.MPID);
            MPSubClass subclass = db.MPSubClass.Find(g.MPSubClassID);
            ViewBag.MPSubClassID = new SelectList(db.MPSubClass.Where(o => o.MPClassID == subclass.MPClassID), "MPSubClassID", "MPSubClass1", mans.MPSubClassID);
            ViewBag.MPClassID = new SelectList(db.MPClass, "MPClassID", "MPClass1", subclass.MPClassID);

            return View(cg);
        }
        public ActionResult SubClass(int id)
        {
            return Json(db.MPSubClass.Where(c => c.MPClassID == id).Select(r => new { MPSubClassID = r.MPSubClassID, MPSubClass1 = r.MPSubClass1 }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RegionC(int id)
        {
            return Json(db.Region.Where(c => c.CityID == id).Select(r => new { RegionID = r.RegionID, RegionName = r.RegionName }), JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemovePicture(int id)//CaseID  清除這案件的圖
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
        public ActionResult EditMan(CaseManPower goodv, HttpPostedFileBase strPhoto)
        {
            if (strPhoto != null)
            {
                //string strPath = Request.PhysicalApplicationPath + "Areas\\Members\\MemberImages";
                //strPhoto.SaveAs(strPath + strPhoto.FileName);
                var imagesSize = strPhoto.ContentLength;
                byte[] imageByte = new byte[imagesSize];
                strPhoto.InputStream.Read(imageByte, 0, imagesSize);

                db.Picture.Add(new Picture { CaseID = goodv.CaseID, Images = imageByte });
                db.SaveChanges();
                TempData["message"] = "上傳成功";
            }
            else
            {
                TempData["message"] = "請先選檔案";

            }
            var c = db.Cases.Find(goodv.CaseID);
            c.CaseTitle = goodv.CaseTitle;
            c.CaseContent = goodv.CaseContent;
            c.Contact = goodv.Contact;
            c.Location = goodv.Location;
            c.RegionID = goodv.RegionID;
            var g = db.ManPower.Find(goodv.MPID);
            //g.MPName = goodv.MPName;
            g.MPNeedCount = goodv.MPNeedCount;
            g.MPActuralCount = goodv.MPActuralCount;
            g.MPSubClassID = goodv.MPSubClassID;
            g.MPDate = goodv.MPDate;
            g.MPTime = goodv.MPTime;
            db.SaveChanges();
            return RedirectToAction("LoadMan");
        }
        public ActionResult BytesImagess(int id = 0)//MemberID   秀人圖
        {
            var q = db.Member.Find(id);
            byte[] img = q.Photo;

            return File(img, "image/jpeg");
        }
        public ActionResult ManCheck(int? id)  //CaseID
        {
            List<OrderViewModel> che = new List<OrderViewModel>();
            var ord = (db.Orders.Where(c => (c.CaseID == id && c.OrderStatusID == 4) || (c.CaseID == id && c.OrderStatusID == 3))).ToList();//多筆
            foreach (var a in ord)
            {
                OrderViewModel _order = new OrderViewModel();
                int mem = a.MemberID;
                var mb = db.Member.Find(mem);
                _order.NickName = mb.NickName;
                _order.MemberID = mem;
                _order.Birthday = mb.Birthday;
                _order.Phone = mb.Phone;
                _order.Address = mb.Address;
                var re = db.Region.Find(mb.RegionID);
                _order.RegionName = re.RegionName;
                _order.CityName = db.City.Where(o => o.CityID == re.CityID).First().CityName;

                string mpclassname = "";
                var mpl = db.MPSCList.Where(o => o.MemberID == mem).ToList();  //每個會員的多個小專長ID
                foreach (var b in mpl)
                {
                    string MPSub = db.MPSubClass.Find(b.MPSubClassID).MPSubClass1;
                    mpclassname = mpclassname + MPSub + " ";
                }
                _order.MPSubClass1 = mpclassname;

                che.Add(_order);
            }
            var m = db.ManPower.Where(c => c.CaseID == id).First();
            ViewBag.CaseID = id;
            ViewBag.MPActuralCount = db.Orders.Where(c => (c.CaseID == id && c.OrderStatusID == 3) || (c.CaseID == id && c.OrderStatusID == 4)).Count();
            ViewBag.MPNeedCount = m.MPNeedCount - m.MPActuralCount;
            var ca = db.Cases.Find(id);
            ViewBag.StatusID = ca.StatusID;
            ViewBag.CaseTitle = ca.CaseTitle;
            return View(che);
        }
        public ActionResult CheckRight(int id, string[] Area)//CaseID   會員id
        {
            int m;
            int len = Area.Length; //陣列數量
            var qq = db.Cases.Find(id);
            int casestatusid = qq.StatusID;
            for (int i = 0; i < len; i++)  //核准迴圈
            {
                m = Convert.ToInt32(Area[i]);//會員id
                var q = db.Orders.Where(o => o.CaseID == id && o.MemberID == m).First();
                if (casestatusid == 7)
                {
                    int orderid = q.OrderID;
                    string message = "您已通過審核";
                    SendEmail(m, message, id, orderid);
                    q.OrderStatusID = 1005;  //改成人力核准狀態    多
                    q.FinishedDate = DateTime.Now;
                }
                else if (casestatusid == 8)
                {
                    int orderid = q.OrderID;
                    string message = "發文者已同意";
                    SendEmail(m, message, id, orderid);
                    q.OrderStatusID = 1004;  //改成人力啟用狀態     一
                    q.FinishedDate = DateTime.Now;
                }
                db.SaveChanges();
            }
            var q2 = db.Orders.Where(o => o.OrderStatusID != 1005 && o.CaseID == id).ToList();
            foreach (var a in q2)  //拒絕迴圈
            {
                a.OrderStatusID = 1008;
                a.FinishedDate = DateTime.Now;

            }
            //db.SaveChanges();
            var q3 = db.ManPower.Where(o => o.CaseID == id).First();
            int actcount = q3.MPActuralCount;
            q3.MPActuralCount = actcount + len;
            if (q3.MPNeedCount <= actcount + len)
            {
                db.Cases.Find(id).StatusID = 6;//結案
            }
            db.SaveChanges();
            //return RedirectPermanent("/MyManPower/LoadMan/");
            return Json(Url.Action("LoadMan", "MyManPower", new { Area = "MyCaseCenter" }));
        }
        public void SendEmail(int memberid, string mess, int caseid, int ord)//出貨 發送電子郵件給會員  方法
        {
            var q = db.Member.Find(memberid);
            string E = q.Email;
            string nick = q.NickName;
            var q2 = db.Cases.Find(caseid);
            string casetile = q2.CaseTitle;


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
            string subject = "幫幫你幫幫我 公益互助網 ----> 人力通知 編號" + ord;
            //內容
            string body;
            if (mess == "發文者已同意")
            {
                body = "✽此郵件是系統自動發送，請勿直接回覆此郵件！<br>   這個訊息是發自 幫幫你幫幫我公益互助網<br><br>親愛的會員 " + nick + "您好：<br>   " +
                casetile + "<br>   " + mess + " <br><br>   需求編號：" + ord + "<br><br>✽如有任何問題，請您至客服中心與我們聯絡。<br>   感謝您對本網站的支持，敬祝您　平安順心！";

            }
            else   //"您已通過審核"
            {
                body = "✽此郵件是系統自動發送，請勿直接回覆此郵件！<br>   這個訊息是發自 幫幫你幫幫我公益互助網<br><br>親愛的會員 " + nick + "您好：<br>   " +
                casetile + "<br>   " + mess + " <br><br>   需求編號：" + ord + "<br><br>✽如有任何問題，請您至客服中心與我們聯絡。<br>   感謝您對本網站的支持，敬祝您　平安順心！";

            }

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
        public ActionResult Answer(int id, string ans, int con) //會員ID  回覆字串 ContentID
        {
            var q = db.Content.Where(o => o.MemberID == id && o.ContentID == con).First();
            q.AuthorRepeat = ans;
            db.SaveChanges();
            return Json(Url.Action("LoadGood", "MyGood", new { Area = "MyCaseCenter" }));
        }

    }
}