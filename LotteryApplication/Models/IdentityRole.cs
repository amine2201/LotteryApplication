using Microsoft.AspNetCore.Identity;

namespace LotteryApplication.Models
{
    public class IdentityRole : IdentityRole<string>
    {
        public IdentityRole() : base() { }
        public IdentityRole(string roleName) : base(roleName) { }
    }

}
