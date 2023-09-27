namespace JRAppAPI.Models
{
    public class SearchResponse
    {
        public string From { get; set; }
        public string To { get; set; }
        public List<Listing> Listings { get; set; }
    }
}
