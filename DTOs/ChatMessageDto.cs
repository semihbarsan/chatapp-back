using System.ComponentModel.DataAnnotations;

namespace ChatApp.DTOs
{
    public class ChatMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public bool Read { get; set; }
        public bool Delivered { get; set; }
        public string? RoomName { get; set; } // For room messages
    }

    public class SendMessageRequest
    {
        [Required]
        public string ReceiverId { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }

    public class SendRoomMessageRequest
    {
        [Required]
        public string RoomName { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Content { get; set; }
    }

    public class JoinRoomRequest
    {
        [Required]
        public string RoomName { get; set; }
    }
} 