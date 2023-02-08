using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Config;
using library_api.Entities;
using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace library_api.Controllers;
[ApiController]
[Route("api/")]
public class ShelfController : ControllerBase
{
    public  MySqlConnection  connection;
    private readonly ILogger<ShelfController> _logger;
    private RequestResponse requestResponse;
    public ShelfController(ILogger<ShelfController> logger)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
    }

    // :::::::::::::::::::: get all book
    [HttpGet("shelves")]
    public IActionResult GetShelves()
    {
        Console.WriteLine("get shelves");
        // :::::::::::::::: Create a list of books
        List<dynamic> shelves=new List<dynamic>();
        string query = "SELECT * FROM shelves;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var shelve=new Shelf();
            shelve.Id = reader.GetInt32(0);
            shelve.Name = reader.GetString(1);
            shelve.Description =reader.GetString(2);
            Console.WriteLine(shelve);
            shelves.Add(shelve);
        }
        requestResponse=new RequestResponse{
            Message="success",
            Result=  shelves,
            Code="200"
        };
        return Ok(requestResponse);
    }
    // ::::::::::::::::::::::: get a book information
    [HttpGet("shelf")]
    public IActionResult GetShelf(int Id)
    {
        Console.WriteLine("get shelf");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> shelves=new List<dynamic>();
        command  = new MySqlCommand(
            "SELECT * FROM shelves WHERE shelves.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var shelf=new Shelf();
            shelve.Id = reader.GetInt32(0);
            shelve.Name = reader.GetString(1);
            shelve.Description =reader.GetString(2);
            Console.WriteLine(shelve);
            shelves.Add(shelve);
            requestResponse=new RequestResponse{
                Message="success",
                Result= shelves,
                Code="200"
            };
            return Ok(requestResponse);
        }else{ 
            requestResponse=new RequestResponse{
                Message="failed, no data received",
                Result= shelves,
                Code="200"
            };
            return Ok(requestResponse);}
    }

    // ::::::::::::::::::::: add book information
    [Authorize(Role.Admin)]
    [HttpPost("add-shelf")]
    public IActionResult AddShelf(Shelf shelf)
    {
        Console.WriteLine("add shelf");
        // :::::::::::::::: Add new Shelf
        if(!userExist.checkUserExist(user.Username)){
            // :::::::::::::::::
            Console.WriteLine("add new shelf");
            var queryStatement = "INSERT INTO shelves( \n"+
            "ID,NAME,DESCRIPTION) VALUES (@Id,@Name,@Description)";
            // :::::::::::
            MySqlCommand command;
            command = new MySqlCommand(queryStatement,this.connection);
            command.Prepare();
            command.Parameters.AddWithValue("@Id", null);
            command.Parameters.AddWithValue("@Name", shelf.Name);
            command.Parameters.AddWithValue("@Description", shelf.Description);
            command.ExecuteNonQuery();
            // :::::::::::::::: 
            return Ok(new Shelf());
        }
        return Ok(new Shelf());
    }

    // :::::::::::::::::: admin delete notification
    [Authorize(Role.Admin)]
    [HttpDelete("admin-delete-shelf/{Id}")]
    public IActionResult AdminDeleteShelf(int Id)
    {
        Console.WriteLine("delete shelf");
        // :::::::::::::::: delete shelf from db
        MySqlCommand command;
        List<dynamic> shelves=new List<dynamic>();
        command  = new MySqlCommand(
            "DELETE FROM shelves WHERE shelves.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        if (command.ExecuteNonQuery()){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success",
                Result= shelves,
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failure, Could not delete",
                Result= shelves,
                Code="200"
            };
            return Ok(requestResponse);
        }

        return new Shelf();
    }
}
