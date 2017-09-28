using System;

namespace WmCrawler.Core.Models
{
    public class MenuItem
    {
        public string FileFrom { get; set; }

        public decimal Sno { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string DispensaryWebsite { get; set; }

        public string Email { get; set; }

        public DateTime? DateMenuLastUpdated { get; set; }

        public DateTime? DateJoined { get; set; }

        public string IsDispensary { get; set; }

        public string IsRecreational { get; set; }

        public string CanDeliver { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public string ProductName { get; set; }

        public string ProductDescription { get; set; }

        public string ProductSize { get; set; }

        public decimal? ProductCost { get; set; }

        public string Source { get; set; }
    }
}
