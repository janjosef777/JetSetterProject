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


            /*
                         [DisplayName("Vendor ID")] // Give nice label name for CRUD.
            public int VendorID { get; set; }
            [DisplayName("User ID")]   // Give nice label name for CRUD.
            public string UserID { get; set; }
            [DisplayName("Ad ID")]  // Give nice label name for CRUD.
            public int AdID { get; set; }
            [DisplayName("Is Ad published")]  // Give nice label name for CRUD.
            public bool Published { get; set; }
            [DisplayName("Ad Description")]  // Give nice label name for CRUD.
            public string Description { get; set; }
            [DisplayName("Ad Expirey Date")]  // Give nice label name for CRUD.
            public DateTime ExpiryDate { get; set; }
            [DisplayName("Ad Image")]  // Give nice label name for CRUD.
            public string Image { get; set; }
            */
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
