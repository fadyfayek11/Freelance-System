using System.Web.Mvc;

namespace WebApplication1.Areas.FreeLancer
{
    public class FreeLancerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "FreeLancer";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "FreeLancer_default",
                "FreeLancer/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}