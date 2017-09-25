using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Areas.Admin.Models.MemberGroup;

namespace Msit115BestOne.Areas.Admin.Models
{
    public class MemberGroupViewModel
    {
        public MemberByIdViewModel Member { get; set; }

        public List<CityGroupViewModel> City { get; set; }

    }
}