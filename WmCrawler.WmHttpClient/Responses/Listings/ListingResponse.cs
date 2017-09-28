using Newtonsoft.Json;
using System.Collections.Generic;

namespace WmCrawler.WmHttpClient.Responses.Listings
{
    public class ListingResponse
    {
        [JsonProperty("data")]
        public Data DataContainer { get; set; } = new Data();

        public class Data
        {
            [JsonProperty("listings")]
            public ICollection<Listing> Dispensaries { get; set; } = new List<Listing>();

            public class Listing
            {
                [JsonProperty("slug")]
                public string Slug { get; set; }

                public override string ToString()
                {
                    return $"Slug: {Slug}";
                }
            }
        }
    }
}
