using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace WebApiMongoDbSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureServices((hostContext, services) =>
                {
                    var configuration = hostContext.Configuration;
                    var mongoDbSettings = configuration.GetSection("MongoDb");
                    var mongoDbEndpoint = mongoDbSettings["Endpoint"];
                    var mongoDbDatabaseName = mongoDbSettings["DatabaseName"];
                    var mongoDbCollectionName = mongoDbSettings["CollectionName"];

                    services.AddSingleton<IMongoClient>(new MongoClient(mongoDbEndpoint));
                    services.AddSingleton<IMongoDatabase>(serviceProvider =>
                    {
                        var client = serviceProvider.GetRequiredService<IMongoClient>();
                        var database = client.GetDatabase(mongoDbDatabaseName);
                        
                        var collectionNames = database.ListCollectionNames().ToList();
                        if (!collectionNames.Contains(mongoDbCollectionName))
                        {
                            database.CreateCollection(mongoDbCollectionName);
                            Console.WriteLine($"Collection '{mongoDbCollectionName}' has been created.");
                        }
                        else
                        {
                            Console.WriteLine($"Collection '{mongoDbCollectionName}' already exists.");
                        }

                        return database;
                    });
                });
    }
}
