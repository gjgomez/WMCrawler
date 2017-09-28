using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WmCrawler.WmHttpClient.Requests.Listings
{
    internal class ListingRequest : Getable
    {
        [Required]
        public string RegionSlug { get; set; }

        public override string Endpoint => $"https://api-v2.weedmaps.com/api/v2/listings?filter[plural_types][]=dispensaries&filter[plural_types][]=deliveries&filter[region_slug[deliveries]]={RegionSlug}&filter[region_slug[dispensaries]]={RegionSlug}&page_size=150&size=150";

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { };
        }
    }
}
