using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.Admin.Models
{
    public class GoodsViewModel
    {
        // Goods
        [DisplayName("案件編號")]
        public int CaseID { get; set; }

        [DisplayName("數量")]
        public Nullable<int> GdsCount { get; set; }

        // Cases
        [DisplayName("案件標題")]
        public string CaseTitle { get; set; }

        [DisplayName("起始時間")]
        public System.DateTime StartDateTime { get; set; }

        [DisplayName("結束時間")]
        public Nullable<System.DateTime> EndDateTime { get; set; }

        // Member LastName+FirstName
        [DisplayName("發案者")]
        public string MemberName { get; set; }

        //Status
        [DisplayName("案件狀態")]
        public string StatusName { get; set; }


    }
}