using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Raven.Client;
using Raven.Client.Document;
using Raven.Database;
using Raven.Database.Indexing;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Queries;
using StudyStudio.Infrastructure.Scripts;

namespace StudyStudio.Infrastructure.Tests
{
    public class RavenTestFixture : IDisposable
    {
        private string _dbPath;

        protected DocumentStore GetConfiguredDB()
        {
            var db = GetEmbeddedDB();
            DBUtils.CreateIndexes(db);
            return db;
        }

        private DocumentStore GetEmbeddedDB()
        {
            _dbPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            _dbPath = Path.Combine(_dbPath, "TestDb").Substring(6);

            DeleteTestDB();

            var db = new DocumentStore
            {
                Configuration =
                {
                    DataDirectory = _dbPath,
                    RunInUnreliableYetFastModeThatIsNotSuitableForProduction = true
                }
            };

            db.Initialize();

            return db;
        }

        protected void WaitForNonStaleResults(DocumentStore db)
        {
            while (db.DocumentDatabase.Statistics.StaleIndexes.Length > 0)
            {
                Thread.Sleep(100);
            }
        }

        public void Dispose()
        {
            DeleteTestDB();
        }

        private void DeleteTestDB()
        {
            if (Directory.Exists(_dbPath))
            {
                Directory.Delete(_dbPath, true);                
            }
        }
    }
}