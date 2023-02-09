
namespace library_api.Models;

public class IssuedBooks
{

    // :::::book info

    public string? Isbn { get; set; }
    public string BookName { get; set; } = null!;
    public string? BookDescription { get; set; }
    public string? BookCoverUrl { get; set; }
    public int GenreId { get; set; }
    public int CreatedBy { get; set; }
    public string BookAuthor { get; set; }
    public int IsAvailable { get; set; }
    public int IsReserved { get; set; }
    public int BookShelveId { get; set; }
    public string? BookEdition { get; set; }
    public string? ListDate { get; set; }
    public string? ListTime { get; set; }


    public int IssuedBookId { get; set; }
    public int IssuedBy { get; set; }
    public int IssuedTo { get; set; }
    public int BookId { get; set; }
    public string? ExpiryDate { get; set; }
    public string DateIssued { get; set; } = null!;
    public string TimeIssued { get; set; } = null!;
    public string? ReturnDate { get; set; }
    public string? ReturnTime { get; set; }

}