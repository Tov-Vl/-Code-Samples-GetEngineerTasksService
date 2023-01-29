using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public class CorrDateEngineerTasksRepository : IEngineerTasksRepository
    {
        public IEngineerTasksRepository Target { get; set;
        }
        public EngineerTasks GetEngineerTasks(string engLogin, DateTime dateFrom, DateTime dateTo)
        {
            if (Target == null)
                throw new ArgumentNullException();

            if (dateTo == default)
                dateTo = dateFrom;

            var res = Target.GetEngineerTasks(engLogin, dateFrom, dateTo);

            return res;
        }
    }
}
