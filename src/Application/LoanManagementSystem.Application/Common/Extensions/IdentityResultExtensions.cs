using LoanManagementSystem.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace LoanManagementSystem.Application.Common.Extensions;
public static class IdentityResultExtensions
{
    public static Result ToApplicationResult(this IdentityResult result)
    {
        return result.Succeeded
            ? Result.Success()
            : throw new Exceptions.ValidationException(result.Errors.Select(e => e.Description));
    }
}
