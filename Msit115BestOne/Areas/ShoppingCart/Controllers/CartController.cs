using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using Msit115BestOne.Models;
using Msit115BestOne.Areas.ShoppingCart.Models;

namespace Msit115BestOne.Areas.ShoppingCart.Controllers
{
    public class CartController : Controller
    {
        private Repository<ShoppingCarts> db1 = new Repository<ShoppingCarts>();
        private ManPowerEntities db = new ManPowerEntities();
        private Repository<Cases> Casedb = new Repository<Cases>();
        private Repository<Cart> Cartdb = new Repository<Cart>();
        // GET: ShoppingCart/Cart

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Cart()
        {        List<int> ShoppingcartIDinCart = new List<int>();
        List<int> ShoppingcartIDinOrders = new List<int>();
           
            int id;
            if (Session["MEMBERID"] == null)
            {

                return RedirectToAction("Login", "Member", new { area = "MyMember" });

            }
            else {
                id = Convert.ToInt32(Session["MEMBERID"]);
            }
            
            var sc = (from s in db.Cart.AsEnumerable()
                      where s.MemberID == id
                      select s.ShoppingCartID).ToList();
            var osc = (from s in db.Orders.AsEnumerable()
                       where s.MemberID == id
                       select s.ShoppingCartID).ToList();
            if (sc.Max() == null)
            {

                return RedirectToAction("BrowseGoodsAll", "Goods", new { area = "TotalCase" });
            }
            else { 
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
            if (NoinOrdersShoppingcartid.Count()>0)
            {
                thisshoppingID = NoinOrdersShoppingcartid.First();
                //List<ShoppingCarts> carts = new List<ShoppingCarts>();
                var tsc = (from s in db.Cart.AsEnumerable()
                           where s.MemberID == id && s.ShoppingCartID == thisshoppingID
                           select s).ToList();
                decimal totalAmount = 0;
                foreach (var i in tsc)
                {
                    ShoppingCarts ca = new ShoppingCarts();
                    ca.CartID = i.CartID;
                    ca.ShoppingCartID = i.ShoppingCartID;
                    ca.CaseID = i.CaseID;
                    ca.Quantity = i.Quantity;
                    ca.Point = i.Point;

                    var caseT = db.Cases.Find(i.CaseID);
                    ca.CaseTitle = caseT.CaseTitle;
                    ca.Amount = Convert.ToInt32(i.Quantity * i.Point);
                    totalAmount += ca.Amount;
                    carts.Add(ca);
                }
                TempData["TotalAmount"] = totalAmount;
                ViewBag.ShoppingCartID = carts[0].ShoppingCartID;
                return View(carts);
            }
                else
                {
                    return RedirectToAction("BrowseGoodsAll", "Goods", new { area = "TotalCase" });
                    //return View(); // 未做 => 需轉回商品頁面 上面先轉回到首頁代替
                }
            }
        }
  

        public ActionResult Delete(int id)
        {
            Cartdb.Delete(Cartdb.GetById(id));
            return RedirectToAction("Cart");
        }
   
        public ActionResult editedit(FormCollection collec)
        {
            var iii = collec["cart.CartID"].Split(',');
            var ii = collec["cart.Quantity"].Split(',');
            //int[] aa = { 2, 3 };

            for (int i = 0; i < iii.Length; i++)
            {
                var c =Convert.ToInt32( iii[i]);
                var qu= Convert.ToInt32(ii[i]);

                var q = from p in db.Cart
                        where p.CartID.Equals(c)
                        select p;
                foreach (var a in q)
                {
                    a.Quantity = qu;
                }
            }

            db.SaveChanges();

            return RedirectToAction("Cart");
        }
    }  
}
