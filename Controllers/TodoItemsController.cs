using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class TodoItemsController : ControllerBase
  {
    private readonly TodoService _todoService;
    private readonly ILogger _logger;

    public TodoItemsController(
      TodoService todoService,
      ILogger<TodoItemsController> logger)
    {
      _todoService = todoService;
      _logger = logger;
    }


    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> Get() =>
      await _todoService.GetTodoItems();


    [HttpGet("{id:length(24)}", Name = "GetTodoItem")]
    public async Task<ActionResult<TodoItem>> Get(string id)
    {

      _logger.LogInformation("Getting item {Id}", id);

      var todoItem = await _todoService.GetTodoItem(id);

      if (todoItem == null)
      {
        _logger.LogWarning($"Item {id} not found");
        return NotFound();
      }

      return todoItem;
    }


    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem todoItem)
    {
      await _todoService.Create(todoItem);

      _logger.LogInformation($"Creating new todo item");

      return CreatedAtRoute(
        "GetTodoItem",
        new { id = todoItem.Id.ToString() },
        todoItem);
    }


    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, TodoItem todoItemIn)
    {
      var todoItem = await _todoService.GetTodoItem(id);

      if (todoItem == null)
      {
        _logger.LogWarning($"Item {id} not found");
        return NotFound();
      }

      await _todoService.Update(id, todoItemIn);

      return NoContent();
    }


    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
      var todoItem = await _todoService.GetTodoItem(id);

      if (todoItem == null)
      {
        _logger.LogWarning($"Item {id} not found");
        return NotFound();
      }

      _logger.LogInformation($"Deleting item {id}");

      await _todoService.Remove(todoItem.Id);

      return NoContent();
    }
  }
}