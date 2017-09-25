using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyCaseCenter.Models
{
    public class ContentViewModel
    {
        public int ContentID { get; set; }
        public int CaseID { get; set; }
        public int MemberID { get; set; }
        public string MessageContent { get; set; }
        public System.DateTime MessageDateTime { get; set; }
        public string AuthorRepeat { get; set; }

        [DisplayName("會員")]
        public string NickName { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] Photo { get; set; }
        public string CityName { get; set; }
        public string RegionName { get; set; }
        public string Address { get; set; }

        public string MPSubClass1 { get; set; }
    }
}