namespace  library_api.Models;

using  library_api.Entities;

public class AuthenticateResponse
{
   
    public int Id { get; set; }
    public string? UserOfficialId { get; set; }
    public string Name { get; set; } = null!;
    public string Username{ get; set; }
    public string Email{ get; set; }
    public string Password{ get; set; }
    public string Token { get; set; }
    public Role Role { get; set; }

    public AuthenticateResponse(UserAuth user, string token)
    {
        Id = user.Id;
        UserOfficialId =user.UserOfficialId;
        Name =user.Name;
        Username =user.Username;
        Email =user. Email;
        Password =user.Password;
        Role = user.Role;
        Token = token;
    }
}