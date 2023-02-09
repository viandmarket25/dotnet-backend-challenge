
namespace library_api.Models;

public class ReservedBooks
{
    // ::::::::::::: issued book info
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

    // ::::::: reserved book info
    public int ReservedBookId { get; set; }
    public int ReservedBy { get; set; }
    public int ReservedFor { get; set; }
    public int BookId { get; set; }
    public string? ReserveExpiryDate { get; set; }
    public string? ReserveExpiryTme { get; set; }
    public string ReserveDate { get; set; } = null!;
    public string ReserveTime { get; set; } = null!;

}