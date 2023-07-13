using Application.Common.Models.Auth;

namespace Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JwtDto> SocialLoginAsync(string email, string firstName, string lastName, CancellationToken cancellationToken);
    }
}
