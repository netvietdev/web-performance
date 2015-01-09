using System.Web.Mvc;
using NetVietDev.Performance.Cacheable;

namespace NetVietDev.Performance.Mvc.Cache
{
    public interface ICacheableController : IController
    {
        CacheableInfo GetCacheableInfo();
    }
}
