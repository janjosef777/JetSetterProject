using jetsetterProj.Data;
using JetSetterProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace jetsetterProj.Models
{

    public class Diary
    {
        [Key]
        public int DiaryID { get; set; }
        public string UserId { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime DateStamp { get; set; }
        public string Tips { get; set; }
        public string DiaryEntry { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool Private { get; set; }
        public string Image { get; set; }

        // Navigation properties.
        // Child.        
        //public virtual ICollection<Ratings>
        //    Ratings
        //{ get; set; }


        // Pointing to parent.
        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
