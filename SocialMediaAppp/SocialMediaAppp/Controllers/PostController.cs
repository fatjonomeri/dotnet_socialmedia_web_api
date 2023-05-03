using Microsoft.AspNetCore.Mvc;
using SocialMediaAppp.Interfaces;
using SocialMediaAppp.Models;
using SocialMediaAppp.Dto;
using AutoMapper;

namespace SocialMediaAppp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public PostController(ICommentRepository commentRepository, IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        [ProducesResponseType(400)]
        public IActionResult GetPosts()
        {
            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPosts());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Post))]
        [ProducesResponseType(400)]
        public IActionResult GetPost(int id)
        {
            if (!_postRepository.PostExists(id))
                return NotFound();

            var post = _mapper.Map<PostDto>(_postRepository.GetPost(id));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(post);
        }

        [HttpGet("user/{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Post>))]
        [ProducesResponseType(400)]
        public IActionResult GetPostsByUser(int userId)
        {
            if (!_userRepository.UserExists(userId))
                return NotFound();

            var posts = _mapper.Map<List<PostDto>>(_postRepository.GetPostsByUser(userId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(posts);

        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePost([FromQuery] int userId, [FromBody] PostDto postCreate)
        {
            if (postCreate == null)
                return BadRequest(ModelState);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMap = _mapper.Map<Post>(postCreate);
            postMap.User = _userRepository.GetUser(userId);
            postMap.Created_at = DateTime.UtcNow;

            if (!_postRepository.CreatePost(postMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");

        }

        [HttpPut("{postId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdatePost(int postId, [FromBody] PostDto updatedPost)
        {
            if (updatedPost == null)
                return BadRequest(ModelState);

            if (postId != updatedPost.Id)
                return BadRequest(ModelState);

            if (!_postRepository.PostExists(postId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var postMap = _mapper.Map<Post>(updatedPost);

            if (!_postRepository.UpdatePost(postMap))
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{postId")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeletePost(int postId)
        {
            if (!_postRepository.PostExists(postId))
            {
                return NotFound();
            }
            var postToDelete = _postRepository.GetPost(postId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_postRepository.DeletePost(postToDelete))
            {
                ModelState.AddModelError("", "Something went wrong");
            }

            return NoContent();
        }
    }
}