using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using SocialMediaAppp.Data;
using SocialMediaAppp.Models;
using SocialMediaAppp.Interfaces;

namespace SocialMediaAppp.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;

        public CommentRepository(DataContext context)
        {
            _context = context;
        }

        public bool CommentExists(int id)
        {
            return _context.Comments.Any(c => c.Id == id);
        }

        public ICollection<Comment> GetComments()
        {
            return _context.Comments.OrderBy(c => c.Id).ToList();
        }

        public Comment GetComment(int id)
        {
            return _context.Comments.Where(e => e.Id == id).FirstOrDefault();
        }
        
        public ICollection<Comment> GetCommentsByUser(int userId)
        {
            return _context.Comments.Where(e => e.UserId == userId).ToList();
        }

        public ICollection<Comment> GetCommentsByPost(int postId)
        {
            return _context.Comments.Where(e => e.PostId == postId).ToList();
        }

        public bool CreateComment(Comment comment)
        {
            _context.Add(comment);
            return Save();
        }

        public bool UpdateComment(Comment comment)
        {
            _context.Update(comment);
            return Save();
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Remove(comment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

    }
}