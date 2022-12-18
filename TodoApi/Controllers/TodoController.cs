using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTOs;
using TodoApi.Models;

namespace TodoApi.Controllers {

    [Route("api/todo")]
    [ApiController]
    public class TodoItemsController : ControllerBase {

        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context) { _context = context; }

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItemGetDto>>> GetTodoItems() {
            return await _context.TodoItems
                .Select(x => TodoItemGetDto.ToDto(x))
                .ToListAsync();
        }

        // GET: api/TodoItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemGetDto>> GetTodoItem(long id) {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            return TodoItemGetDto.ToDto(todoItem);
        }

        // PUT: api/TodoItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItemPostDto todoDTO) {
            // if (id != todoDTO.Id) return BadRequest();

            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            todoItem.Name = todoDTO.Name;
            todoItem.IsComplete = todoDTO.IsComplete;
            
            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException) when (!TodoItemExists(id)) {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/TodoItems
        [HttpPost]
        public async Task<ActionResult<TodoItemGetDto>> PostTodoItem(TodoItemPostDto todoDTO) {
            var todoItem = TodoItemPostDto.ToItem(todoDTO);

            _context.TodoItems.Add(todoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetTodoItem),
                new { id = todoItem.Id },
                TodoItemGetDto.ToDto(todoItem)
            );
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id) {
            var todoItem = await _context.TodoItems.FindAsync(id);
            if (todoItem == null) return NotFound();

            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoItemExists(long id) {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
