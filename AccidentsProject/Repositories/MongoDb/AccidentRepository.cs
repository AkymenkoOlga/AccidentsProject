using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccidentsProject.Repositories.Entities;
using AccidentsProject.Repositories.MongoDb;
using MongoDB.Driver;

namespace AccidentsProject.Repositories
{
    public class AccidentRepository : AbstractMongoDbRepository<AccidentEntity>, IAccidentRepository
    {
        private const string DatabaseName = "accidents";

        private const string CollectionName = "accidents";

        public AccidentRepository(IMongoClient client)
            : base(client, DatabaseName, CollectionName)
        {
        }
    }
}