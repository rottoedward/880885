using System.Web.Mvc;

namespace Msit115BestOne.Areas.MyMember
{
    public class MyMemberAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MyMember";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MyMember_default",
                "MyMember/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}