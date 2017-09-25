using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.SearchCase.Models
{
    public class CaseViewModel
    {
        [DisplayName("關鍵字")]
        public string KeyWord { get; set; }
        [DisplayName("城市及行政區")]
        public int gCityID { get; set; }
        public int gRegionID { get; set; }
        [DisplayName("城市及行政區")]
        public int mCityID { get; set; }
        public int mRegionID { get; set; }
        [DisplayName("搜尋分類")]
        public int Condition { get; set; }
        [DisplayName("截止日期")]
        public string EndDate { get; set; }
        public int gClassID { get; set; }
        public int gSubClassID { get; set; }
        public int mClassID { get; set; }
        public int mSubClassID { get; set; }
        [DisplayName("提供/徵求")]
        public int CaseStatus { get; set; }
    }
}