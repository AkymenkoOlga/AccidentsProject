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
            // use for debug (to pump dummy data)
            //client.DropDatabase(DatabaseName);
            //this.Reset().Wait();
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
                    Date = DateTime.Now.AddDays(-1).ToUniversalTime(),
                    Location = new LocationEntity() { Latitude = GetRandomNumber(47.0, 49.0), Longitude = GetRandomNumber(7.0, 10.0) },
                    Tags = tags.OrderBy(arg => Guid.NewGuid()).Take(2).ToList()
                });
                x++;
            } while (x < 100);

            x = 0;
            do
            {
                await this.InsertOneAsync(new AccidentEntity()
                {
                    Id = Guid.NewGuid().ToString(),
                    Date = DateTime.Now.ToUniversalTime(),
                    Location = new LocationEntity() { Latitude = GetRandomNumber(47.0, 49.0), Longitude = GetRandomNumber(7.0, 10.0) },
                    Tags = tags.OrderBy(arg => Guid.NewGuid()).Take(2).ToList()
                });
                x++;
            } while (x < 100);
        }

        private static double GetRandomNumber(double minimum, double maximum)
        {
            var random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
    }
}