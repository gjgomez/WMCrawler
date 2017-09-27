using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace WmCrawler.WmHttpClient.Requests
{
    internal abstract class Getable : IRequest
    {
        [JsonIgnore]
        public abstract string Endpoint { get; }

        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);

        [JsonIgnore]
        protected string QueryString
        {
            get
            {
                IEnumerable<KeyValuePair<string, string>> values = Getable.GetQueryString(this);

                IEnumerable<string> keyValues = values.Select<KeyValuePair<string, string>, string>(pair => string.Format("{0}={1}", pair.Key, pair.Value));

                return string.Join("&", keyValues);
            }
        }

        [JsonIgnore]
        internal abstract string PathAndQuery { get; }

        private static IEnumerable<KeyValuePair<string, string>> GetQueryString(object value, string startingName = null)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();

            IEnumerable<PropertyInfo> properties = value.GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(property =>
                    property.CanRead
                    && property.GetGetMethod(false) != null
                    && property.GetCustomAttribute<JsonIgnoreAttribute>(true) == null);

            foreach (PropertyInfo property in properties)
            {
                JsonPropertyAttribute attribute = property.GetCustomAttribute<JsonPropertyAttribute>(true);
                string name = attribute == null
                    ? property.Name
                    : attribute.PropertyName;

                name = string.Format("{0}{1}", startingName, name);

                if (value != null)
                {
                    object propertyValue = property.GetValue(value);

                    if (propertyValue == null)
                    {
                        values.Add(name, string.Empty);
                    }
                    else if (!propertyValue.GetType().IsByRef)
                    {
                        values.Add(name, propertyValue.ToString());
                    }
                    else
                    {
                        IEnumerable<KeyValuePair<string, string>> flattenedValues = Getable.GetQueryString(propertyValue, name + ".");

                        foreach (KeyValuePair<string, string> flattenedValue in flattenedValues)
                        {
                            values.Add(flattenedValue.Key, flattenedValue.Value);
                        }
                    }
                }
            }

            return values;
        }
    }
}
