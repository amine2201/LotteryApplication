using System.ComponentModel.DataAnnotations;

namespace LotteryApplication.Models
{
    public class Participation
    {
        [Key]
        public Guid Id { get; set; }
        public bool HaveWon { get; set; }
        public DateTime DateOfParticipation { get; set; }=DateTime.Now;
        public ApplicationUser? Participant { get; set; }
        
    }
}
