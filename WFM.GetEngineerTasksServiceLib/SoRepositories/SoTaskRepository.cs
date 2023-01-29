using System;
using System.Collections.Generic;
using System.Text;

using WFM.So.Provider;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.SoRepositories
{
    public class SoTaskRepository : ISoTaskRepository
    {
        readonly IProvider<Task> _soTaskProvider;

        public SoTaskRepository(IProvider<Task> soTaskProvider)
        {
            _soTaskProvider = soTaskProvider ?? throw new ArgumentNullException();
        }

        public Task[] GetTasks(int[] taskKeys)
        {
            if (taskKeys == null || taskKeys.Length == 0)
                return null;

            var res = _soTaskProvider.GetItems(taskKeys, i => i);

            return res;
        }
    }
}
