namespace SocialMediaAppp.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Created_at { get; set; }
        public bool Is_suspended { get; set; }

    }
}