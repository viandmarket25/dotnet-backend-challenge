
namespace library_api.Models;

public class User
{
    public int Id { get; set; }
    public string? UserOfficialId { get; set; }
    public string Name { get; set; } = null!;
    public string Email{ get; set; }
    public string Username{ get; set; }
    public string Password{ get; set; }
    public string Address { get; set; }
    public int Role { get; set; }
    public string Gender { get; set; } = null!;
    public string Date_{ get; set; }
    public string Time_ { get; set; }
    public string PhoneNumber { get; set; }

}