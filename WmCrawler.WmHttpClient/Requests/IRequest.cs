using System.ComponentModel.DataAnnotations;

namespace WmCrawler.WmHttpClient.Requests
{
    internal interface IRequest : IValidatableObject
    {
        string Endpoint { get; }
    }
}
