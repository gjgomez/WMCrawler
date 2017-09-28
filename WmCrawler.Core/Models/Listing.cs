namespace WmCrawler.Core.Models
{
    public class Listing
    {
        public Listing(string slug, string type)
        {
            Slug = slug;
            Type = type;
        }

        public string Slug { get; set; }

        public string Type { get; set; }
    }
}
