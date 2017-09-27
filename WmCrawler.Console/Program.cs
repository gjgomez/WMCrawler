using System.Threading.Tasks;
using WmCrawler.Console.Services;

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
            var rootRegion = await crawlerService.GetStorefrontRegionsAsync();
        }
    }
}
