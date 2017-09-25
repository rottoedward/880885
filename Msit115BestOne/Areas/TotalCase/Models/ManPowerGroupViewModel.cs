using Msit115BestOne.Areas.TotalCase.Models.ManPowersGroupView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    public class ManPowerGroupViewModel
    {
        public List<ManPowersViewModel> ManPower { get; set; }
        public List<ManPowersClassViewModel> ManPowerClass { get; set; }


    }
}