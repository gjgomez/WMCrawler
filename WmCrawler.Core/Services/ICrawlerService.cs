using System.Collections.Generic;
using System.Threading.Tasks;
using WmCrawler.Core.Models;

namespace WmCrawler.Core.Services
{
    public interface ICrawlerService
    {
        Task<Region> GetStorefrontRegionsAsync();

        Task<IEnumerable<Listing>> GetListingsOnLeafRegionsAsync(Region region);

        Task<IEnumerable<MenuItem>> GetMenuItems(IEnumerable<Listing> listings);
    }
}
