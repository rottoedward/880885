using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyEva.Models.partial
{
    public class Orderlistpartial
    {
        [DisplayName("訂單編號")]
        public int OrderID { get; set; }
        [DisplayName("案件編號")]
        public int CaseID { get; set; }
        [DisplayName("訂單內容")]
        public string content {get; set; }
        [DisplayName("會員")]
        public int MemberID { get; set; }
        [DisplayName("數量")]
        public int Quantity { get; set; }
        [DisplayName("點數收支")]
        public int Point { get; set; }
        [DisplayName("案主")]
        public string NickName { get; set; }
        public int OrderStatusID { get; set; }

        [DisplayName("訂單狀態")]
        public string OrderStatus1 { get; set; }
        [DisplayName("聯繫方式")]

        public string Contact { get; set; }

    }
}