using System;
using System.Collections.Generic;
using System.Text;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public interface ISoTaskRepository
    {
        public Task[] GetTasks(int[] taskKeys);
    }
}
