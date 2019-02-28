using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Models
{
    public class Ad
    {
        [Key]
        public int AdID { get; set; }
        public int VendorID { get; set; }
        public bool Published { get; set; }
        public string Description { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Image { get; set; }
        
        // Navigation properties.
        // Child.

        public virtual Vendor Vendor { get; set; }
    }
}
