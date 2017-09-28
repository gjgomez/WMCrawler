using System;
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
                listings.AddRange(await _httpClient.GetListingsAsync(region.Slug));
            }

            return listings;
        }

        public async Task<IEnumerable<MenuItem>> GetMenuItems(IEnumerable<Listing> listings)
        {
            var menuItems = new List<MenuItem>();
            var sno = 1;

            foreach (var listing in listings)
            {
                var items = await _httpClient.GetMenusAsync(listing);
                foreach (var item in items)
                {
                    item.FileFrom = DateTime.UtcNow.Month.ToString();
                    item.Sno = sno;

                    menuItems.Add(item);
                    sno++;
                }
            }

            return menuItems;
        }

        private async Task SetStorefrontSubRegionsAsync(Region region)
        {
            var subRegions = await _httpClient.GetSubRegionsAsync(region.Slug);
            foreach (var subRegion in subRegions)
            {
                var curRegion = new Region(subRegion.Slug);
                region.SubRegions.Add(curRegion);

                await SetStorefrontSubRegionsAsync(curRegion);
            }
        }
    }
}
