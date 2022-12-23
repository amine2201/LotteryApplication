using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LotteryApplication.Models
{
    public class Participation
    {
        [Key]
        public Guid Id { get; set; }
        public bool HaveWon { get; set; }
        public DateTime DateOfParticipation { get; set; }=DateTime.Now;
        public string? ParticipantId { get; set; }
        public ApplicationUser? Participant { get; set; }
        
    }
}
