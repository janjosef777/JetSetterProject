using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JetSetterProject.Models;
using jetsetterProj.Data;

namespace JetSetterProject.Controllers
{
    [Authorize(Roles = "Admin,Vendor")]

    public class VendorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private IServiceProvider _serviceProvider;

        public VendorsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IServiceProvider serviceProvider)
        {
            _userManager = userManager;
            _context = context;
            _serviceProvider = serviceProvider;
        }

        // GET: Vendors
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Vendors.Include(v => v.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Vendors/Details/5
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.ApplicationUser)
                .FirstOrDefaultAsync(m => m.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // GET: Vendors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vendors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string Email, string Password, string ConfirmPass, [Bind("VendorID,Name,Address,City,Province,Monthly,Priority,Website,PostalCode,AdPosted")] Vendor vendor)
        {
            UserRoleRepo userRoleRepo = new UserRoleRepo(_serviceProvider);

         
            if (Password != ConfirmPass)
            {
                return View(vendor);
            }
            var user = new ApplicationUser { UserName = Email, Email = Email };
            var result = await _userManager.CreateAsync(user, Password);
            if (result.Succeeded){
                var userID = user.Id;
                vendor.UserID = userID;
                var addUR = await userRoleRepo.AddUserRole(user.Email,"Vendor");
                                                         
            }
            if (ModelState.IsValid)
            {
                
                _context.Add(vendor);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", new { id = vendor.VendorID } );
            }
            return View(vendor);
        }

        // GET: Vendors/Edit/5
        [Authorize(Roles="Admin, Vendor")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors.FindAsync(id);
            if (vendor == null)
            {
                return NotFound();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vendor.UserID);
            return View(vendor);
        }

        // POST: Vendors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Vendor")]
        public async Task<IActionResult> Edit(int id, [Bind("VendorID,UserID,Name,Address,City,Province,Monthly,Priority,Website,PostalCode,AdPosted")] Vendor vendor)
        {
            if (id != vendor.VendorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorExists(vendor.VendorID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", new { id = vendor.VendorID });
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", vendor.UserID);
            return View(vendor);
        }

        // GET: Vendors/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendor = await _context.Vendors
                .Include(v => v.ApplicationUser)
                .FirstOrDefaultAsync(m => m.VendorID == id);
            if (vendor == null)
            {
                return NotFound();
            }

            return View(vendor);
        }

        // POST: Vendors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendor = await _context.Vendors.FindAsync(id);
            _context.Vendors.Remove(vendor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendorExists(int id)
        {
            return _context.Vendors.Any(e => e.VendorID == id);
        }
    }
}
