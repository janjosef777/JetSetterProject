﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jetsetterProj.Data;
using jetsetterProj.Models;
using Microsoft.AspNetCore.Authorization;
using HttpPostedFileHelper;
using JetSetterProject.Repositories;
using System.Security.Claims;

namespace JetSetterProject.Controllers
//controller
{

    [Authorize(Roles = "Admin,Traveler")]
    public class DiariesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiariesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diaries
        public IActionResult Index()
        {
            DiariesRepo diRepo = new DiariesRepo(_context);

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var es = diRepo.GetAll(userId);
            var esList = es.ToList();
            return View(es);

           // var applicationDbContext = _context.Diaries.Include(d => d.ApplicationUser);
           // return View(await applicationDbContext.ToListAsync());
        }

        public IActionResult API()
        {
            return View();
        }

        // GET: Diaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiaryID == id);
            if (diary == null)
            {
                return NotFound();
            }

            return View(diary);
        }

        // GET: Diaries/Create
        public IActionResult Create()
        {
            ViewData["UserID"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return View();
        }

        // POST: Diaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiaryID,UserID,ActualDate,DateStamp,Tips,DiaryEntry,Country,City,Private,Image")] Diary diary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", diary.UserID);
            return View(diary);
        }


        // GET: Diaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries.FindAsync(id);
            if (diary == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewData["DiaryID"] = id;

            return View(diary);
        }


        // POST: Diaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiaryID,UserID,ActualDate,DateStamp,Tips,DiaryEntry,Country,City,Private,Image")] Diary diary)
        {
            if (id != diary.DiaryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiaryExists(diary.DiaryID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", diary.UserID);
            return View(diary);
        }

        // GET: Diaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diary = await _context.Diaries
                .Include(d => d.ApplicationUser)
                .FirstOrDefaultAsync(m => m.DiaryID == id);
            if (diary == null)
            {
                return NotFound();
            }

            return View(diary);
        }

        // POST: Diaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diary = await _context.Diaries.FindAsync(id);
            _context.Diaries.Remove(diary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiaryExists(int id)
        {
            return _context.Diaries.Any(e => e.DiaryID == id);
        }

    }
}
