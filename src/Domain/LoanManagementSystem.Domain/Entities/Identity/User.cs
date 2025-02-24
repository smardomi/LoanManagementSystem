using Microsoft.AspNetCore.Identity;

namespace LoanManagementSystem.Domain.Entities.Identity
{
    public class User : IdentityUser<int>
    {
        public string Firstname { get; set; } = default!;
        public string Lastname { get; set; } = default!;
        public bool IsActice { get; set; } = true;
    }
}
