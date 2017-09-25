using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.SearchCase.Models
{
    public class CaseResultViewModel
    {
        [DisplayName("案件編號")]
        public int CaseID { get; set; }
        [DisplayName("提供/徵求")]
        public string GiveOrNeed { get; set; }
        [DisplayName("案件分類")]
        public int Condition { get; set; }
        [DisplayName("案件標題")]
        public string CaseTitle { get; set; }
        [DisplayName("案件內容")]
        public string CaseContent { get; set; }
        [DisplayName("所在位置")]
        public string Location { get; set; }
    }
}