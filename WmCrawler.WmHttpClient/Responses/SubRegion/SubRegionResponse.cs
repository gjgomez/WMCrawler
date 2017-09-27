using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WmCrawler.WmHttpClient.Responses.SubRegion
{
    public  class SubRegionResponse
    {
        [JsonProperty("data")]
        public Data DataContainer { get; set; } = new Data();

        public override string ToString()
        {
            return $"Total SubRegions: {DataContainer.SubRegions.Count}";
        }

        public class Data
        {
            [JsonProperty("subregions")]
            public ICollection<SubRegion> SubRegions { get; set; } = new List<SubRegion>();

            public class SubRegion
            {
                [JsonProperty("slug")]
                public string Slug { get; set; }
            }
        }
    }
}
