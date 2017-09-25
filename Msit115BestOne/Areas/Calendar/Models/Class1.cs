using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.Calendar.Models
{
    [Serializable]
    public class Class1
    {
        public int CaseID { get; set; }
        public System.DateTime StartDateTime { get; set; }
        public Nullable<System.DateTime> EndDateTime { get; set; }
        public int MemberID { get; set; }
        public string CaseTitle { get; set; }
        public string CaseContent { get; set; }
        public string Location { get; set; }
        public Nullable<int> Recommendation { get; set; }
        public int StatusID { get; set; }
        public int RegionID { get; set; }
        public string Contact { get; set; }
    }
}