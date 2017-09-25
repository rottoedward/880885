using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.TotalCase.Models
{
    public class MemberSimpleProfile
    {
        public int MemberID { get; set; }
        public int CaseID { get; set; }

        public string NickName { get; set; }

        public string Email { get; set; }
        public byte[] Photo { get; set; }

        public int sumpoint { get; set; } //依照會員發的case獲得的評價加總 當作分數依據
        //case數量
        //case 人力總數
        public int casecount { get; set; }
        public int GDcasecount { get; set; }
        public int GDcaseneed { get; set; }
        public int GDcasegive { get; set; }

        public int MPcasecount { get; set; }
        public int MPcaseneed { get; set; }
        public int MPcasegive { get; set; }

    

    }
}