using System.Web.Mvc;

namespace Msit115BestOne.Areas.MyEva
{
    public class MyEvaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MyEva";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MyEva_default",
                "MyEva/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}