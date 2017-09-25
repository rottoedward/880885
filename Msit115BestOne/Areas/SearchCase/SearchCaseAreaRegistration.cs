using System.Web.Mvc;

namespace Msit115BestOne.Areas.SearchCase
{
    public class SearchCaseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SearchCase";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "SearchCase_default",
                "SearchCase/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}