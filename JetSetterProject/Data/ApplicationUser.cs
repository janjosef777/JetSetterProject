using jetsetterProj.Models;
using JetSetterProject.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace jetsetterProj.Data
{
    public class ApplicationUser:IdentityUser 
    {
        public virtual ICollection<Diary> Diaries { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<Vendor> Vendors { get; set; }
    }
}
