using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiMongoDbSample.Infrastructure;
using WebApiMongoDbSample.Models;

namespace WebApiMongoDbSample.Services
{
    public class NewsService : INewsService
    {
        private readonly IMongoCollection<News> _news;
        private readonly MongoDbSettings _mongoDbSettings;

        public NewsService(IOptions<MongoDbSettings> options)
        {
            _mongoDbSettings = options.Value;
            var client = new MongoClient(_mongoDbSettings.Endpoint);
            var database = client.GetDatabase(_mongoDbSettings.DatabaseName);
            _news = database.GetCollection<News>(_mongoDbSettings.CollectionName);
        }

        public async Task<News> AddNewsAsync(string body, string photoUrl)
        {
            var news = new News
            {
                Body = body,
                PhotoUrl = photoUrl
            };

            await _news.InsertOneAsync(news);
            return news;
        }

        public async Task<List<News>> GetAllNewsAsync()
        {
            var news = await _news.FindAsync(news => true);
            return await news.ToListAsync();
        }

        public async Task<News> GetNewsByIdAsync(string id)
        {
            var newsCursor = await _news.FindAsync<News>(n => n.Id == id);
            var news = await newsCursor.FirstOrDefaultAsync();
            if (news != null)
                return news;
            return null;
        }
    }
}
