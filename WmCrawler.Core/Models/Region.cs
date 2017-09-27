using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmCrawler.Core.Models
{
    public class Region
    {
        public string Slug { get; set; }

        public ICollection<Region> SubRegions { get; set; }

        public Region(string slug)
        {
            Slug = slug;
            SubRegions = new List<Region>();
        }
    }
}
