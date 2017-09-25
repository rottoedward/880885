using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyCaseCenter.Models
{
    public class OrderViewModel
    {
        [DisplayName("需求編號")]
        public int OrderID { get; set; }
        public int MemberID { get; set; }
        public int CaseID { get; set; }
        [DisplayName("總額")]
        public int Point { get; set; }
        [DisplayName("數量")]
        public int Quantity { get; set; }
        [DisplayName("下單日期")]
        public System.DateTime OrderDate { get; set; }
        [DisplayName("結束日期")]
        public Nullable<System.DateTime> FinishedDate { get; set; }
        public int OrderStatusID { get; set; }



        [DisplayName("貨品狀況")]
        public string OrderStatus1 { get; set; }
        [DisplayName("會員")]
        public string NickName { get; set; }
        public string MPSubClass1 { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }

        [DisplayName("貨品")]
        public string GdsName { get; set; }
        public Nullable<int> GdsCount { get; set; }
        [DisplayName("標題")]
        [Required(ErrorMessage = "請輸入標題")]
        [DataType(DataType.MultilineText)]
        public string CaseTitle { get; set; }
    }
}