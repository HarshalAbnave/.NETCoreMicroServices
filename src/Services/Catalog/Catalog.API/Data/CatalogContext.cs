using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private IConfiguration _config;

        public CatalogContext(IConfiguration configuration)
        {
            _config = configuration;
            var client = new MongoClient(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(_config.GetValue<string>("DatabaseSettings:DatabaseName"));

            Products = database.GetCollection<Product>(_config.GetValue<string>("DatabaseSettings:CollectionName"));
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
