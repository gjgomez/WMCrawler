using System.Collections.Generic;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.Infrastructure.Services;

namespace WmCrawler.Console
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }

        private static async Task MainAsync(string[] args)
        {
            var crawlerService = new CrawlerService();
            //var rootRegion = await crawlerService.GetStorefrontRegionsAsync();
            var rootRegion = new Region("united-states")
            {
                SubRegions = new List<Region> {
                    new Region("minnesota")
                    {
                        SubRegions =new List<Region>
                        {
                            new Region("st-cloud")
                        }
                    },
                    new Region("wisconsin"),
                    new Region("illinois")
                    {
                        SubRegions =new List<Region>
                        {
                            new Region("chicago")
                        }
                    }
                }
            };
            var listings = await crawlerService.GetListingsOnLeafRegionsAsync(rootRegion);
        }
    }
}
