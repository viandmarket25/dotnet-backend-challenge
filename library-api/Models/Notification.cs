
namespace library_api.Models;

public class Notification
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string Body { get; set; } = null!;
    public int IsRead{ get; set; }
    public int CustomerId { get; set; }

}