using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.MyCaseCenter.Controllers
{
    public class HomeController : Controller
    {
        static string _content = string.Empty;
        void SaveToDb(string content)
        {
            //模擬寫入DB
            _content = content;
        }
        string ReadFromDb()
        {
            //模擬由DB讀取
            return _content;
        }
        // GET: MyCaseCenter/Home//////////////////////////////////////////////////////////////  html寄信 專用
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Submit(string content)//html的內容
        {
            string fromf = Request.Form["aaa"];
            string toto = Request.Form["bbb"];
            string subject1= Request.Form["ccc"];
            EmailEmail(fromf,content,toto,subject1);
            SaveToDb(content);
            //return RedirectToAction("Result");
            return RedirectToAction("Index", "Home",new { area = "test" });
        }

        public ActionResult Result()
        {
            ViewBag.Content = ReadFromDb();
            return View();
        }


        public void EmailEmail(string fromf, string content,string E,string subject1)//發送電子郵件 方法
        {
            string smtpAddress = "smtp.gmail.com";
            //設定Port
            int portNumber = 587;
            bool enableSSL = true;
            //填入寄送方email和密碼
            string emailFrom = "MSIT115bestone@gmail.com"; //fromf //網站信箱  不要動
            string password = "!QAZ3edc5tgb";
            //收信方email
            string emailTo = E;
            //主旨
            string subject = "幫幫你幫幫我 公益互助網 ---->通知 " + subject1;
            //內容
            string body = "✽此郵件是系統自動發送，請勿直接回覆此郵件！<br>   這個訊息是發自 幫幫你幫幫我公益互助網<br><br>" + "您好：<br>   " +
                content + " <br><br><br>✽如有任何問題，請您至客服中心與我們聯絡。<br>   感謝您對本網站的支持，敬祝您　平安順心！";
            
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
    }
}