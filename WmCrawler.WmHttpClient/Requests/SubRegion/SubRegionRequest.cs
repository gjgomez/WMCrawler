using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WmCrawler.WmHttpClient.Requests.SubRegion
{
    internal class SubRegionRequest : Getable
    {
        [Required]
        public string Slug { get; set; }

        public override string Endpoint => "/regions";

        internal override string PathAndQuery => $"{Endpoint}/{Slug}/subregions";

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { };
        }
    }
}
