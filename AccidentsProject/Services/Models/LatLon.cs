namespace AccidentsProject.Services.Models
{
    public class LatLon
    {
        public LatLon(double latitude, double longitude)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        public double Latitude { get; }

        public double Longitude { get; }
    }
}