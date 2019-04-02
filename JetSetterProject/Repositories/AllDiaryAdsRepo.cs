using jetsetterProj.Data;
using JetSetterProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Repositories
{
    public class AllDiaryAdsRepo

    {
        private ApplicationDbContext db;
        
        public AllDiaryAdsRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<AllDiaryAdsVM> GetAllDiary()
        {
            IEnumerable<AllDiaryAdsVM> dList =
                (
                from d in db.Diaries
            
                where d.Private == false
                select new AllDiaryAdsVM()
                {
                    DiaryID = d.DiaryID,
                    Tips = d.Tips,
                    DiaryImage = d.Image,
                    Country = d.Country,
                    City = d.City,

                });
            return dList;
        }
        public IEnumerable<AllDiaryAdsVM> GetAllAds()
        {
            IEnumerable<AllDiaryAdsVM> aList =
                (
                from a in db.Ads
                where a.Published == true
                select new AllDiaryAdsVM()
                {
                     AdID = a.AdID,
                    AdImage = a.Image

                });
            return aList;
        }
    }
}
