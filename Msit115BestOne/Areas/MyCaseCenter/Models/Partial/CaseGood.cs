using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyCaseCenter.Models.Partial
{
    public class CaseGood
    {
        public int CaseID { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public int MemberID { get; set; }
        [DisplayName("標題")]
        [Required(ErrorMessage = "請輸入標題")]
        [DataType(DataType.MultilineText)]
        public string CaseTitle { get; set; }
        [DataType(DataType.MultilineText)]
        [DisplayName("內容")]
        [Required(ErrorMessage = "請輸入內容")]
        public string CaseContent { get; set; }
        [Required(ErrorMessage = "請輸入地址")]
        [DisplayName("地址")]
        public string Location { get; set; }
        public Nullable<int> Recommendation { get; set; }
        public int StatusID { get; set; }
        public string StatusName { get; set; }
        public int RegionID { get; set; }
        [DisplayName("聯繫方式")]
        [Required(ErrorMessage = "請輸入聯繫方式")]
        public string Contact { get; set; }

        public int CityID { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }

        public int GdsID { get; set; }
        [DisplayName("物品名稱")]
        [Required(ErrorMessage = "請輸入物品名")]
        public string GdsName { get; set; }
        [DisplayName("數量")]
        [Required(ErrorMessage = "請輸入數量")]
        public Nullable<int> GdsCount { get; set; }
        [DisplayName("價格")]
        [Required(ErrorMessage = "請輸入價格")]
        public Nullable<int> GdsPoint { get; set; }
        public System.DateTime GdsDeadline { get; set; }
        public Nullable<int> GdsSubClassID { get; set; }
        //public int CaseID { get; set; }
        public int ImageID { get; set; }
        //public byte[] Images { get; set; }
        public int GdsClassID { get; set; }
        public string GdsClass { get; set; }
        [DisplayName("分類名稱")]
        public string GdsSubClass1 { get; set; }
    }
}