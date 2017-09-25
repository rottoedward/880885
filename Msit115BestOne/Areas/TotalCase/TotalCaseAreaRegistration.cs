using System.Web.Mvc;

namespace Msit115BestOne.Areas.TotalCase
{
    public class TotalCaseAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TotalCase";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TotalCase_default",
                "TotalCase/{controller}/{action}/{id}",
                new { controller = "Goods", action = "GoodsIndex", id = UrlParameter.Optional }
            );
        }
    }
}