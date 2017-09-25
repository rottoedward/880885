using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{
    [MetadataType(typeof(_Board))]
    public partial class Board
    {
        public class _Board
        {
            public int BoardID { get; set; }
            [DisplayName("發文時間")]
            public System.DateTime StartDateTime { get; set; }
            [DisplayName("發文者")]
            public int MemberID { get; set; }
            [DisplayName("標題")]
            [Required(ErrorMessage ="標題必填")]
            public string CaseTitle { get; set; }

            [DisplayName("內容")]
            [DataType(DataType.MultilineText)]
            public string CaseContent { get; set; }
          
            public virtual Member Member { get; set; }
        }

    }
}