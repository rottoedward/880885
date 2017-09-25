using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyCaseCenter.Models.Partial
{
    public class CaseManPower
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

        public int MPID { get; set; }
        [DisplayName("人力名稱")]
        [Required(ErrorMessage = "請輸入人力名稱")]
        public string MPName { get; set; }
        [DisplayName("人力需求數量")]
        [Required(ErrorMessage = "請輸入數量")]
        public int MPNeedCount { get; set; }
        [DisplayName("實際徵求到的人力數量")]
        public int MPActuralCount { get; set; }
        [DisplayName("點數")]
        [Required(ErrorMessage = "請輸入點數")]
        public int MPPoint { get; set; }
        [DataType(DataType.Time)]
        [DisplayName("人力徵求時間")]
        [Required(ErrorMessage = "請輸入徵求時間")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:t}")]
        public System.DateTime MPTime { get; set; }
        public Nullable<int> MPSubClassID { get; set; }
        [DisplayName("人力徵求日期")]
        [Required(ErrorMessage = "請輸入徵求日期")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public System.DateTime MPDate { get; set; }


        public int MPClassID { get; set; }
        [DisplayName("分類名稱")]
        public string MPSubClass1 { get; set; }
        public string MPClass1 { get; set; }

        public int ImageID { get; set; }
    }
}