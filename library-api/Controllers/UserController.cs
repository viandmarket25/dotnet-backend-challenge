using Microsoft.AspNetCore.Mvc;
using library_api.Models;
using System;
using System.Data;
using MySql.Data.MySqlClient;
using library_api.Controllers;
using Microsoft.AspNetCore.Mvc;
using library_api.Helpers;
using library_api.Models;
using library_api.Services;
using library_api.Config;
using library_api.Entities;
namespace library_api.Controllers;

// ::::::::::::::
[ApiController]
[Route("api/")]
public class UsersController : ControllerBase
{
    public  MySqlConnection  connection;
    private readonly ILogger<UsersController> _logger;
    private IUserService _userService;
    private RequestResponse requestResponse;

    public UsersController(ILogger<UsersController> logger,IUserService userService)
    {
        _userService = userService;
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _logger = logger;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        Console.WriteLine("login information:: "+model.Username);
        Console.WriteLine("login information:: "+model.Password);
        var response = _userService.Authenticate(model);
        if (response == null)
            return BadRequest(new { message = "Username or password is incorrect" });
        return Ok(response);
    }
    
    // :::::::::::::::::::: get all users
    [Authorize(Role.Admin)]
    [HttpGet("users")]
    public IActionResult GetUsers()
    {
        Console.WriteLine("get users");
        // :::::::::::::::: Create a list of books
        List<dynamic> users=new List<dynamic>();
        string query = "SELECT * FROM users;";
        using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        while (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var user=new User();
            user.Id = reader.GetInt32(0);
            user.UserOfficialId = reader.GetString(1);
            user.Name =reader.GetString(2);
            user.Email=reader.GetString(3);
            user.Username=reader.GetString(4);
            user.Password=reader.GetString(5);
            user.Address=reader.GetString(6);
            user.Role =reader.GetInt32(7);
            user.Gender =reader.GetString(8);
            user.Date_=reader.GetString(9);
            user.Time_ =reader.GetString(10);
            user.PhoneNumber =reader.GetString(11);
            Console.WriteLine(user);
            users.Add(user);
        }
        requestResponse=new RequestResponse{
            Message="success",
            Result= users,
            Code="200"
        };
        return Ok(requestResponse);

    }
    // ::::::::::::::::::::::: get a book information
    [Authorize(Role.Admin)]
    [HttpGet("user")]
    public IActionResult GetUser(int Id)
    {
        Console.WriteLine("get user");
        // :::::::::::::::: Create a list of books
        MySqlCommand command;
        List<dynamic> users=new List<dynamic>();
        command  = new MySqlCommand(
            "SELECT * FROM users WHERE users.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        //using var command = new MySqlCommand(query,this. connection);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: Create a book Object to hold db data
            var user=new User();
            user.Id = reader.GetInt32(0);
            user.UserOfficialId = reader.GetString(1);
            user.Name =reader.GetString(2);
            user.Email=reader.GetString(3);
            user.Username=reader.GetString(4);
            user.Password=reader.GetString(5);
            user.Address=reader.GetString(6);
            user.Role =reader.GetInt32(7);
            user.Gender =reader.GetString(8);
            user.Date_=reader.GetString(9);
            user.Time_ =reader.GetString(10);
            user.PhoneNumber =reader.GetString(11);
            Console.WriteLine(user);
            users.Add(user);
            //return notification;
            requestResponse=new RequestResponse{
                Message="success",
                Result= users,
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failure, No data received",
                Result= users,
                Code="200"
            };
            return Ok(requestResponse);
        }
    
    }
    // ::::::::::::::::::::: add book information
    [HttpPost("register-user")]
    public IActionResult AddUser(User user)
    {
        // ::::::::::::::::: check existing usernames
        UserExist userExist=new UserExist();
        if(!userExist.checkUserExist(user.Username)){
            // :::::::::::::::::
            Console.WriteLine("register user");
            var queryStatement = "INSERT INTO users( \n"+
            "ID, USER_OFFICIAL_ID, NAME,EMAIL, USERNAME,\n"+
            "PASSWORD,ADDRESS,ROLE,GENDER,\n"+
            "DATE_,TIME_,PHONE_NUMBER \n"+
            ") VALUES(@Id,@UserOfficialId,@Name,@Email,@Username,@Password,@Address,@Role,@Gender,@Date_,@Time_,@PhoneNumber)";
            // :::::::::::
            user.UserOfficialId="";
            var hash=new hashPassword();
            user.Password= hash.hashUserPassword(user.Password);
            // :::::::::::
            MySqlCommand command;
            command = new MySqlCommand(queryStatement,this.connection);
            command.Prepare();
            command.Parameters.AddWithValue("@Id", null);
            command.Parameters.AddWithValue("@UserOfficialId", user.UserOfficialId);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Username", user.Username);
            command.Parameters.AddWithValue("@Password", user.Password);
            command.Parameters.AddWithValue("@Address", user.Address);
            command.Parameters.AddWithValue("@Role", user.Role);
            command.Parameters.AddWithValue("@Gender", user.Gender);
            command.Parameters.AddWithValue("@Date_", user.Date_);
            command.Parameters.AddWithValue("@Time_", user.Time_);
            command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
            command.ExecuteNonQuery();
            // :::::::::::::::: 
            return Ok(new User());
        }
        return Ok(new User());
 
    }

    // :::::::::::::::::: admin delete notification
    [Authorize(Role.Admin)]
    [HttpDelete("admin-delete-user/{Id}")]
    public IActionResult AdminDeleteCustomer(int Id)
    {
        Console.WriteLine("delete user");
        // :::::::::::::::: delete a user from db
        MySqlCommand command;
        List<dynamic> users=new List<dynamic>();
        command  = new MySqlCommand(
            "DELETE FROM users WHERE users.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        if (command.ExecuteNonQuery()){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success",
                Result= users,
                Code="200"
            };
            return Ok(requestResponse);  
        }else{
            requestResponse=new RequestResponse{
                Message="failure, Could not delete",
                Result= users,
                Code="200"
            };
            return Ok(requestResponse);
        }
    }
}
