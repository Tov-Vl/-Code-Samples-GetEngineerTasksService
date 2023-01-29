using System;
using Microsoft.Extensions.Options;

using SsServiceReference;

namespace WFM.InventoryLib.Options
{
    public class JsonScheduleServiceClientSettings : IScheduleServiceClientSettings
    {
        readonly IOptions<ScheduleServiceClientOptions> _scheduleServiceClientOptions;

        public JsonScheduleServiceClientSettings(IOptions<ScheduleServiceClientOptions> scheduleServiceClientOptions)
        {
            _scheduleServiceClientOptions = scheduleServiceClientOptions ?? throw new ArgumentNullException();
        }

        public string RemoteAddress => _scheduleServiceClientOptions.Value?.RemoteAddress;
    }
}
