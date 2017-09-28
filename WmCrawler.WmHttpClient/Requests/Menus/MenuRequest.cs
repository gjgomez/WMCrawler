using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WmCrawler.WmHttpClient.Requests.Menus
{
    internal class MenuRequest : Getable
    {
        [Required]
        public string ListingSlug { get; set; }

        [Required]
        public string ListingType { get; set; }

        public override string Endpoint => $"https://weedmaps.com/api/web/v1/listings/{ListingSlug}/menu?type={ListingType}";

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new ValidationResult[] { };
        }
    }
}
