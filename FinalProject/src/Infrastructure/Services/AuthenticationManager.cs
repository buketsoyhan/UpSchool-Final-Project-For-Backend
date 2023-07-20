using Application.Common.Interfaces;
using Application.Common.Models.Auth;


namespace Infrastructure.Services
{
    public class AuthenticationManager : IAuthenticationService
    {

        public async Task<JwtDto> SocialLoginAsync(string email, string firstName, string lastName, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
