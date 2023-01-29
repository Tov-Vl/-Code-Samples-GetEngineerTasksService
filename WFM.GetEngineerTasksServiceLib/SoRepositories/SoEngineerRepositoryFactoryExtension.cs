using System;
using System.Collections.Generic;
using System.Text;
using Unity.Interception.ContainerIntegration;

using WFM.So.Repositories;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class SoEngineerRepositoryFactoryExtension: UnityCachingRepositoryExtensionBase<Engineer>
    {
        InterceptionBehavior _interceptionBehavior;

        public SoEngineerRepositoryFactoryExtension(InterceptionBehavior interceptionBehavior)
        {
            _interceptionBehavior = interceptionBehavior ?? throw new ArgumentNullException();
        }

        protected override InterceptionBehavior LoggingBehavior => _interceptionBehavior;
    }
}
