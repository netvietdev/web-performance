using System;
using NetVietDev.Performance.Extensions;

namespace NetVietDev.Performance.Cacheable
{
    public class CacheableInfo
    {
        public string EntityId { get; set; }

        public DateTime LastModifiedUtc { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(EntityId) && LastModifiedUtc > DateTime.MinValue;
        }

        public string CalculateEtag()
        {
            var dateToSecond = LastModifiedUtc.GetToSecond();
            var ticksFromSecond = LastModifiedUtc.Subtract(dateToSecond).Ticks;

            return string.Format("{0}^{1}", EntityId, ticksFromSecond);
        }
    }
}
