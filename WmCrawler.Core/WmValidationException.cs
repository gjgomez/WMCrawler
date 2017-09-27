using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WmCrawler.WmHttpClient
{
    public  class WmValidationException : HttpRequestException
    {
        public IEnumerable<ValidationResult> Results { get; }

        public WmValidationException(IEnumerable<ValidationResult> results)
            : this(null, results) { }

        public WmValidationException(string message, IEnumerable<ValidationResult> results)
            : this(message, null, results) { }

        public WmValidationException(string message, Exception inner, IEnumerable<ValidationResult> results)
            : base(message, inner)
        {
            Results = results;
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            foreach (var result in Results)
            {
                foreach (var memberName in result.MemberNames)
                {
                    info.AddValue($"Results.{memberName}", result.ErrorMessage);
                }
            }
        }
    }
}
