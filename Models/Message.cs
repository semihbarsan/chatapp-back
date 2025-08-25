using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ChatApp.Models;

namespace ChatApp.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SenderId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime Timestamp { get; set; }

        public bool Read { get; set; } = false;
        public bool Delivered { get; set; } = false; // Mesajın alıcıya ulaşıp ulaşmadığını takip eder
        public bool IsDeleted { get; set; } = false; // Mesajın gönderici veya alıcı tarafından silinip silinmediğini takip eder

        // İlişkisel özellikler (Navigation properties)
        [ForeignKey("SenderId")]
        public virtual User Sender { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual User Receiver { get; set; }
    }
}