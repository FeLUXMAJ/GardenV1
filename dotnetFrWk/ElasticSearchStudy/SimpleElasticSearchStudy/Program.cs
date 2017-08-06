using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Elasticsearch.Net;
using Nest;

namespace ElasticSearchStudy
{
    class Program
    {
        static void Main(string[] args)
        {
            var nodes = new Uri[]{
                new Uri("http://localhost:9200")
            };

            var pool = new StaticConnectionPool(nodes);
            var settings = new ConnectionSettings(pool);
            var client = new ElasticClient(settings);

            var tweet = new Tweet
            {
                Id = 2,
                User = "kimchy",
                PostDate = new DateTime(2009, 11, 15),
                Message = "Trying out NEST, so far so good?"
            };

            //var response = client.Index(tweet, idx => idx.Index("mytweetindex")); //or specify index via settings.DefaultIndex("mytweetindex");

            //response = client.IndexAsync(tweet, idx => idx.Index("mytweetindex")); // returns a Task<IndexResponse>

            var responsex = client.Get<Tweet>(1, idx => idx.Index("mytweetindex")); // returns an IGetResponse mapped 1-to-1 with the Elasticsearch JSON response

            Console.WriteLine(responsex);

            Console.ReadKey();
        }

    }
}
