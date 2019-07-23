using System;

namespace WebAdvert.SearchWorker.Models
{
    public class AdvertDocument
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime CreationDateTime { get; set; }
    }
}
