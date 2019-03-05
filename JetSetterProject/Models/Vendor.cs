using jetsetterProj.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Models
{
    public class Vendor
    {
        [Key]
        public int VendorID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public bool Monthly { get; set; }
        public bool Priority { get; set; }
        public string Website { get; set; }
        public string PostalCode { get; set; }
        [Range(0,5)]
        public int AdPosted { get; set; }

        // Navigation properties.
        // Child.        
        //public virtual ICollection<Ratings>
        //    Ratings
        //{ get; set; }


        // Pointing to parent.
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual ICollection<Ad> Ads { get; set; }

    }
}
