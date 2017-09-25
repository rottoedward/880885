using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.Admin.Models.MemberGroup
{
    public class MemberByIdViewModel
    {

        [DisplayName("會員編號")]
        public int MemberID { get; set; }

        [DisplayName("帳號")]
        public string MAccount { get; set; }

        [DisplayName("生日")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public Nullable<System.DateTime> Birthday { get; set; }

        [DisplayName("地址")]
        public string Address { get; set; }

        [DisplayName("電話")]
        public string Phone { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("密碼")]
        public string MPassword { get; set; }

        [DisplayName("地區")]
        [Required(ErrorMessage ="地區必填")]
        public int RegionID { get; set; }


    }
}