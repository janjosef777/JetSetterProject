using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jetsetterProj.Data;
using jetsetterProj.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetSetterProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DiaryApiController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/DiaryApi
        [HttpGet]
        public IEnumerable<Diary> Get()
        {
            return _context.Diaries.Where(x => x.Private != true).ToList();
        }

        // GET: api/DiaryApi/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var item = _context.Diaries.FirstOrDefault(t => t.DiaryID == id);
            if (item == null || item.Private == true)
            {
                return NotFound();
            }
            return new ObjectResult(item);

        }

        // POST: api/DiaryApi
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/DiaryApi/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
