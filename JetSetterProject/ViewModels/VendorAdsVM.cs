using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.ViewModels
{
    public class VendorAdsVM
    {
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
        }
    }

