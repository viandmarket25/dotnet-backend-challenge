
namespace library_api.Models;

public class ReservedBooks
{
    public int Id { get; set; }
    public int ReservedBy { get; set; }
    public int ReservedFor { get; set; }
    public int BookId { get; set; }
    public string? ReserveExpiry { get; set; }
    public string ReserveDate { get; set; } = null!;
    public string ReserveTime { get; set; } = null!;

}