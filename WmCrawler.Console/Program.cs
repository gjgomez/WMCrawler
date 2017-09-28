using System.Collections.Generic;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.Infrastructure.Repositories.Dapper;
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
            // get menu records
            var crawlerService = new CrawlerService();
            var rootRegion = await crawlerService.GetStorefrontRegionsAsync();
            var listings = await crawlerService.GetListingsOnLeafRegionsAsync(rootRegion);
            var menuItems = await crawlerService.GetMenuItems(listings);

            // save records to database
            var repository = new WmRepository();
            repository.InsertMenuItems(menuItems);
        }
    }
}
