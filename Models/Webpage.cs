//Stina Hedman
//NET23

namespace MinimalAPI.Models
{
    public class Webpage
    {
        
        public int Id { get; set; }
        public string Url { get; set; }
        public Interest Interest { get; set; }
        public User User { get; set; }
    }
}
