using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccidentsProject.Repositories;
using AccidentsProject.Repositories.Entities;
using AccidentsProject.Services.Models;

namespace AccidentsProject.Services
{
    /// <summary>
    /// Default implementation for <see cref="IAccidentService"/>.
    /// </summary>
    public class AccidentService : IAccidentService
    {
        private readonly IAccidentRepository accidentRepository;

        public AccidentService(IAccidentRepository accidentRepository)
        {
            this.accidentRepository = accidentRepository;
        }

        public async Task<Accident> GetAccidentAsync(string accidentId)
        {
            AccidentEntity accidentEntity = await this.accidentRepository
                .RetrieveOneOrThrowArgumentExceptionAsync(accidentId)
                .ConfigureAwait(false);

            return accidentEntity.ToModel();
        }

        public async Task<IEnumerable<Accident>> GetAccidentsAsync()
        {
            IEnumerable<AccidentEntity> accidentEntities = await this.accidentRepository
                .RetrieveAllAsync()
                .ConfigureAwait(false);

            return accidentEntities.Select(accidentEntity => accidentEntity.ToModel());
        }

        public async Task<IEnumerable<Accident>> GetAccidentsAsync(int skip, int take)
        {
            IEnumerable<AccidentEntity> accidentEntities = (await this.accidentRepository
                        .RetrieveAllAsync()
                        .ConfigureAwait(false))
                    .OrderBy(c => c.Date)
                    .Skip(skip)
                    .Take(take);

            return accidentEntities.Select(accidentEntity => accidentEntity.ToModel());
        }

        public async Task<int> GetAccidentsCountAsync()
        {
            IEnumerable<AccidentEntity> accidentEntities = await this.accidentRepository
                .RetrieveAllAsync()
                .ConfigureAwait(false);

            return accidentEntities.Count();
        }

        public async Task<Location> GetLocationOfAccidentAsync(string accidentId)
        {
            AccidentEntity accidentEntity = await this.accidentRepository
                .RetrieveOneOrThrowArgumentExceptionAsync(accidentId)
                .ConfigureAwait(false);

            return accidentEntity.Location.ToModel();
        }

        public async Task<Accident> CreateAccidentAsync(Accident accident)
        {
            accident.Id = Guid.NewGuid().ToString();
            AccidentEntity accidentEntity = AccidentEntity.From(accident);

            await this.accidentRepository
                .InsertOneAsync(accidentEntity)
                .ConfigureAwait(false);

            return accident;
        }

        public async Task UpdateAccidentAsync(Accident accident)
        {
            await this.accidentRepository
                .UpdateOneOrThrowArgumentExceptionAsync(AccidentEntity.From(accident))
                .ConfigureAwait(false);
        }

        public async Task DeleteAccidentAsync(string accidentId)
        {
            await this.accidentRepository
                .DeleteOneAsync(accidentId)
                .ConfigureAwait(false);
        }

        public async Task<IEnumerable<string>> GetTagsAsync()
        {
            return (await this.accidentRepository.RetrieveAllAsync()                .ConfigureAwait(false))
                .Where(a => a.Tags != null)
                .SelectMany(a => a.Tags)
                .Distinct()
                .ToArray();
        }
    }
}