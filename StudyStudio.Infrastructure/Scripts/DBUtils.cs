using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Database.Data;
using Raven.Database.Indexing;
using StudyStudio.Domain;
using StudyStudio.Infrastructure.Queries;

namespace StudyStudio.Infrastructure.Scripts
{
    public static class DBUtils
    {
        public const string ExercisesByBodyIndexName = "ExercisesByBody";
        public const string ExercisesByAssignmentIndexName = "Exercises/ByAssignment";
        
        public static IDocumentStore SetupFreshStudyStudioDB()
        {
            var db = new DocumentStore { ConnectionStringName = "RavenDBServer" };
            db.Initialize();
            CreateIndexes(db);

            return db;
        }

        public static void CreateIndexes(DocumentStore db)
        {
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), db);
            db.DatabaseCommands.DeleteByIndex("Raven/DocumentsByEntityName", new IndexQuery(), allowStale: false);
            var indexNames = db.DatabaseCommands.GetIndexNames(0, int.MaxValue);

            if (indexNames.Contains(ExercisesByBodyIndexName))
            {
                db.DatabaseCommands.DeleteIndex(ExercisesByBodyIndexName);
            }

            db.DatabaseCommands.PutIndex(ExercisesByBodyIndexName,
                                         new IndexDefinition
                                             {
                                                 Map = @"from exercise in docs.Exercises 
                                                         select new {Body = exercise.Body}",
                                                 Stores = { { "Body", FieldStorage.Yes } }
                                             });

            if (indexNames.Contains(ExercisesByAssignmentIndexName))
            {
                db.DatabaseCommands.DeleteIndex(ExercisesByAssignmentIndexName);
            }

            db.DatabaseCommands.PutIndex(ExercisesByAssignmentIndexName,
                                         new IndexDefinition
                                             {
                                                 Map =
                                                     @"from e in docs.Exercises 
                                                       from aid in e.Assignments 
                                                       select new {Assignment = aid}",
                                             });
        }

        public static IDocumentStore FillWithDemoData(this IDocumentStore db)
        {
            using (var session = db.OpenSession())
            {
                var exercises = new[]
                                    {
                                        new Exercise {Body = "demo exercise 1"}, new Exercise {Body = "demo exercise 2"}
                                        ,
                                        new Exercise {Body = "demo exercise 3"}, new Exercise {Body = "demo exercise 4"}
                                        ,
                                        new Exercise {Body = "demo exercise 5"}
                                    };

                session.StoreCollection(exercises);
                session.SaveChanges();
            }

            return db;
        }
    }
}

namespace Raven.Client.Document
{
    public static class RavendExtensions
    {
        public static void StoreCollection<T>(this IDocumentSession session, IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                session.Store(entity);
            }
        }    
    }
}
