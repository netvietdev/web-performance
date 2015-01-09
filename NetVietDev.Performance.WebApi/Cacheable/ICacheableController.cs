using System.Web.Http.Controllers;
using NetVietDev.Performance.Cacheable;

namespace NetVietDev.Performance.WebApi.Cacheable
{
    public interface ICacheableController : IHttpController
    {
        CacheableInfo GetCacheableInfo();
    }
}
