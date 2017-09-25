using Msit115BestOne.Areas.ShoppingCart.Models;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Msit115BestOne.Areas.ShoppingCart.Controllers
{
    public class RecipientController : Controller
    {
        private ManPowerEntities db = new ManPowerEntities();
        private IRepository<Cart> dbCart = new Repository<Cart>();
        // GET: ShoppingCart/Recipient
        public ActionResult Index()
        {
            return View(dbCart.GetAll());
        }
        [HttpGet]
        public ActionResult Edit(int ShoppingCartID = 0)
        {
            return View(ShoppingCartID);
        }
        [HttpPost]
        public ActionResult Edit(Cart c)
        {  
                int aaa = Convert.ToInt32(Request.Form["ShoppingCartID"]);
                var sp = db.Cart.Where(a => a.ShoppingCartID == aaa).Select(a => a);
            //if(c.Recipient!=null&&c.RecipientPhone!=null&&c.RecipientAddress!=null)
            //{ 
                foreach (Cart q in sp)
                {
                    //q.Cases = null;
                    q.Recipient = c.Recipient;
                    q.RecipientPhone = c.RecipientPhone;
                    q.RecipientAddress = c.RecipientAddress;
                    db.Entry(q).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
                return RedirectToAction("OrderDetail");
            //}
            //else
            //{
            //    return RedirectToAction("Edit");
            //}    
        }

     
        public ActionResult OrderDetail( )
        {   List<int> ShoppingcartIDinCart = new List<int>();
        List<int> ShoppingcartIDinOrders = new List<int>();
            int id;
            if (Session["MEMBERID"] == null)
            {

                return RedirectToAction("Login", "Member", new { area = "MyMember" });

            }
            else
            {
                id = Convert.ToInt32(Session["MEMBERID"]);
            }


            var sc = (from s in db.Cart.AsEnumerable()
                      where s.MemberID == id
                      select s.ShoppingCartID).ToList();
            var osc = (from s in db.Orders.AsEnumerable()
                       where s.MemberID == id
                       select s.ShoppingCartID).ToList();
            int lastshoppingID = (int)sc.Max();
            var distinctsc = sc.Distinct().ToList();
            var distinctosc = osc.Distinct().ToList();
            distinctosc.RemoveAll(n => n == null);

            for (int i = 0; i < distinctsc.Count(); i++)
            {
                ShoppingcartIDinCart.Add((int)distinctsc[i]);
            }
            for (int i = 0; i < distinctosc.Count(); i++)
            {
                ShoppingcartIDinOrders.Add((int)distinctosc[i]);
            }
            var NoinOrdersShoppingcartid = ShoppingcartIDinCart.Except(ShoppingcartIDinOrders);
            int thisshoppingID;

            List<ShoppingCarts> carts = new List<ShoppingCarts>();
            if (NoinOrdersShoppingcartid != null)
            {
                thisshoppingID = NoinOrdersShoppingcartid.First();
                //List<ShoppingCarts> carts = new List<ShoppingCarts>();
                var tsc = (from s in db.Cart.AsEnumerable()
                           where s.MemberID == id && s.ShoppingCartID == thisshoppingID
                           select s).ToList();
                decimal totalAmount = 0;
                foreach (var i in tsc)
                {
                    ShoppingCarts cart = new ShoppingCarts();
                    cart.CartID = i.CartID;
                    cart.ShoppingCartID = i.ShoppingCartID;
                    cart.CaseID = i.CaseID;
                    cart.Quantity = i.Quantity;
                    cart.Point = i.Point;
                    cart.Recipient = i.Recipient;
                    cart.RecipientPhone = i.RecipientPhone;
                    cart.RecipientAddress = i.RecipientAddress;

                    var caseT = db.Cases.Find(i.CaseID);
                    cart.CaseTitle = caseT.CaseTitle;

                    cart.Amount = Convert.ToInt32(i.Quantity * i.Point);

                    totalAmount += cart.Amount;
                    //totalAmount += 1;
                    carts.Add(cart);
                }

                //TempData["TotalAmount"] = totalAmount;
                //ViewBag.ShoppingCartID = carts[0].ShoppingCartID;
                //TempData["Recipient"] = carts[0].Recipient;
                //TempData["RecipientPhone"] = carts[0].RecipientPhone;
                //TempData["RecipientAddress"] = carts[0].RecipientAddress;
                
            }return View(carts);
        }



        public ActionResult CartDatainOrders() {

            int scid;
            int mid =(int)Session["MEMBERID"];

            if (Session["shoppingcardID"] != null) {

                 scid= (int)Session["shoppingcardID"] ;

                var q = db.Cart.Where(c => c.MemberID == mid & c.ShoppingCartID == scid).Select(c => c).ToList();
                List<Orders> orders = new List<Orders>();
                  foreach(var a in q)
                {
                    Orders o = new Orders {CaseID= a.CaseID, MemberID=a.MemberID,ShoppingCartID=a.ShoppingCartID,Quantity=a.Quantity,Point=a.Point,OrderDate=DateTime.Now  };
                    //o.CaseID = a.CaseID;
                    //o.MemberID = a.MemberID;
                    //o.ShoppingCartID = a.ShoppingCartID;
                    //o.Quantity = a.Quantity;
                    //o.Point = a.Point;
                    //o.OrderDate = DateTime.Now;

                   int cs = (from c in db.Cases
                             where c.CaseID == a.CaseID
                             select c.StatusID).First();

                    switch (cs)
                    {
                        case 4:
                            o.OrderStatusID = 1;
                            break;
                        case 5:
                            o.OrderStatusID = 2;
                            break;
                        case 7:
                            o.OrderStatusID = 4;
                            break;
                        case 8:
                            o.OrderStatusID = 3;
                            break;
                    }
                    orders.Add(o);
                    //db.Orders.Add(o);
                    //db.SaveChanges();


                }
                db.Orders.AddRange(orders);
                db.SaveChanges();

            }
            Session["shoppingcardID"] = null;
            return RedirectToAction("orderall", "eva", new { area = "MyEva" });
            //return RedirectToAction("BrowseGoodsAll", "Goods", new { area = "TotalCase" });
          
        }

    }
}