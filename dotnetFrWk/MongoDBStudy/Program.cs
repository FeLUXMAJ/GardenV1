using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            test();
        }

        static void test()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("foo");
            var collection = database.GetCollection<BsonDocument>("test");

            //await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

            var list = collection.Find(new BsonDocument("name", "yuzhihui"))
                .ToList();

            foreach (var document in list)
            {
                Console.WriteLine(document["phone"]);
            }
            Console.ReadKey();
        }
    }

}
