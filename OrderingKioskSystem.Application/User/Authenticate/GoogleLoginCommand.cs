using MediatR;

namespace OrderingKioskSystem.Application.User.Authenticate
{
    public class GoogleLoginCommand : IRequest<string>
    {
        public string Email { get; set; }
        public string Role { get; set; } // "Business" or "Manager"

        public GoogleLoginCommand(string email, string role)
        {
            Email = email;
            Role = role;
        }
    }
}
