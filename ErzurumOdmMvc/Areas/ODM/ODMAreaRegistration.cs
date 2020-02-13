using System.Web.Mvc;

namespace ErzurumOdmMvc.Areas.ODM
{
    public class ODMAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ODM";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ODM_default",
                "ODM/{controller}/{action}/{id}",
                new { controller = "Default", action = "Index", id = UrlParameter.Optional }, new[] { "ErzurumOdmMvc.Areas.ODM.Controllers" }
            );
        }
    }
}