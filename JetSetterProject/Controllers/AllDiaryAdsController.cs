﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using jetsetterProj.Data;
using JetSetterProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace JetSetterProject.Controllers
{
    public class AllDiaryAdsController : Controller
    {
        private ApplicationDbContext db;
        // HttpContextAccessor _httpContextAccessor;


        public AllDiaryAdsController(ApplicationDbContext db)
        {
            this.db = db;
            //     this._httpContextAccessor = httpContextAccessor;

        }

        public IActionResult Index()
        {
            AllDiaryAdsRepo daRepo = new AllDiaryAdsRepo(db);
            var diaryList = daRepo.GetAllDiary();
            var adList = daRepo.GetAllAds();
            ViewBag.Ads = adList.ToList();
            var dList = diaryList.ToList();
            return View(dList);
        }
    }
}