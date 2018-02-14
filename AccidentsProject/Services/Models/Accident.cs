using System;

namespace AccidentsProject.Services.Models
{
    public class Accident
    {
        public string Id { get; set; }
        
        public DateTime Date { get; set; }

        public Location Location { get; set; }

        public Severity Severity { get; set; }

        public string[] Tags { get; set; }
    }
}