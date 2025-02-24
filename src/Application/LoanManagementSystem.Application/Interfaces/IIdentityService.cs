using LoanManagementSystem.Application.Common.Models;
using LoanManagementSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace LoanManagementSystem.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string> GetUserNameAsync(int userId);

    Task<User?> GetUserAsync(string username);

    Task<bool> IsInRoleAsync(int userId, string role);

    Task PasswordSignInAsync(User user, string password);

    string GeneratJwtToken(User user);

    Task<IdentityResult> CreateUserAsync(User user, string password);

    Task<IdentityResult> DeleteUserAsync(int userId);
}
