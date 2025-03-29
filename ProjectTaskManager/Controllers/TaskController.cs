using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Application.Tasks.Commands;
using TaskManager.Core.Application.Tasks.Mappers;
using TaskManager.Core.Domain.Interfaces;

namespace ProjectTaskManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskRepository _taskRepository;
        private readonly IMediator _mediator;
        public TaskController(ILogger<TaskController> logger, ITaskRepository taskRepository, IMediator mediator)
        {
            _logger = logger;
            _taskRepository = taskRepository;
            _mediator = mediator;
        }
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById([FromQuery] int taskId)
        {
            try
            {
                var task = await _taskRepository.GetByIdAsync(taskId);
                JsonResult result = new JsonResult(TaskMapper.MapToDto(task));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
        }
        [HttpGet("GetTasksByProjectId")]
        public async Task<IActionResult> GetTasksByProjectId([FromQuery] int projectId)
        {
            var task = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            JsonResult result = new JsonResult(task.Select(TaskMapper.MapToDto));
            return Ok(result);
        }
        [HttpGet("GetTasksByUserId")]
        public async Task<IActionResult> GetTasksByUserId([FromQuery] int userId)
        {
            var task = await _taskRepository.GetTasksByUserIdAsync(userId);
            JsonResult result = new JsonResult(task.Select(TaskMapper.MapToDto));
            return Ok(result);
        }
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody] CreateTaskCommand createTaskCommand)
        {
            try
            {
                TaskManager.Core.Domain.Entities.Task task = await _mediator.Send(createTaskCommand);
                return Ok();
            }
            catch (ValidationException validation)
            {
                return BadRequest(validation.Message);
            }
            catch (Exception ex)
            { return StatusCode(500, ex.Message); }
        }
        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTaskCommand updateTaskCommand)
        {
            try
            {
                TaskManager.Core.Domain.Entities.Task task = await _mediator.Send(updateTaskCommand);
                return Ok();
            }
            catch (ValidationException validation)
            {
                return BadRequest(validation.Message);
            }
            catch (Exception ex)
            { return StatusCode(500, ex.Message); }
        }
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromQuery] int taskId)
        {
            try
            {
                DeleteTaskCommand deleteTaskCommand = new DeleteTaskCommand(taskId);
                await _mediator.Send(deleteTaskCommand);

                return NoContent();
            }
            catch (ValidationException validation)
            {
                return BadRequest(validation.Message);
            }
            catch (Exception ex)
            { return StatusCode(500, ex.Message); }
        }
        [HttpPost("AssignTaskToUser")]
        public async Task<IActionResult> AssignTaskToUser([FromQuery] int taskId, [FromQuery] int userId)
        {
            try
            {
                AssignTaskCommand assignTaskCommand = new AssignTaskCommand(taskId, userId);
                await _mediator.Send(assignTaskCommand);

                return Ok();
            }
            catch (ValidationException validation)
            {
                return BadRequest(validation.Message);
            }
            catch (Exception ex)
            { return StatusCode(500, ex.Message); }
        }
    }
}
