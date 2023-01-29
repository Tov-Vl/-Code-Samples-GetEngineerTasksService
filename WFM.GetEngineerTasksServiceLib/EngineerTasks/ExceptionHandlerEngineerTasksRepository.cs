using System;
using System.Collections.Generic;
using System.Text;

using WFM.GetEngineerTasksServiceLib.Helpers;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public class ExceptionHandlerEngineerTasksRepository : IEngineerTasksRepository
    {
        public IEngineerTasksRepository Target { get; set;
        }
        public EngineerTasks GetEngineerTasks(string engLogin, DateTime dateFrom, DateTime dateTo)
        {
            EngineerTasks res;

            try
            {
                if (Target == null)
                    throw new ArgumentNullException();

                res = Target.GetEngineerTasks(engLogin, dateFrom, dateTo);

                return res;
            }
            catch (EngineerNotFoundException ex)
            {
                res = new EngineerTasks
                {
                    Status_Code = "ERROR",
                    Status_Message = ex.Message,
                    Data = new TaskDto[] { }
                };

                return res;
            }
        }
    }
}
