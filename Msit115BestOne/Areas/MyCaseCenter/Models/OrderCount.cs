using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyCaseCenter.Models
{
    public class OrderCount
    {
        [DisplayName("需求編號")]
        public int OrderID { get; set; }
        public int MemberID { get; set; }
        public int CaseID { get; set; }
        [DisplayName("數量")]
        public int Cou { get; set; }
        [DisplayName("標題")]
        [Required(ErrorMessage = "請輸入標題")]
        [DataType(DataType.MultilineText)]
        public string CaseTitle { get; set; }
    }
}