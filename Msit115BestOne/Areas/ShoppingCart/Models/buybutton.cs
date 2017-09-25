using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.ShoppingCart.Models
{
    public class buybutton
    {
        private ManPowerEntities db = new ManPowerEntities();
        List<int> ShoppingcartIDinCart = new List<int>();//存cart表內shoppingcartID
        List<int> ShoppingcartIDinOrders = new List<int>(); //存Order表內shoppingcartID
        public int newshoppingID=0;

        public int buycheck(int id)
        {

            //=====把兩個表中的shoppingcartID取出來
            var sc = (from s in db.Cart.AsEnumerable()
                      where s.MemberID == id
                      select s.ShoppingCartID).ToList();
            var osc = (from s in db.Orders.AsEnumerable()
                       where s.MemberID == id
                       select s.ShoppingCartID).ToList();
            //======存現有的shoppingcartID中最大的那個如果沒有現有的就直接給1
            if (sc.Count == 0)
            {
                newshoppingID = 1;
                return newshoppingID;
            }
            else
            {

            int lastshoppingID = (int)sc.Max();
            

            //把兩個表中重複或是null的shoppingcartID去掉
            var distinctsc = sc.Distinct().ToList();
            var distinctosc = osc.Distinct().ToList();
            distinctosc.RemoveAll(n => n == null);
            //灌值進去list內存
            for (int i = 0; i < distinctsc.Count(); i++)
            {
                ShoppingcartIDinCart.Add((int)distinctsc[i]);
            }
            for (int i = 0; i < distinctosc.Count(); i++)
            {
                ShoppingcartIDinOrders.Add((int)distinctosc[i]);
            }
            //比較兩個list內的shoppingcartID 
            var NoinOrdersShoppingcartid = ShoppingcartIDinCart.Except(ShoppingcartIDinOrders);
            int thisshoppingID;
               
            
            List<ShoppingCarts> carts = new List<ShoppingCarts>();

            if (NoinOrdersShoppingcartid.Count()>0)
            {thisshoppingID = NoinOrdersShoppingcartid.First();
                //=============刪除上次購物車未加入訂單的資料===========
                var tsc = (from s in db.Cart.AsEnumerable()
                           where s.MemberID == id && s.ShoppingCartID == thisshoppingID
                           select s).ToList();
                foreach (var q in tsc)
                {
                    db.Cart.Remove(q);
                    db.SaveChanges();
                }
                    newshoppingID = lastshoppingID;
            }
            else
            {
                newshoppingID = lastshoppingID + 1;
            }
            return newshoppingID;
        }
        }

        public void SaveinCart(int memberID, int caseID, int Quantity,int point,int scid) {
            //==================寫在controller內
            //int shoppingcartID;

            //if(newshoppingID!=0)
            //{
            //    shoppingcartID = newshoppingID;
            //}
            //else
            //{
            //    shoppingcartID= buycheck(memberID);

            //}
            //寫在controller內
            Cart c = new Cart();

            c.MemberID = memberID;
            c.ShoppingCartID = scid;
            c.CaseID = caseID;
            c.Point = point;
            c.Quantity = Quantity;
            c.DateCreated = DateTime.Now;

            db.Cart.Add(c);
            db.SaveChanges();
        }

    }
}