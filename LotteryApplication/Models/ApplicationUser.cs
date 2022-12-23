using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LotteryApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public bool IsAdmin { get; set; }
        public virtual Participation? Participation { get; set; }
    }
    
}
