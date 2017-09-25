using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Msit115BestOne.Areas.MyMember.Models
{
    public class MemberCaseCount
    {
        ManPowerEntities db = new ManPowerEntities();
       
        public List<int> casecount(int mid)
        { int sumpoint=0;
            List<int> allcasecount = new List<int>();


            var sumR = db.Cases.Where(p => p.MemberID == mid && p.Recommendation!=0).Select(p => p.Recommendation).ToList();
          foreach(var o in sumR)
            {
                sumpoint += (int)o;

            }


        

           
            var m = db.Cases.Where(p => p.MemberID == mid).Select(p=>p.CaseID);
            int casecount = m.Count();
         
            
            int GDcasegive = db.Cases.Where(p => p.MemberID == mid && p.StatusID == 4).Select(p => p.CaseID).Count();

            int GDcaseneed = db.Cases.Where(p => p.MemberID == mid && p.StatusID == 5).Select(p => p.CaseID).Count();
            int GDcasecount = GDcasegive + GDcaseneed;
            int MPcasecount = casecount - GDcasecount;

            int MPcaseneed = db.Cases.Where(p => p.MemberID == mid && p.StatusID == 7).Select(p => p.CaseID).Count();
            int MPcasegive = MPcasecount - MPcaseneed;


            allcasecount.Add(sumpoint); //0
            allcasecount.Add(casecount); //1
            allcasecount.Add(GDcasecount); //2
            allcasecount.Add(MPcasecount); //3 
            allcasecount.Add(GDcasegive); //4
            allcasecount.Add(GDcaseneed); //5
            allcasecount.Add(MPcasegive); //6
            allcasecount.Add(MPcaseneed); //7

            return allcasecount;
            //      int GDcaseneed  5
            //      int GDcasegive PO文者提供物品 4

            //     int MPcaseneed 7
            //    int MPcasegive 8
            
        }

    }
}