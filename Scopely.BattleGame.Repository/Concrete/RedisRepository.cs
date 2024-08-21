using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Scopely.BattleGame.Redis;
using Scopely.BattleGame.Repositories.Interfaces;
using StackExchange.Redis;

namespace Scopely.BattleGame.Repositories
{
    public class RedisRepository<T> : IRedisRepository<T> where T : class
    {
        private readonly IDatabase _database;

        public RedisRepository(IOptions<RedisSettings>  redisSettings)
        {
            var connection = ConnectionMultiplexer.Connect(redisSettings.Value.ConnectionString);
            _database = connection.GetDatabase();
        }

        private async Task Set(string id, T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            await _database.StringSetAsync(id, json);
        }

        public async Task Add(string id, T entity)
        {
            await Set(id, entity);
        }
        public async Task Update(string id, T entity)
        {
            await Set(id, entity);
        }

        public async Task<T?> GetById(string id)
        {
            var redisValue = await _database.StringGetAsync(id);
            var value = redisValue.IsNull ? null : JsonConvert.DeserializeObject<T>(redisValue);
            return value;
        }

        public async Task Remove(string id)
        {
            await _database.KeyDeleteAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(string setName)
        {
            var redisValues = await _database.SetMembersAsync(setName);
            var values = Enumerable.Empty<T>();

            if (redisValues is not null)
            {
               values = redisValues.Select(rv => JsonConvert.DeserializeObject<T>(rv));
            }

            return values;
        }

        public async Task AddToSet(string setName, T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            await _database.SetAddAsync(setName, json);
        }

        public async Task<IEnumerable<T>> GetAllFromSortedSet(string setName)
        {
            var sortedValues = await _database.SortedSetRangeByRankWithScoresAsync(setName);
            var values = new List<T>();

            foreach (var value in sortedValues)
            {
                if (value.Element.IsNull) 
                {
                    continue;
                }

                var entity = JsonConvert.DeserializeObject<T>(value.Element);

                if (entity is not null) 
                {
                    values.Add(entity);
                }
            }

            return values;

        }

        public Task AddToSortedSet(string setName, string playerId, long score)
        {
            throw new NotImplementedException();
        }
    }
}
