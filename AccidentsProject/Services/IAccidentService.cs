using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentsProject.Services.Models;

namespace AccidentsProject.Services
{
    /// <summary>
    /// Provides read access to accidents.
    /// </summary>
    public interface IAccidentService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accidentId"></param>
        /// <returns></returns>
        Task<Accident> GetAccidentAsync(string accidentId);
        
        Task<IEnumerable<Accident>> GetAccidentsAsync();

        Task<IEnumerable<Accident>> GetAccidentsAsync(int skip, int take);
        
        Task<int> GetAccidentsCountAsync();
        
        Task<Location> GetLocationOfAccidentAsync(string accidentId);

        Task<Accident> CreateAccidentAsync(Accident accident);

        Task UpdateAccidentAsync(Accident accident);

        Task DeleteAccidentAsync(string accidentId);

        Task<IEnumerable<string>> GetTagsAsync();
    }
}