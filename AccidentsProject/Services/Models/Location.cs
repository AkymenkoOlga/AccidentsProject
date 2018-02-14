namespace AccidentsProject.Services.Models
{
    public class Location
    {
        public string City { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public LatLon GeoCoordinates { get; set; }

        public string State { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string Zip { get; set; }
    }
}