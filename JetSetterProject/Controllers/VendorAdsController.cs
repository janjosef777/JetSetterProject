using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using jetsetterProj.Data;
using JetSetterProject.Repositories;
using JetSetterProject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JetSetterProject.Controllers
{
    public class VendorAdsController : Controller
    {
        private ApplicationDbContext db;
       // HttpContextAccessor _httpContextAccessor;


        public VendorAdsController(ApplicationDbContext db)
        {
            this.db = db;
       //     this._httpContextAccessor = httpContextAccessor;

        }

        public ActionResult Index()
        {
            VendorAdsRepo vaRepo = new VendorAdsRepo(db);
            //string userId = _httpContextAccessor.HttpContext.User
            //                .FindFirst(ClaimTypes.NameIdentifier).Value;

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                // User.Identity.Name;

            var es = vaRepo.GetAll(userId);
            var esList = es.ToList();
            return View(es);
        }
    }
}