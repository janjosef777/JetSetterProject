using jetsetterProj.Data;
using jetsetterProj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Repositories
{
    public class DiariesRepo
    {
        private ApplicationDbContext db;

        public DiariesRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Diary> GetAll(string userID)
        {
            Diary diary = (db.Diaries.Where(di => di.UserID == userID)).FirstOrDefault();

            IEnumerable<Diary> esList =
                (from d in db.Diaries
                 where d.UserID == userID
                 select new Diary()
                 {
                     UserID = userID,
                     DiaryID = d.DiaryID,
                     Tips = d.Tips,
                     ActualDate = d.ActualDate,
                     DateStamp = d.DateStamp,
                     DiaryEntry = d.DiaryEntry,
                     Country = d.Country,
                     City = d.City,
                     Private = d.Private,
                     Image = d.Image
                 });

            return esList;
        }


    }
}
