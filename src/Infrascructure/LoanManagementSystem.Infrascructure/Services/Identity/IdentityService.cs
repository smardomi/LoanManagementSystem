using LoanManagementSystem.Application.Common.Interfaces;
using LoanManagementSystem.Application.Common.Models;
using LoanManagementSystem.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoanManagementSystem.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly SignInManager<User> _signInManager;


    public IdentityService(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    }

    public async Task<string> GetUserNameAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString())!;

        return user.UserName;
    }

    public async Task<IdentityResult> CreateUserAsync(User user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(int userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        //var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return true;
    }

    public async Task<IdentityResult> DeleteUserAsync(int userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());

        return user != null ? await DeleteUserAsync(user) : IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteUserAsync(User user)
    {
        return await _userManager.DeleteAsync(user);       
    }

    public async Task<User?> GetUserAsync(string username)
    {
        return await _userManager.FindByNameAsync(username);
    }

    public async Task PasswordSignInAsync(User user, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        if (!result.Succeeded)
        {
            throw new UnauthorizedAccessException("username or password is invalid");
        }
    }

    public string GeneratJwtToken(User user)
    {
        var claims = new[]
         {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("d712a55bf0d3ae25d16460e933eeb570a8e605eb6df313ee98689d7a5fe6b3a3"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "https://smardomi.ir",
            audience: "https://smardomi.ir",
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
