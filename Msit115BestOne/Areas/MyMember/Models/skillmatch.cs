using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyMember.Models
{
    public class skillmatch
    {

        //找到這會員的專長以及居住地
        //找到全部case內跟這會員"專長" "地區"有關的 
        //專長有關的案件為人力需求案件
        //地區案件不限
        ManPowerEntities db = new ManPowerEntities();

        public List<int> Match(int id)//登入會員ID
        {
            int mid = id;
            int regionid = db.Member.Find(mid).RegionID;

            var matchcase = db.Cases.Where(p => p.RegionID == regionid && p.MemberID != mid).Select(p => p.CaseID); //"符合地區的caseID"
            var matchskill = db.MPSCList.Where(p => p.MemberID == mid).Select(p => p.MPSubClassID).ToList();//會員專長

            List<int> mpcase = new List<int>(); //"符合專長的caseID"



            for (int i = 0; i < matchskill.Count; i++)
            {
                int mat = matchskill[i];
                var q = db.ManPower.Where(p => p.MPSubClassID == mat);

                foreach (var a in q)
                {

                    mpcase.Add(a.CaseID);
                }

            }

            foreach (var w in matchcase)
            {

                mpcase.Add(w);
            }


            List<int> matchcaseall = mpcase.Distinct().ToList(); //移除重複
         

            return matchcaseall;



        }




    }
}