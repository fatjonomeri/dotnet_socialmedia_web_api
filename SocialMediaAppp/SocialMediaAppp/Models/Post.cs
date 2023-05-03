using SocialMediaAppp.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialMediaAppp.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public int Num_comments { get; set; }
        public DateTime Created_at { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}