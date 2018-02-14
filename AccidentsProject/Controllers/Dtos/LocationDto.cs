using AccidentsProject.Services.Models;
using Newtonsoft.Json;

namespace AccidentsProject.Controllers.Dtos
{
    public class LocationDto
    {
        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("geo_coordinates")]
        public LatLonDto GeoCoordinates { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("street2")]
        public string Street2 { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        internal static LocationDto From(Location model)
            => new LocationDto
            {
                City = model.City,
                GeoCoordinates = model.GeoCoordinates != null ? LatLonDto.From(model.GeoCoordinates) : null,
                CountryCode = model.CountryCode,
                State = model.State,
                Street = model.Street,
                Street2 = model.Street2,
                Zip = model.Zip
            };

        internal virtual Location ToModel()
            => new Location
            {
                City = this.City,
                CountryCode = this.CountryCode,
                State = this.State,
                Street = this.Street,
                Street2 = this.Street2,
                Zip = this.Zip,
                GeoCoordinates = this.GeoCoordinates != null 
                    ? new LatLon(this.GeoCoordinates.Latitude, this.GeoCoordinates.Longitude) 
                    : null,
            };
    }
}