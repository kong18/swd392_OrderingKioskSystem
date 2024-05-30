namespace OrderingKioskSystem.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {   string? UserEmail { get; }
        string? UserId { get; }
        Task<bool> IsInRoleAsync(string role);
        Task<bool> AuthorizeAsync(string policy);
    }
}
