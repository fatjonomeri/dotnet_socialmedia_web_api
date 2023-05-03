
namespace SocialMediaAppp.Dto
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Body { get; set; }
        public DateTime Created_at { get; set; }
        public bool Edited { get; set; }
        public int Score { get; set; }

    }
}