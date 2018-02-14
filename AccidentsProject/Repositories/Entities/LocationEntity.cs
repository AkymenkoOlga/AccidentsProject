
namespace AccidentsProject.Repositories.Entities
{
    public class LocationEntity : EntityBase
    {
        public string City { get; set; }

        public string CountryCode { get; set; }

        public string CountryName { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Street { get; set; }

        public string Street2 { get; set; }

        public string Zip { get; set; }

        internal static LocationEntity From(Services.Models.Location model)
            => new LocationEntity
            {
                City = model.City,
                CountryCode = model.CountryCode,
                CountryName = model.CountryName,
                Latitude = model.GeoCoordinates?.Latitude ?? 0d,
                Longitude = model.GeoCoordinates?.Longitude ?? 0d,
                State = model.State,
                Street = model.Street,
                Street2 = model.Street2,
                Zip = model.Zip
            };

        internal virtual Services.Models.Location ToModel()
            => new Services.Models.Location
            {
                City = this.City,
                CountryCode = this.CountryCode,
                CountryName = this.CountryName,
                GeoCoordinates = new Services.Models.LatLon(this.Latitude, this.Longitude),
                State = this.State,
                Street = this.Street,
                Street2 = this.Street2,
                Zip = this.Zip
            };
    }
}