using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using WmCrawler.Core.Models;
using WmCrawler.WmHttpClient.Requests;
using WmCrawler.WmHttpClient.Requests.SubRegion;
using WmCrawler.WmHttpClient.Responses.SubRegion;

namespace WmCrawler.WmHttpClient
{
    public sealed class WmClient
    {
        private HttpClient _client;

        public WmClient(string baseAddress)
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri(baseAddress);
        }

        private async Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request) where TRequest : Getable
        {
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(request, new ValidationContext(request), results, false))
            {
                throw new WmValidationException("Request is invalid.  Please check the required fields and try again.", results);
            }

            var requestUri = _client.BaseAddress.PathAndQuery + request.PathAndQuery;

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
            var request = new SubRegionRequest { Slug = regionSlug };

            return GetAsync<SubRegionRequest, SubRegionResponse>(request);            
        }
    }
}
