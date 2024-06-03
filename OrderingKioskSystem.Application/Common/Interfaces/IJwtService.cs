namespace OrderingKioskSystem.Application.Common.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(string ID, string roles,string email);
        string CreateToken(string email, string roles);
    }
}
