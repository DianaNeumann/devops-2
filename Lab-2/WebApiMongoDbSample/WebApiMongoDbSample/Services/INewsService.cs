using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiMongoDbSample.Models;

namespace WebApiMongoDbSample.Services
{
    public interface INewsService
    {
        Task<News> AddNewsAsync(string body, string photoUrl);
        Task<List<News>> GetAllNewsAsync();
        Task<News> GetNewsByIdAsync(string id);
    }
}
