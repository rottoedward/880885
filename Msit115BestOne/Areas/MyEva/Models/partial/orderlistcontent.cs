using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Areas.MyEva.Models.partial;
using Msit115BestOne.Models;

namespace Msit115BestOne.Areas.MyEva.Models.partial
{
    public class orderlistcontent
    {       //1 A提供東西 B跟A拿          "B給A的評價" v -  casest 4 orderstatusID 1 ->1006
            //2 A提供東西 B跟A買           A給B的評價  o 
            //3 A徵求東西 B提供給A         A給B的評價  o -  casest 5 orderstatusID 2 ->1007
            //4 A徵求東西 B提供給A        "B給A的評價" v 
            //5 A徵求人力 B提供給A         A給B的評價  o    casest 7 orderstatusID 4 ->1005
            //6 A徵求人力 B提供給A        "B給A的評價" v -  
            //7 A提供人力 B跟A拿         A給B的評價  o -  
            //8 A提供人力 B跟A拿       "B給A的評價" v casest 8 orderstatusID 3 ->1004
        ManPowerEntities db = new ManPowerEntities();



        public List<Orderlistpartial> detail(int id)
        {
            List<Orderlistpartial> olist = new List<Orderlistpartial>();
            //=找到我參加的訂單=============================
            var order = (from o in db.Orders
                         where (o.MemberID == id && o.FinishedDate != null && o.OrderStatusID == 1006) || (o.MemberID == id && o.FinishedDate != null && o.OrderStatusID == 1004)
                         select o).ToList();

            for (int i = 0; i < order.Count; i++)
            {
                Orderlistpartial olp = new Orderlistpartial();

                var c = from m in db.Cases.AsEnumerable()
                        where m.CaseID == order[i].CaseID
                        select (new { m.MemberID, m.CaseTitle });


                olp.OrderID = order[i].OrderID;
                var mmiidd = c.First().MemberID;
                olp.CaseID = (int)order[i].CaseID;
                olp.MemberID = mmiidd;
                olp.NickName = db.Member.Find(mmiidd).NickName;
                olp.content = c.First().CaseTitle;
                olp.Quantity = (int)order.First().Quantity;
                olp.Point = order.First().Point;

                olist.Add(olp);
            }

            // 找到我提供的所有案件ID
            var cid = (from c in db.Cases.AsEnumerable()
                       join o in db.Orders on c.CaseID equals o.CaseID
                       where (c.MemberID == id && c.StatusID == 5 && o.OrderStatusID == 1007) || (c.MemberID == id && c.StatusID == 7 && o.OrderStatusID == 1005) || (c.MemberID == id && c.StatusID == 6 && o.OrderStatusID != 1009)
                       select new { c.CaseID, c.CaseTitle }).ToList();
            var discid = cid.Distinct().ToList();

            for (int i = 0; i < discid.Count(); i++)
            {
               
                
                var mem = (from m in db.Orders.AsEnumerable()
                           where m.CaseID == discid[i].CaseID && m.FinishedDate != null
                           select (new { m.MemberID, m.Quantity, m.Point, m.CaseID, m.ShoppingCartID })).ToList();
                
                
                //找到所有訂單內參加我發的案件的會員訂單資料
                if (mem.Count > 0)
                {

                    for(int k =0; k < mem.Count; k++)
                    {
                        Orderlistpartial olp = new Orderlistpartial();
                        var oid = (from p in db.Orders.AsEnumerable()
                               where p.MemberID == mem[k].MemberID && p.ShoppingCartID==mem[k].ShoppingCartID
                               select p.OrderID).First();



                    olp.OrderID = oid;
                    olp.CaseID = (int)mem[k].CaseID;
                        var mmiidd = mem[k].MemberID;
                        olp.MemberID = mmiidd;
                        olp.NickName = db.Member.Find(mmiidd).NickName;
                    olp.content = discid[i].CaseTitle;
                    olp.Quantity = (int)mem[k].Quantity;
                    olp.Point = mem[k].Point;

                    olist.Add(olp);

                    }
                }

            }

            #region


            ////====================================================================
            //var SCid = (from o in db.Orders
            //            where o.MemberID == id && o.ShoppingCartID != null
            //            select o.ShoppingCartID).ToList();
            ////從shoppingcartID往回找CaseID
            //  for (int i = 0; i < SCid.Count; i++)
            //{
            //    var c = (from ca in db.Cart.AsEnumerable()
            //             where ca.ShoppingCartID == SCid[i]
            //             select (ca.CaseID)).ToList();
            //    for (int j = 0; j < c.Count; j++)
            //    {
            //         Orderlistpartial olp = new Orderlistpartial();
            //        var q = from m in db.Cases.AsEnumerable()
            //                where m.CaseID == c[j]
            //                select (new { MID = m.MemberID, CT = m.CaseTitle });
            //        var orderID = (from o in db.Orders.AsEnumerable()
            //                       where o.ShoppingCartID == SCid[i]
            //                       select o.OrderID).First();
            //        var ccc = from qu in db.Cart.AsEnumerable()
            //                  where qu.CaseID == c[j]
            //                  select (new { qu.Quantity, qu.Point });

            //        olp.OrderID = orderID;
            //        olp.CaseID = c[j];
            //        olp.MemberID = q.First().MID;
            //        olp.content = q.First().CT + "(我買的東西)";
            //        olp.Quantity=(int)ccc.First().Quantity;
            //        olp.Point = ccc.First().Point;
            //        olist.Add(olp);
            //    }
            //}
            ////====================================================================
            //var qq = (from o in db.Orders.AsEnumerable()
            //          where o.MemberID == id && o.ShoppingCartID == null
            //          select (new { o.CaseID, o.Quantity, o.Point, o.OrderID })).ToList();

            //for (int i = 0; i < qq.Count; i++)
            //{
            //    Orderlistpartial olp = new Orderlistpartial();

            //    var c = from m in db.Cases.AsEnumerable()
            //            where m.CaseID == qq[i].CaseID
            //            select (new { m.MemberID, m.CaseTitle });


            //    olp.OrderID = qq[i].OrderID;

            //    olp.CaseID = (int)qq[i].CaseID;
            //    olp.MemberID = c.First().MemberID;
            //    olp.content = c.First().CaseTitle + "(我提供的東西/人力)";
            //    olp.Quantity = (int)qq.First().Quantity;
            //    olp.Point = qq.First().Point;

            //    olist.Add(olp);
            //}
            ////==============================================================
            //var cid = (from m in db.Cases.AsEnumerable()
            //           where m.MemberID == id
            //           select (new { m.CaseID, m.CaseTitle })).ToList();

            //for (int i = 0; i < cid.Count(); i++)
            //{
            //    Orderlistpartial olp = new Orderlistpartial();
            //    var mem = (from m in db.Orders.AsEnumerable()
            //               where m.CaseID == cid[i].CaseID
            //               select (new { m.MemberID, m.OrderID, m.Quantity, m.Point, m.CaseID, })).ToList();
            //    if (mem.Count > 0)
            //    {
            //        olp.OrderID = mem[0].OrderID;
            //        olp.CaseID = (int)mem[0].CaseID;
            //        olp.MemberID = mem[0].MemberID;
            //        olp.content = cid[i].CaseTitle + "(我收到的東西/人力)";
            //        olp.Quantity = (int)mem[0].Quantity;
            //        olp.Point = mem[0].Point;

            //        olist.Add(olp);
            //    }
            //}
            ////=======================================================
            //for (int i = 0; i < cid.Count(); i++)
            //{
            //    Orderlistpartial olp = new Orderlistpartial();
            //    var mem = (from m in db.Cart.AsEnumerable()
            //               where m.CaseID == cid[i].CaseID
            //               select (new { m.MemberID, m.Quantity, m.Point, m.CaseID, m.ShoppingCartID })).ToList();

            //    if (mem.Count > 0)
            //    {

            //        var oid = (from p in db.Orders.AsEnumerable()
            //                   where p.ShoppingCartID == mem[0].ShoppingCartID
            //                   select p.OrderID).First();



            //        olp.OrderID = oid;
            //        olp.CaseID = (int)mem[0].CaseID;
            //        olp.MemberID = mem[0].MemberID;
            //        olp.content = cid[i].CaseTitle + "(我賣的東西)";
            //        olp.Quantity = (int)mem[0].Quantity;
            //        olp.Point = mem[0].Point;

            //        olist.Add(olp);
            //    }
            //}
            #endregion
            return olist.ToList();
        }


    }
}