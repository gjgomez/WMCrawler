using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WmCrawler.WmHttpClient.Requests.SubRegions
{
    internal class SubRegionRequest : Getable
    {
        [Required]
        public string RegionSlug { get; set; }

        public override string Endpoint => $"https://weedmaps.com/api/v1/regions/{RegionSlug}/subregions";

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { };
        }
    }
}
