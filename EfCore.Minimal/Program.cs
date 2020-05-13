using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Linq;

namespace EfCore.Minimal
{
    class Program
    {
        static InMemoryDatabaseRoot root;
        private static MasterDbContext masterDb;
        private static ClientDbContext clientDb;

        static void Main(string[] args)
        {
            Setup();
            Document doc = GetDocument(clientDb);
            Debug.Assert(doc != null && doc.User != null);

            Setup();
            doc = GetDocument(clientDb);
            Debug.Assert(doc != null && doc.User != null);

            Console.WriteLine("Finished");
            Console.ReadLine();
        }

        public static void Setup()
        {
            root = new InMemoryDatabaseRoot();
            masterDb = CreateDbContextAndEnsure(root);
            clientDb = CreateClientDbContextAndEnsure(root);
            SeedMasterDb(masterDb);
            SeedClientDb(clientDb);

        }

        private static ClientDbContext CreateClientDbContext(InMemoryDatabaseRoot root)
        {
            return new ClientDbContext(new DbContextOptionsBuilder(new DbContextOptions<ClientDbContext>())
                .UseInMemoryDatabase("1", root)
                .Options);
        }

        private static ClientDbContext CreateClientDbContextAndEnsure(InMemoryDatabaseRoot root)
        {
            var db = new ClientDbContext(new DbContextOptionsBuilder(new DbContextOptions<ClientDbContext>())
                .UseInMemoryDatabase("1", root)
                .Options);

            return db;
        }

        private static MasterDbContext CreateDbContext(InMemoryDatabaseRoot root)
        {
            return new MasterDbContext(new DbContextOptionsBuilder(new DbContextOptions<MasterDbContext>())
                .UseInMemoryDatabase("1", root)
                .Options);
        }

        private static MasterDbContext CreateDbContextAndEnsure(InMemoryDatabaseRoot root)
        {
            var db = new MasterDbContext(new DbContextOptionsBuilder(new DbContextOptions<MasterDbContext>())
                .UseInMemoryDatabase("1", root)
                .Options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        private static Document GetDocument(ClientDbContext clientDb)
        {
            return clientDb.Documents
                .Include(x => x.User)
                .FirstOrDefault();
        }


        private static void SeedClientDb(ClientDbContext clientDb)
        {
            clientDb.Documents.Add(new Document()
            {
                UserId = 1,
                Path = "We/don_t/know/yet"
            });

            clientDb.SaveChanges();

        }

        private static void SeedMasterDb(MasterDbContext masterDb)
        {
            masterDb.Users.Add(new User()
            {
                Forename = "Aaron",
                Lastname = "Philips"
            });

            masterDb.SaveChanges();

        }
    }
}
