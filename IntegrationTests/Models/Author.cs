namespace IntegrationTests.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public  class Author
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid? Id { get; set; }

        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        [JsonProperty("firstName", NullValueHandling = NullValueHandling.Ignore)]
        public string FirstName { get; set; }

        [JsonProperty("lastName", NullValueHandling = NullValueHandling.Ignore)]
        public string LastName { get; set; }

        [JsonProperty("genre", NullValueHandling = NullValueHandling.Ignore)]
        public string Genre { get; set; }

        public static Author FromJson(string json) => JsonConvert.DeserializeObject<Author>(json, IntegrationTests.Models.Converter.Settings);

        public void CreateAuthorRequest(string firstName, string lastName, string genre)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Genre = genre;
        }

        public void CreateAuthorRespons(string firstName, string lastName, string genre)
        {
            this.Name = firstName + " " + lastName;
            this.Genre = genre;
        }
    }

       internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}

