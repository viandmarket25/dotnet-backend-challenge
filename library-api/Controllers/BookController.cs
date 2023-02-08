using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Config;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using library_api.Entities;
namespace library_api.Controllers;
[ApiController]
//[Authorize]
[Route("api/")]
public class BooksController : ControllerBase
{
    public  MySqlConnection  connection;
    private readonly ILogger<BooksController> _logger;
    private RequestResponse requestResponse;
  
    public BooksController(ILogger<BooksController> logger)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
        //return _logger  ;
    }
    // :::::::::::::::::::: get all book
    [HttpGet("books")]
    public List<Book> Get()
    {
        Console.WriteLine("get books");
        // :::::::::::::::: Create a list of books
        List<Book> books=new List<Book>();
        string query = "SELECT * FROM books;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var tempBook=new Book();
            tempBook.Id=reader.GetInt32(0);
            tempBook.Isbn=reader.GetString(1);
            tempBook. BookName=reader.GetString(2);
            tempBook.BookDescription=reader.GetString(3);
            tempBook.BookCoverUrl=reader.GetString(4);
            tempBook.GenreId=reader.GetInt32(5);
            tempBook. CreatedBy=reader.GetInt32(6);
            tempBook. BookAuthor=reader.GetString(7);
            tempBook.IsAvailable=reader.GetInt32(8);
            tempBook.IsReserved=reader.GetInt32(9);
            tempBook.BookShelveId=reader.GetInt32(10);
            tempBook.BookEdition=reader.GetString(11);
            tempBook.ListDate =reader.GetString(12);
            tempBook.ListTime =reader.GetString(13);
            Console.WriteLine( tempBook);
            books.Add(tempBook);
        }
        return books;
        //.ToArray();
    }
    // ::::::::::::::::::::::: search a book information
    [HttpGet("search-books")]
    public async Task<ActionResult<List<Book>>>  SearchBook(string name)
    {
        Console.WriteLine("search books");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<Book> books=new List<Book>();
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE BOOK_NAME LIKE '%'@name'%' ",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@name", name);
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
            book.Id=reader.GetInt32(0);
            book.Isbn=reader.GetString(1);
            book.BookName=reader.GetString(2);
            book.BookDescription=reader.GetString(3);
            book.BookCoverUrl=reader.GetString(4);
            book.GenreId=reader.GetInt32(5);
            book.CreatedBy=reader.GetInt32(6);
            book.BookAuthor=reader.GetString(7);
            book.IsAvailable=reader.GetInt32(8);
            book.IsReserved=reader.GetInt32(9);
            book.BookShelveId=reader.GetInt32(10);
            book.BookEdition=reader.GetString(11);
            book.ListDate =reader.GetString(12);
            book.ListTime =reader.GetString(13);
            Console.WriteLine(book);
            books.Add(book);
        } return books;
    }
    // ::::::::::::::::::::::: get a book information
    [HttpGet("book")]
    public Book GetBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
            book.Id=reader.GetInt32(0);
            book.Isbn=reader.GetString(1);
            book.BookName=reader.GetString(2);
            book.BookDescription=reader.GetString(3);
            book.BookCoverUrl=reader.GetString(4);
            book.GenreId=reader.GetInt32(5);
            book.CreatedBy=reader.GetInt32(6);
            book.BookAuthor=reader.GetString(7);
            book.IsAvailable=reader.GetInt32(8);
            book.IsReserved=reader.GetInt32(9);
            book.BookShelveId=reader.GetInt32(10);
            book.BookEdition=reader.GetString(11);
            book.ListDate =reader.GetString(12);
            book.ListTime =reader.GetString(13);
            Console.WriteLine(book);
            return book;
        }else return new Book();
    }
    
    // ::::::::::::::::::::: add book information
    [HttpPost("add-book")]
    public Book AddBook(Book book)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
       // command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            //var book=new Book();
            book.Id=reader.GetInt32(0);
            book.Isbn=reader.GetString(1);
            book.BookName=reader.GetString(2);
            book.BookDescription=reader.GetString(3);
            book.BookCoverUrl=reader.GetString(4);
            book.GenreId=reader.GetInt32(5);
            book.CreatedBy=reader.GetInt32(6);
            book.BookAuthor=reader.GetString(7);
            book.IsAvailable=reader.GetInt32(8);
            book.IsReserved=reader.GetInt32(9);
            book.BookShelveId=reader.GetInt32(10);
            book.BookEdition=reader.GetString(11);
            book.ListDate =reader.GetString(12);
            book.ListTime =reader.GetString(13);
            Console.WriteLine(book);
            return book;
        }else return new Book();
    }

     // ::::::::::::::::::::: add book information
    [HttpPost("reserve-book")]
    public Book ReserveBook(ReservedBooks reservedBooks)
    {
        Console.WriteLine("reserve book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        //command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
           
            Console.WriteLine(book);


            return book;
        }else return new Book();
    }

     // ::::::::::::::::::::: add book information
    [HttpPost("issue-book")]
    public Book IssueBook(IssuedBooks issuedBooks)
    {
        Console.WriteLine("issue book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        //command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
         

            Console.WriteLine(book);
            return book;
        }else return new Book();
    }
    // ::::::::::::::::::::: update book information
    [HttpPut ("update-book")]
    public Book UpdateBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
            book.Id=reader.GetInt32(0);
            book.Isbn=reader.GetString(1);
            book.BookName=reader.GetString(2);
            book.BookDescription=reader.GetString(3);
            book.BookCoverUrl=reader.GetString(4);
            book.GenreId=reader.GetInt32(5);
            book.CreatedBy=reader.GetInt32(6);
            book.BookAuthor=reader.GetString(7);
            book.IsAvailable=reader.GetInt32(8);
            book.IsReserved=reader.GetInt32(9);
            book.BookShelveId=reader.GetInt32(10);
            book.BookEdition=reader.GetString(11);
            book.ListDate =reader.GetString(12);
            book.ListTime =reader.GetString(13);
            Console.WriteLine(book);
            return book;
        }else return new Book();
    }
    // :::::::::::::::::: delete book
    [HttpDelete("delete-book/{Id}")]
    public Book DeleteBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE  books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();
           
            Console.WriteLine(book);
            return book;
        }else return new Book();
    }
}
