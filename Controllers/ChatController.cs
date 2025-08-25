using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize] // Test i�in kapal�
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageRequest request)
        {
            try
            {
                // Test i�in sabit kullan�c�
                var currentUserId = "test-user-id";

                var message = await _chatService.SaveMessageAsync(currentUserId, request.ReceiverId, request.Content);

                // DTO olu�tur
                var dto = new MessageDto
                {
                    Id = message.Id,
                    SenderId = message.SenderId,
                    ReceiverId = message.ReceiverId,
                    Content = message.Content,
                    Timestamp = message.Timestamp
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error sending message", error = ex.Message });
            }
        }

        // GET kullan�c� mesajlar�n� test etmek i�in
        [HttpGet("messages")]
        public async Task<IActionResult> GetUserMessages()
        {
            try
            {
                var currentUserId = "test-user-id";

                var messages = await _chatService.GetUserMessagesAsync(currentUserId);

                // DTO listesi
                var dtos = messages.Select(m => new MessageDto
                {
                    Id = m.Id,
                    SenderId = m.SenderId,
                    ReceiverId = m.ReceiverId,
                    Content = m.Content,
                    Timestamp = m.Timestamp
                }).ToList();

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving messages", error = ex.Message });
            }
        }

        // DTO s�n�f�
        public class MessageDto
        {
            public int Id { get; set; }
            public string SenderId { get; set; }
            public string ReceiverId { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }
        }

        // POST body i�in request
        public class SendMessageRequest
        {
            public string ReceiverId { get; set; }
            public string Content { get; set; }
        }
    }
}
