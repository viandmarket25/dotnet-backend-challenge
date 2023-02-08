
namespace library_api.Models;

public class Book
{
    public int Id { get; set; }
    public string? Isbn { get; set; }
    public string BookName { get; set; } = null!;
    public string? BookDescription{ get; set; }
    public string? BookCoverUrl { get; set; }
    public int GenreId { get; set; }
    public int CreatedBy { get; set; }
    public string BookAuthor { get; set; }
    public int IsAvailable { get; set; }
    public int IsReserved { get; set; }
    public int BookShelveId { get; set; }
    public string? BookEdition { get; set; }
    public string? ListDate { get; set; }
    public string?  ListTime { get; set; }

}