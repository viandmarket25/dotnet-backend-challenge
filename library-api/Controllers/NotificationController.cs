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
    public Notification GetNotification(int Id)
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
            var notification=new Notification();
           
            Console.WriteLine(notification);
            return notification;
        }else return new Notification();
    }
    // ::::::::::::::::::::: add book information
    [HttpPost("add-notification")]
    public Notification AddNotification(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        return new Notification();
 
    }
    // ::::::::::::::::::::: update book information
    [HttpPut ("read-notification")]
    public Notification ReadNotification(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        return new Notification();
    
    }
    // :::::::::::::::::: delete notification
    [HttpDelete("customer-delete-notification/{Id}")]
    public Notification CustomerDeleteBook(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        return new Notification();
     
    }
    // :::::::::::::::::: admin delete notification
    [HttpDelete("admin-delete-notification/{Id}")]
    public Notification AdminDeleteNotification(int Id)
    {
        Console.WriteLine("get book");
        // :::::::::::::::: Create a list of books
        return new Notification();
   
    }
}
