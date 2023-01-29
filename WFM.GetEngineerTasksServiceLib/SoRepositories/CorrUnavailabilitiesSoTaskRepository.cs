using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class CorrUnavailabilitiesSoTaskRepository : ISoTaskRepository
    {
        public ISoTaskRepository Target { get; set; }

        public Task[] GetTasks(int[] taskKeys)
        {
            if (Target == null)
                throw new ArgumentNullException();

            taskKeys = taskKeys.Where(i => i != -1).ToArray();

            var res = Target.GetTasks(taskKeys);

            return res;
        }
    }
}
