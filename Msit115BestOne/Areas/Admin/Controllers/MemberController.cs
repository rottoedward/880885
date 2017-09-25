using Msit115BestOne.Areas.Admin.Models;
using Msit115BestOne.Areas.Admin.Service;
using Msit115BestOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Msit115BestOne.Areas.Admin.Models.MemberGroup;
using System.Text;

namespace Msit115BestOne.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        ManPowerEntities db = new ManPowerEntities();
        AdminGR<Member> MemberDB = new AdminGR<Member>();
        Interception IL = new Interception();

        // GET: Admin/Member
        public ActionResult BrowseMemberAll()
        {
            List<MemberViewModel> members = new List<MemberViewModel>();
            foreach (var ms in (MemberDB.GetAll()).ToList())
            {
                MemberViewModel member = new MemberViewModel()
                {
                    MemberID = ms.MemberID,
                    MAccount = ms.MAccount,
                    Birthday = ms.Birthday,
                    RegionAddress = db.Region.Where(x => x.RegionID == ms.RegionID).Select(x => x.City.CityName + x.RegionName).First() + ms.Address,
                    Phone = ms.Phone,
                    Email = ms.Email
                };
                members.Add(member);
            }
            return IL.InterceptionLogin(View(members), Session["AdminID"]);
        }

        [HttpGet]
        public ActionResult EditMember(int MemberID)
        {
            Member mDB = MemberDB.GetById(MemberID);
            MemberByIdViewModel member = new MemberByIdViewModel()
            {
                MemberID = mDB.MemberID,
                MAccount = mDB.MAccount,
                MPassword = mDB.MPassword,
                Birthday = mDB.Birthday,
                Email = mDB.Email,
                Phone = mDB.Phone,
                Address = mDB.Address,
                RegionID = mDB.RegionID
            };

            List<SelectListItem> myCitySelectItem = new List<SelectListItem>();

            foreach (var ct in (db.City).ToList())
            {
                SelectListItem city = new SelectListItem()
                {
                    Text = ct.CityName,
                    Value = ct.CityID.ToString()
                };

                if (ct.CityID == mDB.Region.CityID)
                    city.Selected = true;
                else
                    city.Selected = false;

                myCitySelectItem.Add(city);
            }
            ViewBag.SelectCity = myCitySelectItem;


            List<SelectListItem> myRegionSelectItem = new List<SelectListItem>();

            foreach (var rg in (db.Region.Where(x=>x.CityID == mDB.Region.CityID)).ToList())
            {
                SelectListItem region = new SelectListItem()
                {
                    Text = rg.RegionName,
                    Value = rg.RegionID.ToString()
                };

                if (rg.RegionID == mDB.RegionID)
                    region.Selected = true;
                else
                    region.Selected = false;

                myRegionSelectItem.Add(region);
            }
            ViewBag.SelectRegion = myRegionSelectItem;

            return IL.InterceptionLogin(View(member), Session["AdminID"], "準備修改會員『" + mDB.MAccount + "』", "進行修改", "瀏覽會員頁面");
        }

        [HttpPost]
        public ActionResult EditMember(Member member)
        {
            Member memberInfo = MemberDB.GetById(member.MemberID);
            memberInfo.MemberID = member.MemberID;
            memberInfo.MAccount = member.MAccount;
            memberInfo.MPassword = member.MPassword;
            memberInfo.Birthday = member.Birthday;
            memberInfo.Phone = member.Phone;
            memberInfo.Email = member.Email;
            memberInfo.RegionID = member.RegionID;
            memberInfo.Address = member.Address;
            MemberDB.Update(memberInfo, Session["AdminID"].ToString(), "修改會員『" + memberInfo.MAccount + "』", "修改會員頁面");

            return RedirectToAction("BrowseMemberAll","Member");
        }

        public ActionResult GetRegionByCityAll(string cityId)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var rg in (db.Region.Where(x=>x.CityID.ToString() == cityId)).ToList())
            {
                sb.AppendFormat("<option value=\"{0}\">{1}</option>", rg.RegionID, rg.RegionName);
            }
            return Content(sb.ToString());
        }
    }
}