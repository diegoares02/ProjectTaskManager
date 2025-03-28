using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Domain.Interfaces;

namespace ProjectTaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }
        [HttpGet("GetById")]
        public IActionResult GetById([FromQuery] int taskId)
        {
            var task = _taskRepository.GetByIdAsync(taskId);
            JsonResult result = new JsonResult(task);
            return Ok(result);
        }
    }
}
