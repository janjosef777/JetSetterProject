﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JetSetterProject.Models;
using jetsetterProj.Data;
using Microsoft.AspNetCore.Authorization;
using JetSetterProject.Repositories;
using System.Security.Claims;

namespace JetSetterProject.Controllers
    //controller
{
    [Authorize(Roles = "Admin, Vendor")]
    public class AdsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ads
        public IActionResult Index()
        {
            AdsRepo adRepo = new AdsRepo(_context);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var es = adRepo.GetAll(userId);
            var esList = es.ToList();
            return View(es);
          //  var applicationDbContext = _context.Ads.Include(a => a.Vendor);
          // return View(await applicationDbContext.ToListAsync());
        }

        // GET: Ads/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Vendor)
                .FirstOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // GET: Ads/Create
        public IActionResult Create()
        {
            AdsRepo adRepo = new AdsRepo(_context);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewData["VendorID"] = adRepo.getVendorID(userId);
            return View();
        }

        // POST: Ads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdID,VendorID,Published,Description,ExpiryDate,Image")] Ad ad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VendorID"] = new SelectList(_context.Vendors, "VendorID", "VendorID", ad.VendorID);
            return View(ad);
        }

        // GET: Ads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            AdsRepo adRepo = new AdsRepo(_context);
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            ViewData["VendorID"] = adRepo.getVendorID(userId);
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads.FindAsync(id);
            if (ad == null)
            {
                return NotFound();
            }
            return View(ad);
        }

        // POST: Ads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdID,VendorID,Published,Description,ExpiryDate,Image")] Ad ad)
        {
            if (id != ad.AdID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdExists(ad.AdID))
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
            ViewData["VendorID"] = new SelectList(_context.Vendors, "VendorID", "VendorID", ad.VendorID);
            return View(ad);
        }

        // GET: Ads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ad = await _context.Ads
                .Include(a => a.Vendor)
                .FirstOrDefaultAsync(m => m.AdID == id);
            if (ad == null)
            {
                return NotFound();
            }

            return View(ad);
        }

        // POST: Ads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ad = await _context.Ads.FindAsync(id);
            _context.Ads.Remove(ad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdExists(int id)
        {
            return _context.Ads.Any(e => e.AdID == id);
        }
    }
}
