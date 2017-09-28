using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using WmCrawler.WmHttpClient.Requests;
using WmCrawler.WmHttpClient.Requests.Listings;
using WmCrawler.WmHttpClient.Requests.SubRegions;
using WmCrawler.WmHttpClient.Responses.Listings;
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

        public Task<SubRegionResponse> GetSubRegionsAsync(string regionSlug)
        {
            var request = new SubRegionRequest { RegionSlug = regionSlug };

            return GetAsync<SubRegionRequest, SubRegionResponse>(request);            
        }

        public Task<ListingResponse> GetListingsAsync(string regionSlug)
        {
            var request = new ListingRequest { RegionSlug = regionSlug };

            return GetAsync<ListingRequest, ListingResponse>(request);
        }
    }
}
