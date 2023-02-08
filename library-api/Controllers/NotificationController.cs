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
    private RequestResponse requestResponse;
  
    public NotificationsController(ILogger<NotificationsController> logger)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
        //return _logger  ;
    }
    // :::::::::::::::::::: get all book
    [HttpGet("notifications")]
    public List<Notification> GetNotifications()
    {
        Console.WriteLine("get notifications");
        // :::::::::::::::: Create a list of books
        List<Notification> notifications=new List<Notification>();
        string query = "SELECT * FROM books;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var tempNotification=new Notification();
        
            Console.WriteLine(tempNotification);
            notifications.Add(tempNotification);
        }
        return notifications;
        //.ToArray();
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
