using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Msit115BestOne.Areas.TotalCase.Models.GoodsGroupView;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    public class GoodsGroupViewModel
    {
        public List<GoodsViewModel> Goods { get; set; }
        public List<GoodsClassViewModel> GoodsClass { get; set; }
    }
}