using System.ComponentModel.DataAnnotations;

namespace SocialMediaAppp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }
        public bool Edited { get; set; }
        public int Score { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Post? Post { get; set; }
        public int? PostId { get; set; }
    }
}