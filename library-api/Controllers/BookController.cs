using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Config;
using library_api.Entities;
using library_api.Helpers;
using System;
using System.Data;
using library_api.Services;
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
    private IUserService _userService;
    private RequestResponse requestResponse;
  
    public BooksController(ILogger<BooksController> logger,IUserService userService)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
    }
    // :::::::::::::::::::: get all book
    [HttpGet("books")]
    public  IActionResult Get()
    {
        Console.WriteLine("get books");
        // :::::::::::::::: Create a list of books
        List<dynamic> books=new List<dynamic>();
        string query = "SELECT * FROM books;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var tempBook=new Book();
            tempBook.Id=reader.GetInt32(0);
            tempBook.Isbn=reader.GetString(1);
            tempBook.BookName=reader.GetString(2);
            tempBook.BookDescription=reader.GetString(3);
            tempBook.BookCoverUrl=reader.GetString(4);
            tempBook.GenreId=reader.GetInt32(5);
            tempBook.CreatedBy=reader.GetInt32(6);
            tempBook.BookAuthor=reader.GetString(7);
            tempBook.IsAvailable=reader.GetInt32(8);
            tempBook.IsReserved=reader.GetInt32(9);
            tempBook.BookShelveId=reader.GetInt32(10);
            tempBook.BookEdition=reader.GetString(11);
            tempBook.ListDate =reader.GetString(12);
            tempBook.ListTime =reader.GetString(13);
            Console.WriteLine( tempBook);
            books.Add(tempBook);
        }
        requestResponse=new RequestResponse{
            Message="success",
            Result= books,
            Status="success",
            Code="200"
        };
        return Ok(requestResponse);
      
    }
    // ::::::::::::::::::::::: search a book information
    [HttpGet("search-books")]
    public IActionResult  SearchBook(string name)
    {
        Console.WriteLine("search books");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
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
        }
        requestResponse=new RequestResponse{
            Message="success",
            Result= books,
            Status="success",
            Code="200"
        };
        return Ok(requestResponse);
    }
    // ::::::::::::::::::::::: get a book information
    [HttpGet("book")]
    public  IActionResult GetBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
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
            books.Add(book);
            requestResponse=new RequestResponse{
                Message="success",
                Result= books,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);
            
        }else{
            requestResponse=new RequestResponse{
                Message="failed, no record found",
                Result= books,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        } 
    }
    
    // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("add-book")]
    public IActionResult AddBook(Book book)
    {
        Console.WriteLine("add book");
        var queryStatement = "INSERT INTO books( \n"+
        "BOOK_ID, ISBN, BOOK_NAME,BOOK_DESCRIPTION,BOOK_COVER_URL,\n"+
        "GENRE_ID,CREATED_BY,BOOK_AUTHOR,IS_AVAILABLE,\n"+
        "IS_RESERVED,BOOK_SHELVE_ID, BOOK_EDITION,LIST_DATE,LIST_TIME \n"+
        ") VALUES(@Id,@Isbn,@BookName,@BookDescription,@BookCoverUrl,@GenreId,@CreatedBy,@BookAuthor,@IsAvailable,@IsReserved,@BookShelveId,@BookEdition,@ListDate,@ListTime)";
        // :::::::::::
        MySqlCommand command;
        command = new MySqlCommand(queryStatement,this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", null);
        command.Parameters.AddWithValue("@Isbn", book.Isbn);
        command.Parameters.AddWithValue("@BookName", book.BookName);
        command.Parameters.AddWithValue("@BookDescription", book.BookDescription);
        command.Parameters.AddWithValue("@BookCoverUrl", book.BookCoverUrl);
        command.Parameters.AddWithValue("@GenreId", book.GenreId);
        command.Parameters.AddWithValue("@CreatedBy", book.CreatedBy);
        command.Parameters.AddWithValue("@BookAuthor", book.BookAuthor);
        command.Parameters.AddWithValue("@IsAvailable",1);
        command.Parameters.AddWithValue("@IsReserved", 0);
        command.Parameters.AddWithValue("@BookShelveId", book.BookShelveId);
        command.Parameters.AddWithValue("@BookEdition", book.BookEdition);
        command.Parameters.AddWithValue("@ListDate", book.ListDate);
        command.Parameters.AddWithValue("@ListTime", book.ListTime);
        command.ExecuteNonQuery();
        // :::::::::::::::: 
        return Ok(new Book());
      
    }

    // ::::::::::::::::::::: add book information
    [HttpPost("reserve-book")]
    public  IActionResult ReserveBook(IssueReserveBook issueReserveBook)
    {
        Console.WriteLine("reserve book");
        // :::::::::::::::: Create a list of books
        BookIsReserved bookIsReserved=new BookIsReserved();
        BookIsIssued bookIsIssued = new BookIsIssued();
        BookIsReservedBySameUser bookIsReservedBySameUser = new BookIsReservedBySameUser();
        // :::::::: check is book is available
        if(bookIsIssued.checkBookIsIssued(issueReserveBook.BookId)){
            // ::::::::::: check if it has not been reserved,
            if(!bookIsReserved.checkBookIsReserved(issueReserveBook.BookId)){
                // ::::::::: if book is not reserved
                // ::::::::: reserve book
                Console.WriteLine("reserve book");
                var queryStatement = "INSERT INTO reserved_books( \n"+
                "ID, RESERVED_BY, RESERVED_FOR,BOOK_ID,RESERVE_EXPIRY_DATE,RESERVE_EXPIRY_TIME,\n"+
                "RESERVE_DATE, RESERVE_TIME,\n"+
                ") VALUES(@Id,@ReservedBy,@ReservedFor,@BookId,@ReserveExpiryDate,@ReserveExpiryTime,@ReserveDate,@ReserveTime)";
                // :::::::::::
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                // ::::::::: add 24 hours
                var expDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1)); 
                DateTime currentTime = DateTime.Now;
                // :::::::: 60 minutes (1 hour x 24 = 25 hours, 1 day)
                DateTime expTime = currentTime.AddMinutes(60*24);

                MySqlCommand command;
                command = new MySqlCommand(queryStatement,this.connection);
                command.Prepare();
                command.Parameters.AddWithValue("@Id", null);
                command.Parameters.AddWithValue("@Reservedby", issueReserveBook.UserId);
                command.Parameters.AddWithValue("@ReservedFor", issueReserveBook.UserId);
                command.Parameters.AddWithValue("@BookId", issueReserveBook.BookId);
                command.Parameters.AddWithValue("@ReserveExpiryDate", expDate);
                command.Parameters.AddWithValue("@ReserveExpiryTime", expTime);
                command.Parameters.AddWithValue("@ReserveDate", expDate);
                command.Parameters.AddWithValue("@ReserveTime", currentTime);
                command.ExecuteNonQuery();
                // :::::::::::::::: set book to reserved
                SetBookReserved setBookReserved = new SetBookReserved();
                setBookReserved.setReserved(issueReserveBook.BookId);
                return Ok(new Book());
            }
        }else{
            // :::::::::::: book has already been issued
            return Ok(new Book());
        }
        return Ok(new Book());

    }

    // ::::::::::::::::::::: add book information
    [HttpPost("issue-book")]
    public  IActionResult IssueBook(IssueReserveBook issueReserveBook)
    {
        BookIsReserved bookIsReserved=new BookIsReserved();
        BookIsIssued bookIsIssued = new BookIsIssued();
        BookIsReservedBySameUser bookIsReservedBySameUser = new BookIsReservedBySameUser();
        // :::::::: check is book is available
        // :::::::: then check if it is reserved, if it is reserved by same user then issue
        if(bookIsIssued.checkBookIsIssued(issueReserveBook.BookId)){
            if(!bookIsReserved.checkBookIsReserved(issueReserveBook.BookId)){
                // ::::::::: if book is not reserved
                // ::::::::: issue book
                Console.WriteLine("issue book");
                var queryStatement = "INSERT INTO issued_books( \n"+
                "ID, ISSUED_BY, ISSUED_TO, BOOK_ID, EXPIRY_DATE,\n"+
                "DATE_ISSUED, TIME_ISSUED, RETURN_DATE, RETURN_TIME\n"+
                ") VALUES(@Id, @IssuedBy, @IssuedTo, @BookId, @ExpiryDate, @DateIssued, @TimeIssued, @ReturnDate, @ReturnTime)";
                // :::::::::::
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var currentTime = TimeOnly.FromDateTime(DateTime.Now);
                var expDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)); 
                var expTime = new TimeOnly(08, 00);

                MySqlCommand command;
                command = new MySqlCommand(queryStatement,this.connection);
                command.Prepare();
                command.Parameters.AddWithValue("@Id", null);
                command.Parameters.AddWithValue("@Issuedby", issueReserveBook.UserId);
                command.Parameters.AddWithValue("@IssuedTo",  issueReserveBook.UserId);
                command.Parameters.AddWithValue("@BookId",  issueReserveBook.BookId);
                command.Parameters.AddWithValue("@ExpiryDate", expDate);
                command.Parameters.AddWithValue("@DateIssued", currentDate);
                command.Parameters.AddWithValue("@TimeIssued", currentTime);
                command.Parameters.AddWithValue("@ReturnDate", expDate);
                command.Parameters.AddWithValue("@ReturnTime", expTime);
                command.ExecuteNonQuery();
                // :::::::::::::::: set book to unavailable
                SetBookAvailable setBookAvailable = new SetBookAvailable();
                setBookAvailable.setUnAvailable(issueReserveBook.BookId);
                return Ok(new Book());

            }else if(bookIsReserved.checkBookIsReserved(issueReserveBook.BookId) && bookIsReservedBySameUser.checkBookIsReservedBySameUser(issueReserveBook) ){
                // :::::::: book is reserved but same user wants to usee
                // ::::::::: issue book
                Console.WriteLine("issue book");
                var queryStatement = "INSERT INTO issued_books( \n"+
                "ID, ISSUED_BY, ISSUED_TO,BOOK_ID,EXPIRY_DATE,\n"+
                "DATE_ISSUED,TIME_ISSUED,RETURN_DATE,RETURN_TIME\n"+
                ") VALUES(@Id,@IssuedBy,@IssuedTo,@BookId,@ExpiryDate,@DateIssued,@TimeIssued,@ReturnDate,@ReturnTime)";
                // :::::::::::
                var currentDate = DateOnly.FromDateTime(DateTime.Now);
                var currentTime = TimeOnly.FromDateTime(DateTime.Now);
                var expDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(7)); 
                var expTime = new TimeOnly(08, 00);
                MySqlCommand command;
                command = new MySqlCommand(queryStatement,this.connection);
                command.Prepare();
                command.Parameters.AddWithValue("@Id", null);
                command.Parameters.AddWithValue("@Issuedby", issueReserveBook.UserId);
                command.Parameters.AddWithValue("@IssuedTo",  issueReserveBook.UserId);
                command.Parameters.AddWithValue("@BookId",  issueReserveBook.BookId);
                command.Parameters.AddWithValue("@ExpiryDate", expDate);
                command.Parameters.AddWithValue("@DateIssued", currentDate);
                command.Parameters.AddWithValue("@TimeIssued", currentTime);
                command.Parameters.AddWithValue("@ReturnDate", expDate);
                command.Parameters.AddWithValue("@ReturnTime", expTime);
                command.ExecuteNonQuery();
                // :::::::::::::::: set book to unavailable
                SetBookAvailable setBookAvailable = new SetBookAvailable();
                setBookAvailable.setUnAvailable(issueReserveBook.BookId);
                return Ok(new Book());

            }
        }else{
            // :::::::::::: book has already been issued


        }
        return Ok(new Book());
    }

    // ::::::::::::::::::::: add book information
    [HttpPost("return-book")]
    public  IActionResult ReturnBook(IssueReserveBook issueReserveBook)
    {
        Console.WriteLine("return book");
        // :::::::::::::::: return an issued book
        // :::::: remove reserve record
        RemoveReservedBook removeReservedBook =new RemoveReservedBook();
        removeReservedBook.removeReserved( issueReserveBook.BookId);
        // :::::: remove issued record
        RemoveIssuedBook removeIssuedBook=new RemoveIssuedBook();
        removeIssuedBook.removeIssued(issueReserveBook.BookId);
        // :::::: set available
        SetBookAvailable setBookAvailable = new SetBookAvailable();
        setBookAvailable.setAvailable(issueReserveBook.BookId);
        // :::::: set iunreserved
        SetBookReserved setBookReserved = new SetBookReserved();
        setBookReserved.setReserved(issueReserveBook.BookId);
        return Ok(new Book());
    }

     // ::::::::::::::::::::: add book information
    [HttpGet("get-issued-books{UserId}")]
    public  IActionResult GetIssuedBooks(int UserId)
    {
        Console.WriteLine("get issued books ");
        // :::::::::::::::: return an issued book
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        List<dynamic> issuedBooks=new List<dynamic>();
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books JOIN issued_books on books.BOOK_ID=issued_books.BOOK_ID;", this.connection);
        command.Prepare();
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new IssuedBooks();
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
            book.IssuedBookId = reader.GetInt32(14);
            book.IssuedBy = reader.GetInt32(15);
            book.IssuedTo = reader.GetInt32(16);
            book.BookId = reader.GetInt32(17);
            book.ExpiryDate = reader.GetString(18);
            book.DateIssued = reader.GetString(19);
            book.TimeIssued = reader.GetString(20);
            book.ReturnDate = reader.GetString(21);
            book.ReturnTime = reader.GetString(22);
        
            Console.WriteLine(book);
            issuedBooks.Add(book);
            requestResponse=new RequestResponse{
                Message="success",
                Result= issuedBooks,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);
        }else{
            requestResponse=new RequestResponse{
                Message="failed, no record found",
                Result= books,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
        return Ok(new Book());
    }

     // ::::::::::::::::::::: add book information
    [HttpGet("get-reserved-books{UserId}")]
    public  IActionResult GetReservedBooks(int UserId)
    {
        Console.WriteLine("get reserved books");
        // :::::::::::::::: return an issued book
        // :::::: remove reserve record
         MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        List<dynamic> issuedBooks=new List<dynamic>();
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books JOIN reserved_books on books.BOOK_ID=reserved_books.BOOK_ID;", this.connection);
        command.Prepare();
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new ReservedBooks();
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
            book.ReservedBookId = reader.GetInt32(14);
            book.ReservedBy = reader.GetInt32(15);
            book.ReservedFor = reader.GetInt32(16);
            book.BookId = reader.GetInt32(17);
            book.ReserveExpiryDate = reader.GetString(18);
            book.ReserveExpiryTme = reader.GetString(19);
            book.ReserveDate  = reader.GetString(20);
            book.ReserveTime = reader.GetString(21);
    
            Console.WriteLine(book);
            issuedBooks.Add(book);
            requestResponse=new RequestResponse{
                Message="success",
                Result= issuedBooks,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);
        }else{
            requestResponse=new RequestResponse{
                Message="failed, no record found",
                Result= books,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }

        return Ok(new Book());
       
    }

    // ::::::::::::::::::::: update book information
    [HttpPut ("update-book")]
    public  IActionResult UpdateBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        //string query = "SELECT * FROM books WHERE books.BOOK_ID== @Id;";
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
       
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var book=new Book();

           return Ok(new Book());
        }else return Ok(new Book());
    }
    // :::::::::::::::::: delete book
    [Authorize(Role.Admin)]
    [HttpDelete("delete-book/{Id}")]
    public  IActionResult DeleteBook(IssueReserveBook issueReserveBook)
    {
        Console.WriteLine("delete book");
        BookIsReserved bookIsReserved=new BookIsReserved();
        BookIsIssued bookIsIssued = new BookIsIssued();
        BookIsReservedBySameUser bookIsReservedBySameUser = new BookIsReservedBySameUser();
        // :::::::: check is book is available
        // :::::::: then check if it is reserved, if it is reserved by same user then issue
        List<dynamic> books=new List<dynamic>();
        if(!bookIsIssued.checkBookIsIssued(issueReserveBook.BookId)){
            // :::::: remove reserve record if there is any
            RemoveReservedBook removeReservedBook =new RemoveReservedBook();
            removeReservedBook.removeReserved( issueReserveBook.BookId);
            // :::::: delete book record
            MySqlCommand command;
            command  = new MySqlCommand(
                "DELETE FROM books WHERE books.BOOK_ID=@Id",this.connection);
            command.Prepare();
            command.Parameters.AddWithValue("@Id", issueReserveBook.BookId);
            using var reader = command.ExecuteReader();
            if (command.ExecuteNonQuery()>0){
                // :::::::::::::: Create a book Object to hold db data
                requestResponse=new RequestResponse{
                    Message="success, book is now deleted",
                    Result= books,
                    Status="success",
                    Code="200"
                };
                return Ok(requestResponse); 
            }else{
                requestResponse=new RequestResponse{
                    Message="failed, Could not delete",
                    Result= books,
                    Status="failed",
                    Code="200"
                };
                return Ok(requestResponse);
            }
        }else{
            requestResponse=new RequestResponse{
                    Message="failed, sorry you cannot delete this book at the moment",
                    Result= books,
                    Status="failed",
                    Code="200"
                };
            return Ok(requestResponse);

        }

    }
}
