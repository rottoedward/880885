﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.Admin.Models
{
    public class MemberViewModel
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
        public string RegionAddress { get; set; }

        [DisplayName("電話")]
        public string Phone { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }
    }
}