using DAL;  // Assuming your database context is in the DAL namespace
using DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models; // Assuming the Message and ArtistProfile models are in the Models namespace
using System.Threading.Tasks;

namespace Artist_Recruitment_Website.Controllers
{
    public class ChatController : Controller
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly AppDbContext _context;

        public ChatController(IHubContext<ChatHub> chatHubContext, AppDbContext context)
        {
            _chatHubContext = chatHubContext;
            _context = context;
        }

        // GET: /Chat/Index
        public IActionResult Index()
        {
            // Return the chat page view
            return View();
        }

        // Send a message (AJAX or SignalR action can be called here)
        public async Task<IActionResult> SendMessage(int senderId, int receiverId, string message)
        {
            // Validate sender and receiver
            var senderArtist = await _context.ArtistProfiles.FindAsync(senderId);
            var receiverArtist = await _context.ArtistProfiles.FindAsync(receiverId);

            if (senderArtist == null || receiverArtist == null)
            {
                return NotFound("Sender or Receiver not found.");
            }

            // Save the message to the database
            var msg = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = message,
                SentAt = DateTime.Now,
                IsRead = false
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            // Use SignalR to send the message to the receiver
            await _chatHubContext.Clients.User(receiverId.ToString())
                .SendAsync("ReceiveMessage", senderId.ToString(), message);

            return Ok();
        }
    }
}

