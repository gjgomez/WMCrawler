using System.Collections.Generic;
using WmCrawler.Core.Models;

namespace WmCrawler.Core.Repositories
{
    public interface IWmRepository
    {
        void InsertMenuItems(IEnumerable<MenuItem> menuItems);
    }
}
