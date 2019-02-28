using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.ViewModels
{
    public class NewsVM
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public List<ArticlesVM> Articles { get; set;}

    }
}
