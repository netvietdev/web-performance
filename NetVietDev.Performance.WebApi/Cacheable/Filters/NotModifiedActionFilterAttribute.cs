using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NetVietDev.Performance.WebApi.Cacheable.Filters
{
    public class NotModifiedActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var httpController = actionContext.ControllerContext.Controller as ICacheableController;

            if (httpController == null)
            {
                base.OnActionExecuting(actionContext);
                return;
            }

            var cacheableInfo = httpController.GetCacheableInfo();
            var httpContext = actionContext.Request;

            base.OnActionExecuting(actionContext);
        }
    }
}
