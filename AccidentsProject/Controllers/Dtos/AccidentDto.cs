using System;
using AccidentsProject.Services.Models;
using Newtonsoft.Json;

namespace AccidentsProject.Controllers.Dtos
{
    public class AccidentDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("location")]
        public LocationDto Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        public Accident ToModel()
        {
            return new Accident()
            {
                Id = this.Id,
                Date = this.Date,
                Location = this.Location?.ToModel(),
                Tags = this.Tags
            };
        }

        internal static AccidentDto From(Accident accident)
            => new AccidentDto
            {
                Id = accident.Id,
                Date = accident.Date,
                Location = accident.Location != null ? LocationDto.From(accident.Location) : null,
                Tags = accident.Tags
            };
    }
}