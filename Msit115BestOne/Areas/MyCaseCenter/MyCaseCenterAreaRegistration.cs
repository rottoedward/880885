using System.Web.Mvc;

namespace Msit115BestOne.Areas.MyCaseCenter
{
    public class MyCaseCenterAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MyCaseCenter";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MyCaseCenter_default",
                "MyCaseCenter/{controller}/{action}/{id}",
                new {Controller="MyManPower", action = "Indexx", id = UrlParameter.Optional }
            );
        }
    }
}