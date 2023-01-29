using System;
using Microsoft.Extensions.Options;

using SoServiceReference;

namespace WFM.InventoryLib.Options
{
    public class JsonServiceOptimizationServiceClientSettings : IServiceOptimizationServiceClientSettings
    {
        readonly IOptions<ServiceOptimizationServiceClientOptions> _serviceOptimizationServiceClientOptions;

        public JsonServiceOptimizationServiceClientSettings(IOptions<ServiceOptimizationServiceClientOptions> serviceOptimizationServiceClientOptions)
        {
            _serviceOptimizationServiceClientOptions = serviceOptimizationServiceClientOptions ?? throw new ArgumentNullException();
        }

        public string RemoteAddress => _serviceOptimizationServiceClientOptions.Value?.RemoteAddress;
    }
}
