using Microsoft.AspNetCore.Identity;

namespace Primarie_Craiova.Services.Interfaces
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(string email, string password);
        Task<SignInResult> LoginAsync(string email, string password);
        Task LogoutAsync();
    }

}
