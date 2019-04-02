using jetsetterProj.Data;
using JetSetterProject.Models;
using JetSetterProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Repositories
{
    public class VendorAdsRepo
    {
            private ApplicationDbContext db;

            public VendorAdsRepo(ApplicationDbContext db)
            {
                this.db = db;
            }

            public IEnumerable<VendorAdsVM> GetAll(string userID)
            {
                Vendor vendor 
                 = (db.Vendors.Where(va => va.UserID == userID)).FirstOrDefault();

               IEnumerable<VendorAdsVM> esList =
               ( from a in db.Ads
                from v in db.Vendors
                where a.VendorID == vendor.VendorID && 
                        v.UserID == userID
                select new VendorAdsVM ()
                {
                    UserID = userID,
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
