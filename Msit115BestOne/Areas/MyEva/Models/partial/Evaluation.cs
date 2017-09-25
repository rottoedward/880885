using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{

    [MetadataType(typeof(aa))]
    public partial class Evaluation
    {
        public class aa
        {
            [DisplayName("訂單ID")]
            public int OrderID { get; set; }
            [DisplayName("評價等級")]
            [Required(ErrorMessage = "請評價")]
            [Range(1, 5)]
            public int Evaluation1 { get; set; }
            public int EvaID { get; set; }
            [DisplayName("評價內容")]
            public string Evacontent { get; set; }

            public virtual Orders Orders { get; set; }
        }
    }
}