using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Entities;
using library_api.Config;
using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace library_api.Controllers;
[ApiController]
[Route("api/")]
public class GenreController : ControllerBase
{
    public  MySqlConnection  connection;
    private readonly ILogger<GenreController> _logger;
    private RequestResponse requestResponse;
    public GenreController(ILogger<GenreController> logger)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
    }
    // :::::::::::::::::::: get all book
    [HttpGet("genres")]
    public IActionResult GetShelves()
    {
        Console.WriteLine("get customers");
        // :::::::::::::::: Create a list of books
        List<Genre> genres=new List<Genre>();
        string query = "SELECT * FROM books;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var tempNotification=new Genre();
            Console.WriteLine(tempNotification);
            genres.Add(tempNotification);
        }
        return genres;
    }

    // ::::::::::::::::::::::: get a book information
    [HttpGet("genre")]
    public IActionResult GetShelf(int Id)
    {
        Console.WriteLine("get genre");
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
            var notification=new Genre();
            Console.WriteLine(notification);
            return notification;
        }else return new Genre();
    }

    // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("add-genre")]
    public IActionResult AddGenre(int Id)
    {
        Console.WriteLine("add genre");
        // :::::::::::::::: Create a list of books
           if(!userExist.checkUserExist(user.Username)){
            // :::::::::::::::::
            Console.WriteLine("add new genre");
            var queryStatement = "INSERT INTO genres( \n"+
            "ID, NAME) VALUES (@Id,@Name) ";
            // :::::::::::
            MySqlCommand command;
            command = new MySqlCommand(queryStatement,this.connection);
            command.Prepare();
            command.Parameters.AddWithValue("@Id", null);
            command.Parameters.AddWithValue("@Name", genre.Name);
            command.ExecuteNonQuery();
            // :::::::::::::::: 
            return Ok(new Genre());
        }
        return Ok(new Genre());
    }

     // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("update-genre")]
    public IActionResult UpdateGenre(string Name)
    {
        Console.WriteLine("update genre");
        // :::::::::::::::: Create a list of books



        return new Genre();
    }

    // :::::::::::::::::: admin delete notification
    [Authorize(Role.Admin)]
    [HttpDelete("admin-delete-genre/{Id}")]
    public IActionResult AdminDeleteGenre(int Id)
    {
        Console.WriteLine("delete genre");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> genres=new List<dynamic>();
        command  = new MySqlCommand(
            "DELETE FROM genres WHERE genres.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        if (command.ExecuteNonQuery()){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success",
                Result= genres,
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failure, Could not delete",
                Result= genres,
                Code="200"
            };
            return Ok(requestResponse);
        }

        return new Genre();
    }
}
