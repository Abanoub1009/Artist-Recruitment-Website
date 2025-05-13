using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using DAL.Data;

namespace DAL
{
    public class ChatHub : Hub
    {
        private static readonly Dictionary<string, string> OnlineUsers = new(); // ConnectionId → UserId
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;

        public ChatHub(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {
            var user = await _userManager.GetUserAsync(Context.User);
            if (user != null)
            {
                OnlineUsers[Context.ConnectionId] = user.Id.ToString();
                await Clients.All.SendAsync("UsersOnline", OnlineUsers.Values.Distinct().ToList());
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            OnlineUsers.Remove(Context.ConnectionId);
            await Clients.All.SendAsync("UsersOnline", OnlineUsers.Values.Distinct().ToList());
        }

        public async Task SendPrivateMessage(int senderArtistId, int receiverArtistId, string message)
        {
            var receiverArtist = await _context.ArtistProfiles
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ArtistProfileId == receiverArtistId);

            var senderArtist = await _context.ArtistProfiles
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.ArtistProfileId == senderArtistId);

            if (receiverArtist == null || senderArtist == null) return;

            // Save message to the database
            var msg = new Message
            {
                Content = message,
                SenderId = senderArtistId,
                ReceiverId = receiverArtistId,
                SentAt = DateTime.Now
            };

            _context.Messages.Add(msg);
            await _context.SaveChangesAsync();

            // Send message to the receiver
            await Clients.User(receiverArtist.UserId.ToString())
                .SendAsync("ReceiveMessage", senderArtistId.ToString(), message);
        }
    }
}
