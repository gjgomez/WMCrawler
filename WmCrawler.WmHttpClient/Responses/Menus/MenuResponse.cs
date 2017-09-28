using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace WmCrawler.WmHttpClient.Responses.Menus
{
    public class MenuResponse
    {
        [JsonProperty("menu_updated")]
        public DateTime? DateMenuLastUpdated { get; set; }

        [JsonProperty("listing")]
        public Listing ListingContainer { get; set; } = new Listing();

        [JsonProperty("categories")]
        public ICollection<Category> Categories { get; set; } = new Collection<Category>();

        public override string ToString()
        {
            return $"Name: {ListingContainer.Name}... Total Categories: {Categories.Count}";
        }

        public class Listing
        {
            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("phone_number")]
            public string Phone { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("state")]
            public string State { get; set; }

            [JsonProperty("zip_code")]
            public string ZipCode { get; set; }

            [JsonProperty("website")]
            public string DispensaryWebsite { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("listing_url")]
            public string ListingURL { get; set; }

            [JsonProperty("is_recrational")]
            public bool IsRecreational { get; set; }

            [JsonProperty("is_delivery")]
            public bool CanDeliver { get; set; }

            [JsonProperty("_type")]
            public string TypeOfBusiness { get; set; }

            public bool IsDispensaryBusinessType => TypeOfBusiness == "dispensary";

            public string IsRecreationalString => IsRecreational ? "Yes" : "No";

            public string CanDeliveryString => CanDeliver ? "Yes" : "No";

            public string IsDispensaryString => IsDispensaryBusinessType ? "Yes" : "No";
        }

        public class Category
        {
            [JsonProperty("title")]
            public string Name { get; set; }

            [JsonProperty("items")]
            public ICollection<Product> Products { get; set; } = new Collection<Product>();

            public override string ToString()
            {
                return $"Name: {Name}... Total Products: {Products.Count}";
            }

            public class Product
            {
                [JsonProperty("name")]
                public string Name { get; set; }

                [JsonProperty("body")]
                public string Description { get; set; }

                [JsonProperty("prices")]
                public Price PriceData { get; set; } = new Price();

                public override string ToString()
                {
                    return $"Name: {Name}";
                }

                public class Price
                {
                    [JsonProperty("gram")]
                    public decimal? OneGram { get; set; }

                    [JsonProperty("two_grams")]
                    public decimal? TwoGrams { get; set; }

                    [JsonProperty("eighth")]
                    public decimal? OneEightOnce { get; set; }

                    [JsonProperty("quarter")]
                    public decimal? OneQuarterOnce { get; set; }

                    [JsonProperty("half_ounce")]
                    public decimal? OneHalfOnce { get; set; }

                    [JsonProperty("ounce")]
                    public decimal? OneOunce { get; set; }

                    public bool HasPrice => (OneGram.HasValue && OneGram.Value > 0) || (TwoGrams.HasValue && TwoGrams.Value > 0) || (OneEightOnce.HasValue && OneEightOnce.Value > 0) ||
                                            (OneQuarterOnce.HasValue && OneQuarterOnce.Value > 0) || (OneHalfOnce.HasValue && OneHalfOnce.Value > 0) || (OneOunce.HasValue && OneOunce.Value > 0);
                }
            }
        }
    }
}
