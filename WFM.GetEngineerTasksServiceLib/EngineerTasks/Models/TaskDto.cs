using System;
using System.Collections.Generic;
using System.Text;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public class TaskDto
    {
        public string Task { get; set; }

        public string CallID { get; set; }

        public string TaskType { get; set; }

        public string StatusTaskCode { get; set; }

        public string StatusTaskName { get; set; }

        public string Date { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int Latitude { get; set; }

        public int Longitude { get; set; }

        public string CustomerPhone { get; set; }

        public string CustomerName { get; set; }

        public string Comment { get; set; }

    }
}
