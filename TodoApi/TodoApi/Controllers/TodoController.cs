using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;

            if(_context.TodoItems.Count()==0)
            {
                _context.TodoItems.Add(new TodoItem { MyName = "Item1" });
                _context.SaveChanges();
            }
        }

        //[HttpGet]
        //public List<TodoItem> GetAll()
        //{
        //    return _context.TodoItems.ToList();
        //}

        //[HttpGet("{id}",Name ="GetTodo")]
        //public IActionResult GetById(long id)
        //{
        //    var item = _context.TodoItems.Find(id);
        //    if(item==null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(item);
        //}

        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }
        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();

            return CreatedAtRoute("GetTodo", new { id = item.Id },item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo=_context.TodoItems.Find(id);
            if(todo==null)
            {
                return NotFound();
            }

            todo.IsComplete=item.IsComplete;
            todo.MyName=item.MyName;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo=_context.TodoItems.Find(id);
            if(todo==null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
