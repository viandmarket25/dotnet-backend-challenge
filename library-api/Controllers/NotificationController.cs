using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using library_api.Config;
using library_api.Entities;
using System;
using library_api.Services;
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
            notification.CustomerId = reader.GetInt32(1);
            notification.Date_ =reader.GetString(2);
            notification.Time_ =reader.GetString(2);
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
            notification.CustomerId = reader.GetInt32(1);
            notification.Date_ =reader.GetString(2);
            notification.Time_ =reader.GetString(2);

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
    public IActionResult AddNotification(Notification  notification)
    {
        // :::::::::::::::::
        Console.WriteLine("add new notification");
        var queryStatement = "INSERT INTO notifications( \n"+
        "ID, TITLE, BODY, IS_READ, CUSTOMER_ID,DATE_,TIME_)\n"+
        " VALUES (@Id,@Title,@Description)";
        // :::::::::::
        MySqlCommand command;
        command = new MySqlCommand(queryStatement,this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", null);
        command.Parameters.AddWithValue("@Title", notification.Title);
        command.Parameters.AddWithValue("@Body", notification.Body);
        command.Parameters.AddWithValue("@IsRead", notification.IsRead);
        command.Parameters.AddWithValue("@CustomerId", notification.CustomerId);
        command.Parameters.AddWithValue("@Date_", notification.Date_);
        command.Parameters.AddWithValue("@Time_", notification.Time_);

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
    public  IActionResult ReadNotification(int IsRead)
    {
        Console.WriteLine("read notification");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> notifications=new List<dynamic>();
        var notification = new Notification();
        command  = new MySqlCommand(
            "UPDATE notifications SET notifications.IS_READ=@IsRead",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@IsRead", IsRead);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success",
                Result= notifications,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse); 
        }else{
            requestResponse=new RequestResponse{
                Message="failed, Could not delete",
                Result= notifications,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
    
    }
    // :::::::::::::::::: delete notification
    [HttpDelete("customer-delete-notification/{Id}")]
    public  IActionResult CustomerDeleteBook(int Id)
    {
        Console.WriteLine("delete notification");
        // :::::::::::::::: delete notification
        MySqlCommand command;
        List<dynamic> shelves=new List<dynamic>();
        command  = new MySqlCommand(
            "DELETE FROM notifications WHERE notifications.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: shelf reccord has been deleted
            requestResponse=new RequestResponse{
                Message="success",
                Result= shelves,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failed, Could not delete",
                Result= shelves,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
     
    }
   
}
