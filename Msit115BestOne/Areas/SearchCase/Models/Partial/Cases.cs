using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Models
{
    [MetadataType(typeof(CasesMetadata))]
    public partial class Cases
    {
        public class CasesMetadata
        {
            [DisplayName("案件編號")]
            public int CaseID { get; set; }
            [DisplayName("案件標題")]
            public string CaseTitle { get; set; }
            [DisplayName("案件內容")]
            public string CaseContent { get; set; }
            [DisplayName("所在地點")]
            public string Location { get; set; }
        }
    }
}