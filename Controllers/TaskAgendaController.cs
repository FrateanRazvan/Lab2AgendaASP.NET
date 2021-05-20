using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project1.Data;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskAgendaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TaskAgendaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskAgenda
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskAgenda>>> GetTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        [HttpGet]
        [Route("filter/startDate={startDate:datetime}&endDate={endDate:datetime}")]
        public async Task<ActionResult<IEnumerable<TaskAgenda>>> FilterByTaskDeadline(DateTime startDate, DateTime endDate)
        {

            return await _context.Tasks.Where(task => startDate < task.DateTimeDeadline && task.DateTimeDeadline < endDate).ToListAsync();
        }


        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForTask(int id, Comment comment)
        {
            comment.Task = _context.Tasks.Find(id);
            if (comment.Task == null)
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<Comment>> GetCommentForTask(int id)
        {
            return _context.Comments.Where(comm => comm.Task.Id == id).ToList();
        }

        // GET: api/TaskAgenda/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskAgenda>> GetTaskAgenda(int id)
        {
            var taskAgenda = await _context.Tasks.FindAsync(id);

            if (taskAgenda == null)
            {
                return NotFound();
            }

            return taskAgenda;
        }

        // PUT: api/TaskAgenda/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskAgenda(int id, TaskAgenda taskAgenda)
        {
            if (id != taskAgenda.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskAgenda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskAgendaExists(id))
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

        // POST: api/TaskAgenda
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskAgenda>> PostTaskAgenda(TaskAgenda taskAgenda)
        {
            _context.Tasks.Add(taskAgenda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskAgenda", new { id = taskAgenda.Id }, taskAgenda);
        }

        // DELETE: api/TaskAgenda/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskAgenda(int id)
        {
            var taskAgenda = await _context.Tasks.FindAsync(id);
            if (taskAgenda == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskAgenda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/comments/5
        [HttpDelete("comments/{id}")]
        public async Task<IActionResult> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskAgendaExists(int id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
