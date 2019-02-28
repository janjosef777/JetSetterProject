using jetsetterProj.Data;
using jetsetterProj.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.Models
{
    public class Rating
    {
        [Key]
        public int RatingID { get; set; }
        public int DiaryID { get; set; }
        public int UserID { get; set; }
        public bool Rate { get; set; }

        // Pointing to parent.
        public virtual ApplicationUser ApplicationUser { get; set; }
       
        public ICollection<Diary> Diary { get; set; }

    }
}
