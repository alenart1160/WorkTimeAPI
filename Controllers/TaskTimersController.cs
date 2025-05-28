using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkTimeAPI.Data;
using WorkTimeAPI.Models;

namespace WorkTimeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskTimersController : ControllerBase
    {
        private readonly UserContext _context;

        public TaskTimersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/TaskTimers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskTimer>>> GetTaskTimers()
        {
            return await _context.TaskTimers.ToListAsync();
        }

        // GET: api/TaskTimers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<TaskTimer>>> GetTasksTimerList(long id)
        {
            var tasks = await _context.TaskTimers.Where(x => x.UserID == id).ToListAsync();
            if (tasks == null)
            {
                return NotFound();
            }

            return await _context.TaskTimers.Where(x => x.TaskID == id).ToListAsync();
        }

        // PUT: api/TaskTimers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskTimer(long id, TaskTimer taskTimer)
        {
            if (id != taskTimer.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskTimer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskTimerExists(id))
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

        // POST: api/TaskTimers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TaskTimer>> PostTaskTimer(TaskTimer taskTimer)
        {
            _context.TaskTimers.Add(taskTimer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTaskTimer", new { id = taskTimer.Id }, taskTimer);
        }

        // DELETE: api/TaskTimers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskTimer(long id)
        {
            var taskTimer = await _context.TaskTimers.FindAsync(id);
            if (taskTimer == null)
            {
                return NotFound();
            }

            _context.TaskTimers.Remove(taskTimer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskTimerExists(long id)
        {
            return _context.TaskTimers.Any(e => e.Id == id);
        }
    }
}
