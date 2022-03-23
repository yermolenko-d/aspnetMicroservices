using DLL.Data.Interfaces;
using DLL.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;

namespace DLL.Data
{
    public class CatalogContext : ICatalogContext
    {
        public IMongoCollection<Product> Products { get; }
        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var db = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));
            Products = db.GetCollection<Product>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            
            CatalogContextSeed.SeedData(Products);
        }
    }
}
