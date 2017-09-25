using System.Web.Mvc;

namespace Msit115BestOne.Areas.ShoppingCart
{
    public class ShoppingCartAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ShoppingCart";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ShoppingCart_default",
                "ShoppingCart/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}