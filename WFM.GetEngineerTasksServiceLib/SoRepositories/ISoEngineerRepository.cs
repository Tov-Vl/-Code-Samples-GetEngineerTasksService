using System;
using System.Collections.Generic;
using System.Text;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public interface ISoEngineerRepository
    {
        public Engineer GetEngineer(string engLogin);
    }
}
