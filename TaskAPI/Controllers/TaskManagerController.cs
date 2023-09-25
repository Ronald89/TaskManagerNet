using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskAPI.Models;

namespace TaskAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private readonly TaskContext _context;

        public TaskManagerController(TaskContext context)
        {
            _context = context;
        }

        // GET: api/TaskManager
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTodo>>> GetTaskTodos()
        {
          if (_context.TaskTodos == null)
          {
              return NotFound();
          }
            return await _context.TaskTodos.ToListAsync();
        }

        // GET: api/TaskManager/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskTodo>> GetTaskTodo(int id)
        {
          if (_context.TaskTodos == null)
          {
              return NotFound();
          }
            var taskTodo = await _context.TaskTodos.FindAsync(id);

            if (taskTodo == null)
            {
                return NotFound();
            }

            return taskTodo;
        }

        // PUT: api/TaskManager/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTodo(int id, TaskTodo taskTodo)
        {
            if (id != taskTodo.TaskId)
            {
                return BadRequest();
            }

            _context.Entry(taskTodo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TaskManager
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTodo>> PostTaskTodo(TaskTodo taskTodo)
        {
          if (_context.TaskTodos == null)
          {
              return Problem("Entity set 'TaskContext.TaskTodos'  is null.");
          }
            _context.TaskTodos.Add(taskTodo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskTodo), new { id = taskTodo.TaskId }, taskTodo);
        }

        // DELETE: api/TaskManager/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTodo(int id)
        {
            if (_context.TaskTodos == null)
            {
                return NotFound();
            }
            var taskTodo = await _context.TaskTodos.FindAsync(id);
            if (taskTodo == null)
            {
                return NotFound();
            }

            _context.TaskTodos.Remove(taskTodo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTodoExists(int id)
        {
            return (_context.TaskTodos?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}
