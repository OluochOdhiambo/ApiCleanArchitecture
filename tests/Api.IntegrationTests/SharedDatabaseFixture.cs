using System;
using Bogus;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Api.ApplicationCore.Entities;
using Api.ApplicationCore.Utils;
using Api.Infrastructure.Persistence.Contexts;
using Microsoft.Data.Sqlite;

namespace Api.IntegrationTests
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        private string dbName = "TestDatabase.db";

        public SharedDatabaseFixture()
        {
            Connection = new SqliteConnection($"Filename={dbName}");

            Seed();

            Connection.Open();
        }

        public DbConnection Connection { get; }

        public ApiContext CreateContext(DbTransaction? transaction = null)
        {
            var context = new ApiContext(new DbContextOptionsBuilder<ApiContext>().UseSqlite(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        SeedData(context);
                    }

                    _databaseInitialized = true;
                }
            }
        }

        private void SeedData(ApiContext context)
        {
            var bookIds = 1;
            var fakeBooks = new Faker<Book>()
                .RuleFor(b => b.Title, f => $"Book {bookIds}")
                .RuleFor(b => b.Summary, f => $"Summary {bookIds}")
                .RuleFor(b => b.Pages, f => f.Random.Number(1, 50))
                .RuleFor(b => b.InStock, f => true)
                .RuleFor(b => b.Price, f => f.Random.Double(0.01, 100))
                .RuleFor(o => o.CreatedAt, f => DateUtil.GetCurrentDate())
                .RuleFor(o => o.UpdatedAt, f => DateUtil.GetCurrentDate());

            var books = fakeBooks.Generate(10);

            context.AddRange(books);

            context.SaveChanges();
        }

        public void Dispose() => Connection.Dispose();
    }
}
