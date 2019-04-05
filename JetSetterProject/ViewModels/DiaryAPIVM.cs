using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.ViewModels
{
    public class DiaryAPIVM
    {
        public int DiaryID { get; set; }
        public DateTime DateStamp { get; set; }
        public string Tips { get; set; }
        public string DiaryEntry { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public bool Private { get; set; }
        public string Image { get; set; }
    }
}
