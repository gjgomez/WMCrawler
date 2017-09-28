using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.Core.Services;
using WmCrawler.WmHttpClient;

namespace WmCrawler.Infrastructure.Services
{
    public class CrawlerService : ICrawlerService
    {
        private readonly WmClient _httpClient;

        public CrawlerService()
        {
            _httpClient = new WmClient();
        }

        public async Task<Region> GetStorefrontRegionsAsync()
        {
            var regionSlug = ConfigurationManager.AppSettings["Crawler:RootRegionSlug"];
            var region = new Region(regionSlug);

            await SetStorefrontSubRegionsAsync(region);

            return region;
        }

        public async Task<IEnumerable<Listing>> GetListingsOnLeafRegionsAsync(Region region)
        {
            var listings = new List<Listing>();

            if (region.SubRegions.Any())
            {
                foreach (var subRegion in region.SubRegions)
                {
                    listings.AddRange(await GetListingsOnLeafRegionsAsync(subRegion));
                }
            }
            else
            {
                var listingResponse = await _httpClient.GetListingsAsync(region.Slug);
                listings.AddRange(listingResponse.DataContainer.Dispensaries.Select(dispensary => new Listing { Slug = dispensary.Slug }));
            }

            return listings;
        }

        private async Task SetStorefrontSubRegionsAsync(Region region)
        {
            var subRegionResponse = await _httpClient.GetSubRegionsAsync(region.Slug);
            foreach (var subRegion in subRegionResponse.DataContainer.SubRegions)
            {
                var curRegion = new Region(subRegion.Slug);
                region.SubRegions.Add(curRegion);

                await SetStorefrontSubRegionsAsync(curRegion);
            }
        }
    }
}
