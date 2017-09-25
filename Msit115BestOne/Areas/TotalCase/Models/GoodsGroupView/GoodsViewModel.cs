using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Models;

namespace Msit115BestOne.Areas.TotalCase.Models.GoodsGroupView
{
    public class GoodsViewModel
    {
        //// 物品案件個別資訊
        //public IEnumerable<Goods> GoodsInfo { get; set; }

        //member
        public string NickName { get; set; }


        // Goods
        public string GdsName { get; set; }
        public int CaseID { get; set; }
        public int MemberID { get; set; }
        public Nullable<int> GdsCount { get; set; }

        // Cases
        public string CaseTitle { get; set; }
        public int StatusID { get; set; }
        //CaseStatus
        public string StatusName { get; set; }


        // GoodsClass
        public string GdsClass { get; set; }

        // Picture
        //public byte[] Images { get; set; }


    }
}