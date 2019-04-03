using jetsetterProj.Data;
using JetSetterProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Repositories
{
    public class AdsRepo
    {
        private ApplicationDbContext db;

        public AdsRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Ad> GetAll(string userID)
        {
            Vendor vendor = (db.Vendors.Where(ve => ve.UserID == userID)).FirstOrDefault();

            IEnumerable<Ad> esList =
                (from a in db.Ads
                 where a.VendorID == vendor.VendorID
                 select new Ad()
                 {
                     AdID = a.AdID,
                     Published = a.Published,
                     Description = a.Description,
                     ExpiryDate = a.ExpiryDate,
                     Image = a.Image
                 });

            return esList;
        }
    }
}
