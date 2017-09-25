using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.ShoppingCart.Models
{
    public class ShoppingCarts
    {
        public List<ShoppingCarts> cartItems;

        //Cart
        public int CartID { get; set; }
        [DisplayName("訂單編號")]
        public Nullable<int> ShoppingCartID { get; set; }
        public int CaseID { get; set; }
        public int MemberID { get; set; }
        [DisplayName("點數")]
        public int Point { get; set; }
        [DisplayName("數量")]
        public Nullable<int> Quantity { get; set; }
        [DisplayName("訂購日期")]
        public Nullable<System.DateTime> DateCreated { get; set; }
        [DisplayName("收件人姓名")]

        public string Recipient { get; set; }
        [DisplayName("收件人電話")]
        //[DataType(DataType.PhoneNumber)]
        public Nullable<int> RecipientPhone { get; set; }
        [DisplayName("收件人地址")]
       
        public string RecipientAddress { get; set; }

        //Case
        [DisplayName("案件名稱")]
        public string CaseTitle { get; set; }

        //Goods
        //public Nullable<int> GdsPoint { get; set; }

        //ManPower
        //public int MPPoint { get; set; }

        //X
        [DisplayName("小計")]
        public decimal Amount { get; set; }

        [DisplayName("總計")]
        public decimal TotalAmount{ get; set; }
    }
}