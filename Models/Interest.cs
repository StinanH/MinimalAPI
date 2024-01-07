//Stina Hedman
//NET23

namespace MinimalAPI.Models
{
    public class Interest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; } = new List<User>();
    }
}
