using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    public class ContentViewModel
    {
        public int ContentID { get; set; }
        public int CaseID { get; set; }
        public int MemberID { get; set; }
        public string MessageContent { get; set; }
        public System.DateTime MessageDateTime { get; set; }
        [DataType(DataType.MultilineText)]
        public string AuthorRepeat { get; set; }

        [DisplayName("會員")]
        public string NickName { get; set; }

        public byte[] Photo { get; set; }

        public int Mymid { get; set; }
    }
}