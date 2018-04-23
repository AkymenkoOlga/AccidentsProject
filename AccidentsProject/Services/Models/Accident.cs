using System;

namespace AccidentsProject.Services.Models
{
    public class Accident
    {
        public string Id { get; set; }

        public string ExternalId { get; set; }
        
        public DateTime Date { get; set; }

        public Location Location { get; set; }

        public string[] Tags { get; set; }
    }
}