using System;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetVietDev.Performance.Cacheable;
using NetVietDev.Performance.Extensions;

namespace NetVietDev.Performance.Mvc.Cache.Filters
{
    public class NotModifiedActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var cacheableInfoController = filterContext.Controller as ICacheableController;

            if (cacheableInfoController == null)
            {
                base.OnActionExecuting(filterContext);
                return;
            }

            var cacheableInfo = cacheableInfoController.GetCacheableInfo();
            var httpContext = filterContext.HttpContext;

            if (cacheableInfo.IsValid() && IsNotModified(cacheableInfo, httpContext.Request))
            {
                httpContext.Response.ClearContent();
                httpContext.Response.SuppressContent = true;
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotModified;
            }
            else
            {
                httpContext.Response.Cache.SetCacheability(HttpCacheability.Public);
                httpContext.Response.Cache.SetLastModified(cacheableInfo.LastModifiedUtc);
                httpContext.Response.Cache.SetETag(cacheableInfo.CalculateEtag());
                base.OnActionExecuting(filterContext);
            }
        }

        private bool IsNotModified(CacheableInfo cacheableInfo, HttpRequestBase request)
        {
            var etag = request.Headers["If-None-Match"];
            var modifiedSince = request.Headers["If-Modified-Since"];

            var calculatedEtag = cacheableInfo.CalculateEtag();
            if (calculatedEtag != etag)
            {
                return false;
            }

            DateTime lastModified;
            DateTime.TryParse(modifiedSince, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out lastModified);

            return cacheableInfo.LastModifiedUtc.GetToSecond() == lastModified;
        }
    }
}
