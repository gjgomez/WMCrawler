using System.Configuration;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.Core.Services;
using WmCrawler.WmHttpClient;

namespace WmCrawler.Console.Services
{
    public class CrawlerService : ICrawlerService
    {
        private WmClient _httpClient;

        public CrawlerService()
        {
            _httpClient = new WmClient(ConfigurationManager.AppSettings["HttpClient:BaseEndpoint"]);
        }

        public async Task<Region> GetStorefrontRegionsAsync()
        {
            var regionSlug = ConfigurationManager.AppSettings["Crawler:RootRegionSlug"];
            var region = new Region(regionSlug);

            await GetStorefrontSubRegions(region);

            return region;
        }

        private async Task GetStorefrontSubRegions(Region region)
        {
            var subRegionResponse = await _httpClient.GetSubRegionsAsync(region.Slug);
            foreach (var subRegion in subRegionResponse.DataContainer.SubRegions)
            {
                var curRegion = new Region(subRegion.Slug);
                region.SubRegions.Add(curRegion);

                await GetStorefrontSubRegions(curRegion);
            }
        }
    }
}
