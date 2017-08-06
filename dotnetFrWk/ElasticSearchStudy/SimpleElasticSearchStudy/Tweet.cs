using System;

namespace ElasticSearchStudy
{
    internal class Tweet
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime PostDate { get; set; }
        public string User { get; set; }
    }
}