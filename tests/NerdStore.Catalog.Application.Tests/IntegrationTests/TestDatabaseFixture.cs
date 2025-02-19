using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data.Contexts;
using NerdStore.Catalog.Domain.Entities;

namespace NerdStore.Catalog.Application.Tests.IntegrationTests
{
    public class TestDatabaseFixture
    {
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=EFTestSample;Trusted_Connection=True;ConnectRetryCount=0";

        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public TestDatabaseFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        context.AddRange(
                            Product.CreateProduct("Product1", "Description1", true, 2, 3, Category.Create("Category1", 1), "product1.jpg", 1, 1, 1),
                            Product.CreateProduct("Product2", "Description2", false, 1, 5, Category.Create("Category2", 2), "product2.jpg", 1, 1, 1));
                        context.SaveChanges();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public CatalogContext CreateContext()
            => new CatalogContext(
                new DbContextOptionsBuilder<CatalogContext>()
                    .UseSqlServer(ConnectionString)
                    .Options);
    }
}
