
namespace library_api.Models;

public class IssuedBooks
{
    public int Id { get; set; }
    public int IssuedBy { get; set; }
    public int IssuedTo { get; set; }
    public int BookId { get; set; }
    public string? ExpiryDate { get; set; }
    public string DateIssued { get; set; } = null!;
    public string TimeIssued { get; set; } = null!;
    public string? ReturnDate { get; set; }
    public string? ReturnTime { get; set; }
 
}