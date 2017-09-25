using Msit115BestOne.Areas.MyMember.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.MyMember.Controllers
{
    public class MemberController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        // GET: MyMember/Member
        int id;
        string nm;


        public ActionResult Login()
        {

            return View();


        }
        public ActionResult Logout()
        {
            //Response.Cookies["MEMBERID"].Value = "";
            Session["MEMBERID"] = null;
            Session["Lo"] = 10;

            return RedirectToAction("Index", "Home", new { area = "" });


        }
        [HttpPost]
        public ActionResult Login(string Account, string Password)
        {
            int lo = (int)Session["Lo"];
            //接收Home表單傳回來的值
            string a = Account;
            string p = Password;
            //把值送到資料庫去找看有沒有符合
            var q = (from m in db.Member
                     where m.MAccount == a & m.MPassword == p
                     select m.MemberID).Count();

            var s = db.Member.Where(m => m.MAccount == a & m.MPassword == p);
            id = s.Select(m => m.MemberID).FirstOrDefault();
            nm = s.Select(m => m.NickName).FirstOrDefault();

            if (q > 0)
            {
                //Response.Cookies["MEMBERID"].Value = id.ToString();
                //int mid = Convert.ToInt32( Request.Cookies["MEMBERID"].Value);
                Session["MEMBERID"] = id;
                Session["NICKNAME"] = nm;
            
                if (lo == 3)
                {

                    return RedirectToAction("ManPowerAll", "ManPowers", new { area = "TotalCase" });


                }
                else if (lo == 1)
                {
                    return RedirectToAction("BrowseGoodsAll", "Goods", new { area = "TotalCase" });


                }
                else if (lo == 2)
                {

                    return RedirectToAction("Index", "Home", new { area = "Calender" });

                }
                else
                {

                    return RedirectToAction("Index", "Home", new { area = "" });
                }



            }
            else
            {
                TempData["error"] = "帳密有誤";
                return RedirectToAction("Login");
            }
        }



        public ActionResult Create()
        {
        
          


            return View();

        }

        [HttpPost]
        public ActionResult Create(Member m, HttpPostedFileBase strPhoto )
        {
            string message = "";
            if (ModelState.IsValid)
            {
                if (strPhoto != null)
                {
                    //string strPath = Request.PhysicalApplicationPath + "Areas\\Members\\MemberImages";

                    //strPhoto.SaveAs(strPath + strPhoto.FileName);

                    var imagesSize = strPhoto.ContentLength;
                    byte[] imageByte = new byte[imagesSize];
                    strPhoto.InputStream.Read(imageByte, 0, imagesSize);

                    m.Photo = imageByte;
                    m.Stage = 1;
                    m.EXP = 0;
                    m.PointCount = 0;

                    db.Member.Add(m);
                    db.SaveChanges();

                    message = "註冊成功";
                    ViewBag.Message = message;
                    //return RedirectToAction("Index", "Home", new { area = "" });
                    return RedirectToAction("Login");
                }
                else
                {
                    message = "請選擇圖檔";
                    ViewBag.Message = message;
                    return View();

                }

            }
            else
            {
                message = "註冊失敗";
                ViewBag.Message = message;
            }






            return RedirectToAction("Login");

        }
        public ActionResult BytesImage(int id)
        {
            Member member = db.Member.Find(id);
            byte[] img = member.Photo;
            return File(img, "image/jpeg");
        }

        public ActionResult GetCity()
        {
            var city = db.City.Select(c => new { CityID = c.CityID, CityName = c.CityName });
            return Json(city, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetRegion(int CityID)
        {
            var region = db.Region.Where(r => r.CityID == CityID).Select(r => new { RegionID = r.RegionID, RegionName = r.RegionName });
            return Json(region, JsonRequestBehavior.AllowGet);
        }



        public ActionResult MemberProfile()
        {
            MemberCaseCount cc = new MemberCaseCount();
            
            int mid =(int)Session["MEMBERID"];

          var acc=  cc.casecount(mid);
            //int mid = 13;
            Member m = new Member();
            var q = db.Member.Find(mid);
            var regionname = db.Region.Find(q.RegionID).RegionName;
            var cityname = db.City.Find(db.Region.Find(q.RegionID).CityID).CityName;

            Memberview mp = new Memberview();
            mp.MemberID = q.MemberID;
            mp.LastName = q.LastName;
            mp.FirstName = q.FirstName;
            mp.Birthday = q.Birthday;
            mp.Email = q.Email;
            mp.CityName = cityname;
            mp.RegionName = regionname;
            mp.Address = q.Address;
            mp.Title = db.MemberStage.Find(q.Stage).Title;
            mp.NickName = q.NickName;
            mp.Phone = q.Phone;
            mp.Photo = q.Photo;

            mp.sumpoint= acc[0];
            mp.casecount = acc[1];
            mp.GDcasecount = acc[2];
            mp.MPcasecount = acc[3];
            mp.GDcasegive = acc[4];
            mp.GDcaseneed = acc[5];
            mp.MPcasegive = acc[6];
            mp.MPcaseneed = acc[7];
           
            //===全部專長
            var sk = db.MPSubClass;
            ViewBag.skill = sk;
            //===============

            //我的專長轉中文=======
            var msk = db.MPSCList.Where(p => p.MemberID == mid).Select(p=>p.MPSubClassID).ToList(); //轉中文
            List<string> myskillname = new List<string>();
            foreach(var a in msk)
            {
               var mk = db.MPSubClass.Where(p => p.MPSubClassID == a).Select(p => p.MPSubClass1).First();
                myskillname.Add(mk);


            }

            ViewBag.Myskill = myskillname;

            //========================
            return View(mp);





            //allcasecount.Add(sumpoint); //0
            //allcasecount.Add(casecount); //1
            //allcasecount.Add(GDcasecount); //2
            //allcasecount.Add(MPcasecount); //3 
            //allcasecount.Add(GDcasegive); //4
            //allcasecount.Add(GDcaseneed); //5
            //allcasecount.Add(MPcasegive); //6
            //allcasecount.Add(MPcaseneed); //7





        }

        public ActionResult GetPhoto()//登入會員自己的大頭照
        {
            //int mid = Convert.ToInt32(Request.Cookies["MEMBERID"].Value);

            int mid =    (int)Session["MEMBERID"];
        
            var q = db.Member.Find(mid);
            byte[] img = q.Photo;

            return File(img, "image/jpeg");
        }
        public ActionResult GetOtherPhoto(int id)//別人的大頭照
        {
            int mid = id;

            var q = db.Member.Find(mid);
            byte[] img = q.Photo;

            return File(img, "image/jpeg");
        }


        public ActionResult Edit(int? id, HttpPostedFileBase strPhoto)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aaa = db.Member.Find(id);
            Memberview member = new Memberview();
            member.MemberID = aaa.MemberID;
            member.FirstName = aaa.FirstName;
            member.LastName = aaa.LastName;
            member.MAccount = aaa.MAccount;
            member.Birthday = aaa.Birthday;
            member.Address = aaa.Address;
            member.Phone = aaa.Phone;
            member.Email = aaa.Email;
            member.MPassword = aaa.MPassword;
            member.Stage = aaa.Stage;
            member.EXP = aaa.EXP;
            member.Phone = aaa.Phone;
            member.PointCount = aaa.PointCount;
            member.NickName = aaa.NickName;

            //member.RegionID = aaa.RegionID;

            Region region = db.Region.Find(aaa.RegionID);
            ViewBag.RegionID = new SelectList(db.Region.Where(o => o.CityID == region.CityID), "RegionID", "RegionName", aaa.RegionID);
            ViewBag.CityID = new SelectList(db.City, "CityID", "CityName", region.CityID);

            //member.RegionName = Request.Form["RegionName"];
            //member.CityName = Request.Form["CityName"];
            member.Photo = aaa.Photo;



            //member.CityID = Convert.ToInt32(Request.Form["CityID"]);



            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.Stage = new SelectList(db.MemberStage, "Stage", "Title", member.Stage);
            //ViewBag.RegionID = new SelectList(db.Region, "RegionID", "RegionName", member.RegionID);

            Session["NICKNAME"] = aaa.NickName;
            //return RedirectToAction("MemberProfile");
            return View(member);
        }

        [HttpPost]
        public ActionResult Edit(Memberview mem, HttpPostedFileBase strPhoto)
        {
            var c = db.Member.Find(mem.MemberID);

            c.MemberID= mem.MemberID;
            c.FirstName = mem.FirstName;
            c.LastName = mem.LastName;
            c.MAccount = mem.MAccount;
            c.Birthday = mem.Birthday;
            c.Address = mem.Address;

            c.Email = mem.Email;
            c.MPassword = mem.MPassword;
            c.Stage = mem.Stage;
            c.EXP = mem.EXP;
            c.Phone = mem.Phone;
            c.PointCount = mem.PointCount;
            c.NickName = mem.NickName;

            c.RegionID = mem.RegionID;

            if (strPhoto != null)
            {
                var imagesSize = strPhoto.ContentLength;
                byte[] imageByte = new byte[imagesSize];
                strPhoto.InputStream.Read(imageByte, 0, imagesSize);

                c.Photo = imageByte;
            }

            
            db.SaveChanges();
            return RedirectToAction("MemberProfile");

            //return RedirectToAction("");

        }

        public ActionResult GetImage(int? id)
        {
            Member member = db.Member.Find(id);
            byte[] img = member.Photo;
            return new FileContentResult(img, "image/jpeg");
        }


        public ActionResult ForgetPassword()
        {

            return View();
        }

        [HttpPost]
        public ActionResult ForgetPassword(string MAccount, string Email, string Phone)
        {
            string M = MAccount;
            string E = Email;
            string P = Phone;

            var _p = (from p in db.Member
                      where p.MAccount == M & p.Email == E & p.Phone == P
                      select p);

            //id = db.Member.Where(m => m.MAccount == M & m.Email == E & m.Phone == P).Select(m => m.MemberID).FirstOrDefault();

            if (_p.Count() > 0)
            {
                foreach (var z in _p)
                {
                    if ((z.Email == E) && (z.MAccount == M))
                    {
                        Random f = new Random();

                        string g = (f.Next(10000000, 100000000)).ToString();
                        z.MPassword = g;


                        string smtpAddress = "smtp.gmail.com";
                        //設定Port
                        int portNumber = 587;
                        bool enableSSL = true;
                        //填入寄送方email和密碼
                        string emailFrom = "MSIT115bestone@gmail.com";
                        string password = "!QAZ3edc5tgb";
                        //收信方email
                        string emailTo = E;
                        //主旨
                        string subject = "新密碼";
                        //內容
                        string body = "幫幫你幫幫我\n新密碼 :" + g;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress(emailFrom);
                            mail.To.Add(emailTo);
                            mail.Subject = subject;
                            mail.Body = body;
                            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                            {
                                smtp.Credentials = new NetworkCredential(emailFrom, password);
                                smtp.EnableSsl = enableSSL;
                                smtp.Send(mail);
                            }
                        }

                    }
                    else if ((z.Email != E) || (z.MAccount != M))
                    {

                    }
                }
                db.SaveChanges();


                //TempData["MEMBERID"] = id;
                //TempData["MPASSWORD"] = id;
                //Session["MEMBERID"] = id;
                return RedirectToAction("Login");
            }
            else
            {
                TempData["error"] = "資料有誤";
                return View("Login");
            }

        }




        public ActionResult OtherMember()
        {
            MemberCaseCount mcc = new MemberCaseCount();
            int mid = (int)Session["MEMBERID"];

            var q = db.Member.Where(p => p.MemberID != mid).OrderBy(o=>o.MemberID).ToList(); //除我之外 其他會員ID所有資料

            var allmem = q.ToList(); //所有除了我之外的會員所有資料
            var allmemid = q.Select(p => p.MemberID).ToList(); //所有其他會員的ID



            List<Memberview> mvlist = new List<Memberview>();

            //=======會員專長
            //var msk = db.MPSCList.Where(p => p.MemberID == mid).Select(p => p.MPSubClassID); //轉中文
            //List<string> myskillname = new List<string>();
            //foreach (var a in msk)
            //{
            //    var mk = db.MPSubClass.Where(p => p.MPSubClassID == a).Select(p => p.MPSubClass1).First();
            //    myskillname.Add(mk);


            //}

            //ViewBag.Myskill = myskillname;
            //=============

            for (int i = 0; i < allmem.Count; i++)
            {
                Memberview mv = new Memberview();
                int memid = allmem[i].MemberID;
                mv.MemberID = memid;
                mv.NickName = allmem[i].NickName;
                mv.Stage = allmem[i].Stage;
                mv.Photo = allmem[i].Photo;
                mv.Email = allmem[i].Email;
                //mv.MPSubClass1 = "無";
                var q12 = db.MPSCList.Where(o => o.MemberID == memid).ToList();
                string mpsub = "";
                foreach(var a in q12) {

                    string asd = db.MPSubClass.Find(a.MPSubClassID).MPSubClass1;
                    if (mpsub == "")
                    {
                        mpsub = asd;

                    }else
                    {
                        mpsub = mpsub + "," + asd;
                    }


                }
                if(mpsub=="")
                {
                    mv.MPSubClass1 = "無";

                }
                else
                {
                    mv.MPSubClass1 = mpsub;

                }



                mv.casecount = mcc.casecount(allmemid[i])[1];
                mv.sumpoint = mcc.casecount(allmemid[i])[0];
                mv.GDcasecount = mcc.casecount(allmemid[i])[2];
                mv.MPcasecount = mcc.casecount(allmemid[i])[3];
                mv.GDcasegive = mcc.casecount(allmemid[i])[4];
                mv.GDcaseneed = mcc.casecount(allmemid[i])[5];
                mv.MPcasegive = mcc.casecount(allmemid[i])[6];
                mv.MPcaseneed = mcc.casecount(allmemid[i])[7];

                mvlist.Add(mv);

            }

            ViewBag.othermember = mvlist;

            return View();
        }

        public ActionResult OtherMemberProfile(int omid) //傳其他會員ID
        {
            MemberCaseCount cc = new MemberCaseCount();

            var acc = cc.casecount(omid);

            Member m = new Member();
            var q = db.Member.Find(omid);
            var regionname = db.Region.Find(q.RegionID).RegionName;
            var cityname = db.City.Find(db.Region.Find(q.RegionID).CityID).CityName;

            Memberview mp = new Memberview();
            mp.MemberID = q.MemberID;
            mp.LastName = q.LastName;
            mp.FirstName = q.FirstName;
            mp.Birthday = q.Birthday;
            mp.Email = q.Email;
            mp.CityName = cityname;
            mp.RegionName = regionname;
            mp.Address = q.Address;
            mp.Title = db.MemberStage.Find(q.Stage).Title;
            mp.NickName = q.NickName;
            mp.Phone = q.Phone;
            mp.Photo = q.Photo;

            mp.sumpoint = acc[0];
            mp.casecount = acc[1];
            mp.GDcasecount = acc[2];
            mp.MPcasecount = acc[3];
            mp.GDcasegive = acc[4];
            mp.GDcaseneed = acc[5];
            mp.MPcasegive = acc[6];
            mp.MPcaseneed = acc[7];



            var msk = db.MPSCList.Where(p => p.MemberID == omid).Select(p => p.MPSubClassID).ToList(); 
            List<string> myskillname = new List<string>();
            foreach (var a in msk)
            {
                var mk = db.MPSubClass.Where(p => p.MPSubClassID == a).Select(p => p.MPSubClass1).First();
                myskillname.Add(mk);


            }

            ViewBag.Myskill = myskillname;

            return View(mp);

       }

        public ActionResult MemberSkills(int id , string[] Area) //存專長
        {
            int mid = id;
            int len = Area.Length;
            int skid;
            var o = db.MPSCList.Where(p => p.MemberID == mid).ToList();

            foreach(var a in o)
            {
                db.MPSCList.Remove(a);
                

            }
db.SaveChanges();
            for (int i = 0; i<len;i++)
            {
                skid = Convert.ToInt32(Area[i]);
                db.MPSCList.Add(new MPSCList
                {

                    MemberID = mid,
                    MPSubClassID = skid,

                });
                db.SaveChanges();


            }

            var msk = db.MPSCList.Where(p => p.MemberID == mid).Select(p => p.MPSubClassID).ToList(); //轉中文
            List<string> myskillname = new List<string>();
            foreach (var a in msk)
            {
                var mk = db.MPSubClass.Where(p => p.MPSubClassID == a).Select(p => p.MPSubClass1).First();
                myskillname.Add(mk);

            }
            return Json(myskillname, JsonRequestBehavior.AllowGet);

        }
    }
}