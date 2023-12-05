using MongoDB.Bson;
using MongoDB.Driver;
using NeoSOFT.Infrastructure.Context;
using NeoSOFT.Infrastructure.Contract;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NeoSOFT.Infrastructure.Repository
{
    public class Repository<T>:IRepository<T> where T:class
    {
        private readonly IMongoCollection<T> _collectionName;

        public Repository(IDbContext DbContextSettings, IMongoClient mongoClient)
        {
            var dbName=mongoClient.GetDatabase(DbContextSettings.DatabaseName);
            _collectionName=dbName.GetCollection<T>(DbContextSettings.CollectionName);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _collectionName.Find(_ => true).ToListAsync();
        }

        public async Task<T> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            return await _collectionName.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _collectionName.InsertOneAsync(entity);
            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _collectionName.ReplaceOneAsync(filter, entity);
            return entity;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var result= await _collectionName.DeleteOneAsync(filter);
            if (result.IsAcknowledged==true) 
                return true;
            else 
                return false;
        }
        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            var filter = Builders<T>.Filter.Where(expression);
            return await _collectionName.Find(filter).ToListAsync();
        }
      
    }
}
