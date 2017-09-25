using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.Admin.Models.MemberGroup
{
    public class CityGroupViewModel
    {
        public int CityID { get; set; }

        [DisplayName("地區")]
        public string CityName { get; set; }
    }
}