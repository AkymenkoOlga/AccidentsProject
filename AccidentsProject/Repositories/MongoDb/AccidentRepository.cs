using System;
using System.Linq;
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
            // TODO: remove once there is a real data in MongoDB
            client.DropDatabase(DatabaseName);
            this.Reset().Wait();
        }

        private async Task Reset()
        {
            var tags = new[] { "day", "night", "fatal", "bicycle", "motorcycle", "automobile" };

            var x = 0;
            do
            {
                await this.InsertOneAsync(new AccidentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now.ToUniversalTime(),
                    Location = new LocationEntity() { Latitude = GetRandomNumber(49.0, 49.5), Longitude = GetRandomNumber(9.0, 9.5) },
                    Tags = tags.OrderBy(arg => Guid.NewGuid()).Take(2).ToList()
                });
                x++;
            } while (x < 25);
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}