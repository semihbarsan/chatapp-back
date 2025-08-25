using ChatApp.Data;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Services
{
    public interface IChatService
    {
        Task<Message> SaveMessageAsync(string senderId, string receiverId, string content);
        Task<List<Message>> GetChatHistoryAsync(string userId1, string userId2);
        Task<List<Message>> GetUserMessagesAsync(string userId);
        Task<bool> MarkMessageAsReadAsync(int messageId);
    }

    public class ChatService : IChatService
    {
        private readonly AppDbContext _context;

        public ChatService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Message> SaveMessageAsync(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow,
                Read = false,
                Delivered = false,
                IsDeleted = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Load the sender and receiver information for the response
            await _context.Entry(message)
                .Reference(m => m.Sender)
                .LoadAsync();

            await _context.Entry(message)
                .Reference(m => m.Receiver)
                .LoadAsync();

            return message;
        }

        public async Task<List<Message>> GetChatHistoryAsync(string userId1, string userId2)
        {
            return await _context.Messages
                .Where(m => !m.IsDeleted && 
                           ((m.SenderId == userId1 && m.ReceiverId == userId2) ||
                            (m.SenderId == userId2 && m.ReceiverId == userId1)))
                .OrderBy(m => m.Timestamp)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }

        public async Task<List<Message>> GetUserMessagesAsync(string userId)
        {
            return await _context.Messages
                .Where(m => !m.IsDeleted && 
                           (m.SenderId == userId || m.ReceiverId == userId))
                .OrderByDescending(m => m.Timestamp)
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .ToListAsync();
        }

        public async Task<bool> MarkMessageAsReadAsync(int messageId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message == null)
                return false;

            message.Read = true;
            message.Delivered = true;
            await _context.SaveChangesAsync();
            return true;
        }
    }
} 