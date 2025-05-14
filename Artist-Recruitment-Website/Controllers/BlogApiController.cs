using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using DAL.Data;
using System.Security.Claims;

namespace Artist_Recruitment_Website.Controllers
{
    [Route("api/blog")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Identity.Application")]
    public class BlogApiController : ControllerBase
    {
        private readonly AppDbContext _context;
        public BlogApiController(AppDbContext context) { _context = context; }

        [HttpPost("likes/toggle")]
        public async Task<IActionResult> ToggleLike([FromBody] LikeRequest req)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artistProfileIdStr = User.FindFirst("ArtistProfile")?.Value;
            int artistProfileId = Convert.ToInt32(artistProfileIdStr);

            if (userId == null || artistProfileId == null)
                return Unauthorized();

            var like = await _context.Likes
                .FirstOrDefaultAsync(l => l.BlogPostId == req.BlogPostId && l.ArtistprofileId == artistProfileId);

            bool liked;
            if (like == null)
            {
                _context.Likes.Add(new Like
                {
                    BlogPostId = req.BlogPostId,
                    ArtistprofileId = artistProfileId,
                    CreatedAt = DateTime.UtcNow
                });
                liked = true;
            }
            else
            {
                _context.Likes.Remove(like);
                liked = false;
            }
            await _context.SaveChangesAsync();
            // Optionally return the new like count
            var likeCount = await _context.Likes.CountAsync(l => l.BlogPostId == req.BlogPostId);
            return Ok(new { liked, likeCount });
        }

        public class LikeRequest { public int BlogPostId { get; set; } }

        [HttpPost("comments/create")]
        public async Task<IActionResult> CreateComment([FromBody] CommentRequest req)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var artistProfileIdStr = User.FindFirst("ArtistProfile")?.Value;
            int? artistProfileId = null;
            if (int.TryParse(artistProfileIdStr, out var parsedId))
                artistProfileId = parsedId;

            if (userId == null || artistProfileId == null)
                return Unauthorized();

            var comment = new Comment
            {
                BlogPostId = req.BlogPostId,
                ArtistProfileId = artistProfileId,
                Text = req.Text,
                CreatedAt = DateTime.UtcNow
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var profile = await _context.ArtistProfiles.Include(p => p.User).FirstOrDefaultAsync(p => p.ArtistProfileId == artistProfileId);
            return Ok(new
            {
                username = profile?.User?.FullName ?? "Unknown",
                profileImage = profile?.ProfileImage,
                text = req.Text,
                createdAt = comment.CreatedAt
            });
        }

        public class CommentRequest
        {
            public int BlogPostId { get; set; }
            public string Text { get; set; }
        }
    }
}