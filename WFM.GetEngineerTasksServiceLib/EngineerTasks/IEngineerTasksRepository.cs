using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public interface IEngineerTasksRepository
    {
        EngineerTasks GetEngineerTasks(string engLogin, DateTime dateFrom, DateTime dateTo);
    }
}
