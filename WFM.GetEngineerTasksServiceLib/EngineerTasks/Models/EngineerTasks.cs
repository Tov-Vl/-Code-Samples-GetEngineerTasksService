using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public class EngineerTasks
    {
        public string Status_Code { get; set; }

        public string Status_Message { get; set; }

        public TaskDto[] Data { get; set; }
    }
}
