using Msit115BestOne.Areas.MyEva.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.MyEva.Models.partial;

namespace Msit115BestOne.Areas.MyEva.Controllers
{
    public class evaController : Controller
    {
        private ManPowerEntities db = new ManPowerEntities();
        private IRepository<Orders> dbo = new Repository<Orders>();
        private IRepository<Evaluation> dbe = new Repository<Evaluation>();
        //private Orderlistpartial olp = new Orderlistpartial();
        orderlistcontent olc = new orderlistcontent();
        // GET: eva
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult orderlist()   //評價畫面
        {
            int mid;
            if (Session["MEMBERID"] == null)
            {
                //if (id > 0)
                //{
                //    Session["MEMBERID"] = id;
                //    mid = (int)(Session["MEMBERID"]);
                //    return View(olc.detail(id));

                //}
                //else
                //{
                return RedirectToAction("Login", "Member", new { area = "MyMember" });
                //}
            }
            else
            {

                mid = Convert.ToInt32(Session["MEMBERID"]);


                string nm;
                nm = db.Member.Find(mid).NickName;
                ViewBag.nickname = nm;


                return View(olc.detail(mid));
            }

        }


        public ActionResult orderall()
        {

            List<Orderlistpartial> olist = new List<Orderlistpartial>();
            int mid = (int)Session["MEMBERID"];

            var order = (from o in db.Orders
                         where o.MemberID == mid
                         select o).ToList();

            for (int i = 0; i < order.Count; i++)
            {
                Orderlistpartial olp = new Orderlistpartial();

                var c = (from m in db.Cases.AsEnumerable()
                         where m.CaseID == order[i].CaseID
                         select new { m.MemberID, m.CaseTitle, m.Contact }).First();
                int mmid = c.MemberID;
                int oord = order[i].OrderID;
                int orderstatusid = order[i].OrderStatusID;

                olp.Contact = c.Contact;
                olp.OrderID = oord;
                olp.OrderStatus1 = db.OrderStatus.Find(orderstatusid).OrderStatus1;
                olp.CaseID = (int)order[i].CaseID;
                olp.MemberID = mmid;
                olp.NickName = db.Member.Find(mmid).NickName;

                olp.content = c.CaseTitle;
                olp.Quantity = (int)order.First().Quantity;
                olp.Point = order.First().Point;

                olist.Add(olp);
            }


            string nm;
            nm = db.Member.Find(mid).NickName;
            ViewBag.nickname = nm;

            return View(olist);


        }

        [HttpPost]
        public ActionResult Edit(FormCollection f  /* Evaluation[] c*/)
        {
            var iiii = f["e.EvaID"].Split(',');
            var iii = f["e.Evaluation1"].Split(',');
            var ii = f["e.Evacontent"].Split(',');
            for (int i = 0; i < iii.Length; i++)
            {
                var ec = ii[i];
                var elevel = Convert.ToInt32(iii[i]);
                var eid = Convert.ToInt32(iiii[i]);
                var q = from p in db.Evaluation
                        where p.EvaID.Equals(eid)
                        select p;
                foreach (var a in q)
                {
                    a.Evaluation1 = elevel;
                    a.Evacontent = ec;
                }
            }
            db.SaveChanges();
            return RedirectToAction("Evalist");
            //foreach(var q in c)
            //{
            //    var ev = db.Evaluation.Find(q.EvaID);
            //    ev.OrderID = q.OrderID;
            //    ev.Evaluation1 = q.Evaluation1;
            //    ev.Evacontent = q.Evacontent;
            //}

            //var ev = db.Evaluation.Find(c.EvaID);
            //ev.OrderID = c.OrderID;
            //ev.Evaluation1 = c.Evaluation1;
            //ev.Evacontent = c.Evacontent;

            //db.SaveChanges();
            ////Repository
            //return View("Index");
        } //不用


        public ActionResult gid(int id)
        {
            //Session["oID"] = id;
            return Json(dbe.GetById(id), JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public ActionResult CreateEva(int orderID, int select1, string content)
        {

            var c = db.Orders.Find(orderID).CaseID;
            var m = db.Cases.Where(p => p.CaseID == c).Select(p => p);



            Evaluation eva = new Evaluation();
            eva.MemberID = Convert.ToInt32(Session["MEMBERID"]);
            eva.OrderID = orderID;
            eva.Evaluation1 = select1;
            eva.Evacontent = content;
            if (select1 > 0 && select1 < 6)
            {
                dbe.Create(eva);
                TempData["message"] = "評價成功";

                var q = from o in db.Orders
                        where o.OrderID.Equals(orderID)
                        select o;

                foreach (var a in q)
                {
                    a.OrderStatusID = 1009;
                }


                foreach (var b in m)
                {
                    b.Recommendation += select1;


                }
                db.SaveChanges();


                return RedirectToAction("orderlist");
            }
            else
            {
                TempData["message"] = "請輸入介於1-5之間的整數";
                return RedirectToAction("orderlist");
            }


        }


        public ActionResult Evalist()
        {

            int mid = (int)Session["MEMBERID"];
            string nm;
            nm = db.Member.Find(mid).NickName;
            ViewBag.nickname = nm;


            //int mid;
            //if (Session["MEMBERID"] == null)
            //{

            //    if (id > 0)
            //    {
            //        Session["MEMBERID"] = id;
            //        mid = (int)(Session["MEMBERID"]);
            //        var q = from o in db.Orders
            //                where o.MemberID == mid
            //                select o;
            //        return View(q.ToList());

            //    }
            //    else
            //    {
            //        mid = (int)(Session["MEMBERID"]);
            //        var q = from o in db.Orders
            //                where o.MemberID == mid
            //                select o;
            //        return View(q.ToList());

            //    }
            //}
            //else
            //{
            //    mid = (int)(Session["MEMBERID"]);
            //    var q = from o in db.Orders
            //            where o.MemberID == mid
            //            select o;
            //    return View(q.ToList());

            //}












            return View(dbe.GetAll());
        }

    }
}