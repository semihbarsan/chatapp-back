using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ChatApp.Models;

namespace ChatApp.Models
{
    public class User : IdentityUser
    {
        // Navigation Properties - BUNLARI EKLEYİN
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        // Constructor
        public User()
        {
            SentMessages = new HashSet<Message>();
            ReceivedMessages = new HashSet<Message>();
        }
    }
}