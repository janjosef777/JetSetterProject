using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JetSetterProject.ViewModels
{
    public class ArticlesVM
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
        public string URLToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }
    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
