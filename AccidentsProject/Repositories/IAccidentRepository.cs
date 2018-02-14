using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AccidentsProject.Repositories.Entities;

namespace AccidentsProject.Repositories
{
    /// <summary>
    /// The accident repository provides access to <see cref="AccidentEntity"/>.
    /// </summary>
    public interface IAccidentRepository : IRepository<AccidentEntity>
    {
    }
}