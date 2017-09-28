using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.Core.Utilities;
using WmCrawler.WmHttpClient.Requests;
using WmCrawler.WmHttpClient.Requests.Listings;
using WmCrawler.WmHttpClient.Requests.Menus;
using WmCrawler.WmHttpClient.Requests.SubRegions;
using WmCrawler.WmHttpClient.Responses.Listings;
using WmCrawler.WmHttpClient.Responses.Menus;
using WmCrawler.WmHttpClient.Responses.SubRegion;

namespace WmCrawler.WmHttpClient
{
    public sealed class WmClient
    {
        private readonly HttpClient _client;

        public WmClient()
        {
            _client = new HttpClient();
        }

        private async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request) where TRequest : Getable
        {
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request, new ValidationContext(request), results, false))
            {
                throw new WmValidationException("Request is invalid.  Please check the required fields and try again.", results);
            }

            var requestUri = request.Endpoint;

            using (var responseTask = _client.GetAsync(requestUri))
            {
                var response = responseTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<TResponse>(json);
                }
                else
                {
                    throw new Exception($"Error calling {requestUri}  Error Code: {response.StatusCode}  Error Message: {response.ReasonPhrase}");
                }
            }
        }

        public async Task<IEnumerable<Region>> GetSubRegionsAsync(string regionSlug)
        {
            var request = new SubRegionRequest { RegionSlug = regionSlug };
            var response = await GetAsync<SubRegionRequest, SubRegionResponse>(request);

            return response.DataContainer.SubRegions.Select(subRegion => new Region(subRegion.Slug)).ToList();
        }

        public async Task<IEnumerable<Listing>> GetListingsAsync(string regionSlug)
        {
            var request = new ListingRequest { RegionSlug = regionSlug };
            var response = await GetAsync<ListingRequest, ListingResponse>(request);

            return response.DataContainer.Listings.Select(dispensary => new Listing(dispensary.Slug, dispensary.Type));
        }

        public async Task<IEnumerable<MenuItem>> GetMenusAsync(Listing listing)
        {
            var request = new MenuRequest { ListingSlug = listing.Slug, ListingType = listing.Type };
            var response = await GetAsync<MenuRequest, MenuResponse>(request);
            var menuItems = new List<MenuItem>();

            foreach (var category in response.Categories)
            {
                foreach (var product in category.Products)
                {
                    var recordBase = new MenuItem
                    {
                        FileFrom = DateTime.UtcNow.Month.ToString(),
                        Name = response.ListingContainer.Name,
                        Phone = response.ListingContainer.Phone,
                        Address = response.ListingContainer.Address,
                        City = response.ListingContainer.City,
                        State = response.ListingContainer.State,
                        ZipCode = response.ListingContainer.ZipCode,
                        DispensaryWebsite = response.ListingContainer.DispensaryWebsite,
                        Email = response.ListingContainer.Email,
                        DateMenuLastUpdated = response.DateMenuLastUpdated,
                        DateJoined = null,
                        IsDispensary = response.ListingContainer.IsDispensaryString,
                        IsRecreational = response.ListingContainer.IsRecreationalString,
                        CanDeliver = response.ListingContainer.CanDeliveryString,
                        Category = string.Empty,
                        SubCategory = category.Name,
                        Type = string.Empty,
                        Brand = string.Empty,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        Source = response.ListingContainer.ListingURL
                    };

                    if (!product.PriceData.HasPrice)
                    {
                        menuItems.Add(recordBase);
                    }
                    else
                    {
                        if (product.PriceData.OneGram.HasValue && product.PriceData.OneGram.Value > 0)
                        {
                            var oneGramRecord = recordBase.CloneJson();
                            oneGramRecord.ProductSize = "One Gram";
                            oneGramRecord.ProductCost = product.PriceData.OneGram.Value;

                            menuItems.Add(oneGramRecord);
                        }

                        if (product.PriceData.TwoGrams.HasValue && product.PriceData.TwoGrams.Value > 0)
                        {
                            var twoGramsRecord = recordBase.CloneJson();
                            twoGramsRecord.ProductSize = "Two Grams";
                            twoGramsRecord.ProductCost = product.PriceData.TwoGrams.Value;

                            menuItems.Add(twoGramsRecord);
                        }

                        if (product.PriceData.OneEightOnce.HasValue && product.PriceData.OneEightOnce.Value > 0)
                        {
                            var oneEightOunceRecord = recordBase.CloneJson();
                            oneEightOunceRecord.ProductSize = "One Eight Once";
                            oneEightOunceRecord.ProductCost = product.PriceData.OneEightOnce.Value;

                            menuItems.Add(oneEightOunceRecord);
                        }

                        if (product.PriceData.OneQuarterOnce.HasValue && product.PriceData.OneQuarterOnce.Value > 0)
                        {
                            var oneQuarterOnceRecord = recordBase.CloneJson();
                            oneQuarterOnceRecord.ProductSize = "One Quarter Once";
                            oneQuarterOnceRecord.ProductCost = product.PriceData.OneQuarterOnce.Value;

                            menuItems.Add(oneQuarterOnceRecord);
                        }

                        if (product.PriceData.OneHalfOnce.HasValue && product.PriceData.OneHalfOnce.Value > 0)
                        {
                            var oneHalfOnceRecord = recordBase.CloneJson();
                            oneHalfOnceRecord.ProductSize = "One Half Once";
                            oneHalfOnceRecord.ProductCost = product.PriceData.OneHalfOnce.Value;

                            menuItems.Add(oneHalfOnceRecord);
                        }


                        if (product.PriceData.OneOunce.HasValue && product.PriceData.OneOunce.Value > 0)
                        {
                            var oneOunceRecord = recordBase.CloneJson();
                            oneOunceRecord.ProductSize = "One Ounce";
                            oneOunceRecord.ProductCost = product.PriceData.OneOunce.Value;

                            menuItems.Add(oneOunceRecord);
                        }
                    }
                }
            }

            return menuItems;
        }
    }
}
