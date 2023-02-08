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
public class NotificationsController : ControllerBase
{
    public  MySqlConnection  connection;
    private readonly ILogger<NotificationsController> _logger;
    private IUserService _userService;
    private RequestResponse requestResponse;
  
    public NotificationsController(ILogger<NotificationsController> logger,IUserService userService)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _userService = userService;
        _logger = logger;
    }
    // :::::::::::::::::::: get all notifications
    [HttpGet("notifications")]
    public IActionResult GetNotifications()
    {
        Console.WriteLine("get notifications");
        // :::::::::::::::: Create a list of books
        List<dynamic> notifications=new List<dynamic>();
        string query = "SELECT * FROM notifications;";
        using var command = new MySqlCommand(query,this.connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var notification = new Notification();
            notification.Id = reader.GetInt32(0);
            notification.Title = reader.GetString(1);
            notification.Body =reader.GetString(2);
            notification.IsRead = reader.GetInt32(0);
            notification.CustomerId = reader.GetString(1);
            Console.WriteLine(notification);
            notifications.Add(notification);
        }
        requestResponse=new RequestResponse{
            Message="success",
            Result=  notifications,
            Status="success",
            Code="200"
        };
        return Ok(requestResponse);
    }
    // ::::::::::::::::::::::: get a book information
    [HttpGet("notification")]
    public  IActionResult GetNotification(int Id)
    {
        Console.WriteLine("get notification");
        // :::::::::::::::: get a notification s
        List<dynamic> notifications=new List<dynamic>();
        string query = "SELECT * FROM notifications;";
        using var command = new MySqlCommand(query,this.connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var notification = new Notification();
            notification.Id = reader.GetInt32(0);
            notification.Title = reader.GetString(1);
            notification.Body =reader.GetString(2);
            notification.IsRead = reader.GetInt32(0);
            notification.CustomerId = reader.GetString(1);
            Console.WriteLine(notification);
            notifications.Add(notification);
            requestResponse=new RequestResponse{
                Message="success",
                Result=  notifications,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);
        }else{
            requestResponse=new RequestResponse{
                Message="failed, no data received",
                Result=  notifications,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
    }
    // ::::::::::::::::::::: add book information
    [HttpPost("add-notification")]
    public  IActionResult AddNotification(Notification  notification)
    {
        // :::::::::::::::::
        Console.WriteLine("add new notification");
        var queryStatement = "INSERT INTO shelves( \n"+
        "ID, TITLE, BODY, IS_READ, CUSTOMER_ID,DATE_,TIME_)\n"+
        " VALUES (@Id,@Name,@Description)";
        // :::::::::::
        MySqlCommand command;
        command = new MySqlCommand(queryStatement,this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", null);
        command.Parameters.AddWithValue("@Title", notification.Name);
        command.Parameters.AddWithValue("@Body", notification.Description);
        command.Parameters.AddWithValue("@IsRead", null);
        command.Parameters.AddWithValue("@CustomerId", notification.Name);
        command.Parameters.AddWithValue("@Date_", notification.Description);
        command.Parameters.AddWithValue("@Time_", notification.Description);


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
    // ::::::::::::::::::::: update notification read
    [HttpPut ("read-notification")]
    public  IActionResult ReadNotification(int Id)
    {
        Console.WriteLine("read notification");
        // :::::::::::::::: Create a list of books
        return new Notification();
    
    }
    // :::::::::::::::::: delete notification
    [HttpDelete("customer-delete-notification/{Id}")]
    public  IActionResult CustomerDeleteBook(int Id)
    {
        Console.WriteLine("delete notification");
        // :::::::::::::::: Create a list of books
        return new Notification();
     
    }
   
}
