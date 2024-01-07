//Stina Hedman
//NET23

namespace MinimalAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<Interest> Interests { get; } = new List<Interest>();
        public ICollection<Webpage> Webpages { get; } = new List<Webpage>();
    }
}
