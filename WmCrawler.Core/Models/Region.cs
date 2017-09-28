using System.Collections.Generic;

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
