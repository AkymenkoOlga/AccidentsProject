using AccidentsProject.Services.Models;
using Newtonsoft.Json;

namespace AccidentsProject.Controllers.Dtos
{
    public class LatLonDto
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lon")]
        public double Longitude { get; set; }

        public static LatLonDto From(LatLon latLon)
        {
            return new LatLonDto
            {
                Latitude = latLon.Latitude,
                Longitude = latLon.Longitude
            };
        }
    }
}