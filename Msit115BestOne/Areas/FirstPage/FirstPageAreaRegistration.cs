using System.Web.Mvc;

namespace Msit115BestOne.Areas.FirstPage
{
    public class FirstPageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FirstPage";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FirstPage_default",
                "FirstPage/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}