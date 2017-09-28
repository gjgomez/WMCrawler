using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WmCrawler.Core.Models;

namespace WmCrawler.Core.Services
{
    public interface ICrawlerService
    {
        Task<Region> GetStorefrontRegionsAsync();

        Task<IEnumerable<Listing>> GetListingsOnLeafRegionsAsync(Region region);
    }
}
