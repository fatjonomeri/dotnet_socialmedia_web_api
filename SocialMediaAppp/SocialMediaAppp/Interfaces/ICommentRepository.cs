using SocialMediaAppp.Models;

namespace SocialMediaAppp.Interfaces
{
    public interface ICommentRepository
    {
        ICollection<Comment> GetComments();
        Comment GetComment(int id);
        ICollection<Comment> GetCommentsByUser(int userId);
        ICollection<Comment> GetCommentsByPost(int postId);
        bool CommentExists(int id);
        bool CreateComment(Comment comment);
        bool UpdateComment(Comment comment);
        bool DeleteComment(Comment comment);
        bool Save();
    }
}