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
            IEnumerable<AllDiaryAdsVM> esList =
                (
                from d in db.Diaries
                from a in db.Ads
                where d.Private == false
                select new AllDiaryAdsVM()
                {
                    DiaryID = d.DiaryID,
                    Tips = d.Tips,
                    DiaryImage = d.Image,
                    Country = d.Country,
                    City = d.City,

                    AdID = a.AdID,
                    AdImage = a.Image

                });
            return esList;
        }
    }
}
