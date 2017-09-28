using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using WmCrawler.Core.Models;
using WmCrawler.Core.Repositories;

namespace WmCrawler.Infrastructure.Repositories.Dapper
{
    public class WmRepository : IWmRepository
    {
        public void InsertMenuItems(IEnumerable<MenuItem> menuItems)
        {
            var db = new SqlConnection(ConfigurationManager.ConnectionStrings["WmCrawlerDb"].ConnectionString);

            const string sql = @"INSERT INTO Scrape_Data (
                    [FileFrom]
                    ,[Sno]
                    ,[Name]
                    ,[Phone]
                    ,[Address]
                    ,[City]
                    ,[State]
                    ,[Zip]
                    ,[Website]
                    ,[Email]
                    ,[Menu_Updated]
                    ,[Joined]
                    ,[Medical_Dispensary]
                    ,[Recreational_Store]
                    ,[Delivery]
                    ,[Category]
                    ,[Subcategory]
                    ,[Type]
                    ,[Brand]
                    ,[Product_Name]
                    ,[Description]
                    ,[Size]
                    ,[Pricing]
                    ,[Source]) 
                VALUES(
                    @FileFrom
                    ,@Sno
                    ,@Name
                    ,@Phone
                    ,@Address
                    ,@City
                    ,@State
                    ,@ZipCode
                    ,@DispensaryWebsite
                    ,@Email
                    ,@DateMenuLastUpdated
                    ,@DateJoined
                    ,@IsDispensary
                    ,@IsRecreational
                    ,@CanDeliver
                    ,@Category
                    ,@Subcategory
                    ,@Type
                    ,@Brand
                    ,@ProductName
                    ,@ProductDescription
                    ,@ProductSize
                    ,@ProductCost
                    ,@Source);";

            db.Execute(sql, menuItems);
        }
    }
}
