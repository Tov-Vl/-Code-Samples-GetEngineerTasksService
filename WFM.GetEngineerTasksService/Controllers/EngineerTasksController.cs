using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

using WFM.GetEngineerTasksServiceLib.EngineerTasks;

namespace WFM.GetEngineerTasksService.Controllers
{
    [ApiController]
    [Route("GetEngineerTasksWfmMtsStv")]
    public class EngineerTasksController : ControllerBase
    {
        readonly IEngineerTasksRepository _engineerTasksRepository;

        public EngineerTasksController(IEngineerTasksRepository engineerTasksRepository)
        {
            _engineerTasksRepository = engineerTasksRepository ?? throw new ArgumentNullException();
        }

        [HttpGet]
        public ActionResult<EngineerTasks> Get(string login, DateTime dateFrom, DateTime dateTo)
        {
            var engineerTasks = _engineerTasksRepository.GetEngineerTasks(login, dateFrom, dateTo);

            return Ok(engineerTasks);
        }
    }
}
