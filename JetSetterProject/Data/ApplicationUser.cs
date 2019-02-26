using jetsetterProj.Models;
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
    }
}
