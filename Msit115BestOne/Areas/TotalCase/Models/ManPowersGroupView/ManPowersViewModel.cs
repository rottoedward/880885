using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.TotalCase.Models.ManPowersGroupView
{
    public class ManPowersViewModel
    {

        //member
        public string NickName { get; set; }

        //MP
        public string MPName { get; set; }
        public int CaseID { get; set; }
        public int MemberID { get; set; }

        public int MPNeedCount { get; set; }
        public int MPActuralCount { get; set; }


        // Cases
        public string CaseTitle { get; set; }
        public int StatusID { get; set; }
        //CaseStatus
        public string StatusName { get; set; }


        // MPClass
        public string MPClass1 { get; set; }

        // Picture
        //public byte[] Images { get; set; }

        






    }
}