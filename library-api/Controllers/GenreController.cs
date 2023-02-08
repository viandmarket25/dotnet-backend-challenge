using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Entities;
using library_api.Config;
using System;
using library_api.Services;
using library_api.Helpers;
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
    private IUserService _userService;

    public GenreController(ILogger<GenreController> logger, IUserService userService)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
         _userService = userService;
        _logger = logger;
    }
    // :::::::::::::::::::: get all book
    [HttpGet("genres")]
    public IActionResult GetGenres()
    {
        Console.WriteLine("get genres");
        // :::::::::::::::: Create a list of books
        List<dynamic> genres=new List<dynamic>();
        string query = "SELECT * FROM genres; ";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var genre=new Genre();
            genre.Id = reader.GetInt32(0);
            genre.Name = reader.GetString(1);
            Console.WriteLine(genre);
            genres.Add(genre);
            requestResponse=new RequestResponse{
                Message="success",
                Status="success",
                Result= genres,
                Code="200"
            };
            return Ok(requestResponse);
        }
        requestResponse=new RequestResponse{
            Message="no record found",
            Status="failed",
            Result= genres,
            Code="200"
        };
        return Ok(requestResponse);
    }

    // ::::::::::::::::::::::: get a book information
    [HttpGet("genre")]
    public IActionResult GetGenre(int Id)
    {
        Console.WriteLine("get genre");
        // :::::::::::::::: Get a genre
        List<dynamic> genres=new List<dynamic>();
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM genres WHERE  genres.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: get genre information
            var genre=new Genre();
            genre.Id = reader.GetInt32(0);
            genre.Name = reader.GetString(1);
            Console.WriteLine(genre);
            genres.Add(genre);
            requestResponse=new RequestResponse{
                Message="success",
                Result= genres,
                 Status="success",
                Code="200"
            };
            return Ok(requestResponse);
        }else{
            requestResponse=new RequestResponse{
                Message="failed, not found",
                Result= genres,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        } 
    }

    // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("add-genre")]
    public IActionResult AddGenre(Genre genre)
    {
        Console.WriteLine("add genre");
        // :::::::::::::::: Create a list of books
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
        requestResponse=new RequestResponse{
                Message="Successful, Record created",
                Result= new List<dynamic>(),
                Status="success",
                Code="200"
        };
        return Ok(requestResponse);
    }

     // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("update-genre")]
    public IActionResult UpdateGenre(string Name)
    {
        Console.WriteLine("update genre");
        // :::::::::::::::: Create a list of books



        return Ok(new Genre());
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
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success",
                Result= genres,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failure, Could not delete",
                Result= genres,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
        return Ok(new Genre());
    }

}
