using SocialMediaAppp.Data;
using SocialMediaAppp.Models;
using SocialMediaAppp.Interfaces;

namespace SocialMediaAppp.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;

        public PostRepository(DataContext context)
        {
            _context = context;
        }

        public bool PostExists(int id)
        {
            return _context.Posts.Any(p => p.Id == id);
        }

        public ICollection<Post> GetPosts()
        {
            return _context.Posts.OrderBy(p => p.Id).ToList();
        }

        public Post GetPost(int id)
        {
            return _context.Posts.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Post> GetPostsByUser(int userId)
        {
            return _context.Posts.Where(e => e.UserId == userId).ToList();
        }

        public bool CreatePost(Post post)
        {
            _context.Add(post);
            return Save();
        }

        public bool UpdatePost(Post post)
        {
            _context.Update(post);
            return Save();
        }

        public bool DeletePost(Post post)
        {
            _context.Remove(post);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}