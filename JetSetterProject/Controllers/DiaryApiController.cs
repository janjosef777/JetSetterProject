using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jetsetterProj.Data;
using jetsetterProj.Models;
using JetSetterProject.ViewModels;
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
        public IEnumerable<DiaryAPIVM> Get()
        {
            var diaries = from d in _context.Diaries
                          where d.Private != true
                          select new DiaryAPIVM {
                              DiaryID = d.DiaryID, DateStamp = d.DateStamp,
                              Tips = d.Tips, DiaryEntry = d.DiaryEntry,
                              Country = d.Country, Image = d.Image
                          };
            return diaries;
        }

        // GET: api/DiaryApi/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var item = from d in _context.Diaries
                       where d.Private != true && d.DiaryID == id
                       select new DiaryAPIVM
                       {
                           DiaryID = d.DiaryID,
                           DateStamp = d.DateStamp,
                           Tips = d.Tips,
                           DiaryEntry = d.DiaryEntry,
                           Country = d.Country,
                           Image = d.Image
                       };
            if (item == null)
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
