using System;
using System.Collections.Generic;
using System.Linq;
using AccidentsProject.Services.Models;

namespace AccidentsProject.Repositories.Entities
{
    public class AccidentEntity : EntityBase
    {
        public DateTime Date { get; set; }

        public LocationEntity Location { get; set; }

        public string Severity { get; set; }

        public IEnumerable<string> Tags { get; set; }

        internal static AccidentEntity From(Accident model)
            => new AccidentEntity
            {
                Id = model.Id,
                Date = model.Date,
                Location = LocationEntity.From(model.Location),
                Severity = model.Severity.ToString(),
                Tags = model.Tags?.ToArray() ?? new string[0]
            };

        internal virtual Accident ToModel()
        {
            if (!Enum.TryParse(this.Severity, true, out Severity severity))
            {
                severity = Services.Models.Severity.Unknown;
            }

            return new Accident
            {
                Id = this.Id,
                Date = this.Date,
                Location = this.Location.ToModel(),
                Severity = severity,
                Tags = this.Tags?.ToArray() ?? new string[0]
            };
        }
    }
}