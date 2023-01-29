using System;
using System.Collections.Generic;
using System.Linq;

using WFM.GetEngineerTasksServiceLib.SoRepositories;
using WFM.So.Repositories;

using SoServiceReference;

namespace WFM.GetEngineerTasksServiceLib.EngineerTasks
{
    public class EngineerTasksRepository: IEngineerTasksRepository
    {
        readonly ISoEngineerRepository _soEngineerRepository;
        readonly ISoAssignmentRepository _soAssignmentRepository;
        readonly ISoTaskRepository _soTaskRepository;
        readonly IRepository<TaskStatus> _soTaskStatusRepository;

        public EngineerTasksRepository(
            ISoEngineerRepository soEngineerRepository,
            ISoAssignmentRepository soAssignmentRepository,
            ISoTaskRepository soTaskRepository,
            IRepository<TaskStatus> soTaskStatusRepository)
        {
            _soAssignmentRepository = soAssignmentRepository ?? throw new ArgumentNullException();
            _soEngineerRepository = soEngineerRepository ?? throw new ArgumentNullException();
            _soTaskRepository = soTaskRepository ?? throw new ArgumentNullException();
            _soTaskStatusRepository = soTaskStatusRepository ?? throw new ArgumentNullException();
        }
        public EngineerTasks GetEngineerTasks(string engLogin, DateTime dateFrom, DateTime dateTo)
        {
            dateTo = dateTo.AddDays(1);

            var soEngineer = _soEngineerRepository.GetEngineer(engLogin);
            var soAssignments = _soAssignmentRepository.GetSoAssignments(soEngineer.Key, dateFrom, dateTo);
            var soTasks = _soTaskRepository.GetTasks(soAssignments.Select(a => a.Task.Key).ToArray());

            var tasksDto = CreateTasksDto(soAssignments, soTasks);

            return new EngineerTasks
            {
                Status_Code = "OK",
                Status_Message = "",
                Data = tasksDto
            };
        }

        private TaskDto[] CreateTasksDto(Assignment[] soAssignments, Task[] soTasks)
        {
            var tasksDtoList = new List<TaskDto>();

            if (soTasks != null && soTasks.Length > 0)
            {
                foreach (var soTask in soTasks)
                {
                    var soAssignment = soAssignments.Where(a => a.Task.Key == soTask.Key).SingleOrDefault();
                    var taskStatus = _soTaskStatusRepository.GetItem(new { Key = soTask.Status.Key }, i => i);

                    var taskDto = new TaskDto
                    {
                        Address = GetAddress(soTask),
                        CallID = soTask.CallID,
                        City = soTask.City,
                        Comment = soTask.personal_info,
                        CustomerName = soTask.ContactName,
                        CustomerPhone = soTask.ContactPhoneNumber,
                        Date = soAssignment.Start.ToString("yyyy-MM-ddThh:mm:ss"),
                        Latitude = soTask.Latitude,
                        Longitude = soTask.Longitude,
                        StatusTaskCode = taskStatus.Code,
                        StatusTaskName = taskStatus.Name,
                        Task = soTask.CustWFMTaskNumber,
                        TaskType = soTask.TaskType.DisplayString
                    };

                    tasksDtoList.Add(taskDto);
                }
            }

            return tasksDtoList.ToArray();
        }

        private string GetAddress(Task soTask)
        {
            string res;

            if(!string.IsNullOrEmpty(soTask.CustObjectAddress))
            {
                res = soTask.CustObjectAddress;
            }
            else
            {
                res = $"ул. {soTask.Street}, д. {soTask.Building}";

                res += string.IsNullOrEmpty(soTask.CustBuildingCorp) ? "" : $"корп. {soTask.CustBuildingCorp}";
                res += string.IsNullOrEmpty(soTask.CustBuildingStr) ? "" : $"стр. {soTask.CustBuildingStr}";

                res += $"кв. {soTask.Flat}";
            }

            return res;
        }
    }
}
