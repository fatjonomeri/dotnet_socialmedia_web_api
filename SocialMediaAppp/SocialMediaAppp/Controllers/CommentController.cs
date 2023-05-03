using Microsoft.AspNetCore.Mvc;
using SocialMediaAppp.Interfaces;
using SocialMediaAppp.Models;
using SocialMediaAppp.Dto;
using AutoMapper;

namespace SocialMediaAppp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetComments()
        {
            var comments = _commentRepository.GetComments();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Comment))]
        [ProducesResponseType(400)]
        public IActionResult GetComment(int id)
        {
            if (!_commentRepository.CommentExists(id))
                return NotFound();

            var comment = _commentRepository.GetComment(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comment);
        }

        [HttpGet("users/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var comments = _mapper.Map<List<CommentDto>>(_commentRepository.GetCommentsByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);

        }

        [HttpGet("posts/{postId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Comment>))]
        [ProducesResponseType(400)]
        public IActionResult GetCommentsByPost(int postId)
        {
            if (!_postRepository.PostExists(postId))
                return NotFound();

            var comments = _mapper.Map<List<CommentDto>>(_commentRepository.GetCommentsByPost(postId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(comments);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateComment([FromQuery] int userId, [FromBody] CommentDto commentCreate)
        {
            if (commentCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentMap = _mapper.Map<Comment>(commentCreate);
            commentMap.User = _userRepository.GetUser(userId);
            commentMap.Created_at = DateTime.UtcNow;

            if (!_commentRepository.CreateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{commentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateComment(int commentId, [FromBody] CommentDto updatedComment)
        {
            if (updatedComment == null)
                return BadRequest(ModelState);

            if (commentId != updatedComment.Id)
                return BadRequest(ModelState);

            if (!_commentRepository.CommentExists(commentId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var commentMap = _mapper.Map<Comment>(updatedComment);

            if (!_commentRepository.UpdateComment(commentMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();

        }

        [HttpDelete("{commentId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteComment(int commentId)
        {
            if (!_commentRepository.CommentExists(commentId))
            {
                return NotFound();
            }
            var commentToDelete = _commentRepository.GetComment(commentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_commentRepository.DeleteComment(commentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return NoContent();
        }
    }
}
