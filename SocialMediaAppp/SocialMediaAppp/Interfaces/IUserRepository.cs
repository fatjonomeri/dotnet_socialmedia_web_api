using SocialMediaAppp.Models;

namespace SocialMediaAppp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        bool UserExists(int id);
        bool CreateUser(User user);
        bool DeleteUser(User user);
        bool Save();
    }
}