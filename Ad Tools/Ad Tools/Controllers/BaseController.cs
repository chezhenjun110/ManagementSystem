using System.Web.Mvc;

namespace Ad_Tools.Controllers
{
    public class BaseController:Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["username"] == null)
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(
new { action = "Login", controller = "Home" }));

            base.OnActionExecuting(filterContext);
        }
    }
}