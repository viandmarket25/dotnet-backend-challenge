namespace library_api.Entities;
using System.Text.Json.Serialization;

public class UserAuth
{
    public int Id { get; set; }
    public string? UserOfficialId { get; set; }
    public string Name { get; set; } = null!;
    public string Email{ get; set; }
    public string Username{ get; set; }
    public string Password{ get; set; }
    public string Address { get; set; }
    public Role Role { get; set; }
    public string Gender { get; set; } = null!;
    public string Date_{ get; set; }
    public string Time_ { get; set; }
    public string PhoneNumber { get; set; }
}