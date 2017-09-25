using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.ShoppingCart.Models
{
    public class MemberGoodPartial
    {

        public int MemberID { get; set; }

        public int Point { get; set; }

        public Nullable<int> Quantity { get; set; }
        public int CaseID { get; set; }
    }
}