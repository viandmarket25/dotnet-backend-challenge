namespace library_api.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using library_api.Entities;
using library_api.Helpers;
using library_api.Models;
using library_api.Config;
using System;
using System.Data;
using MySql.Data.MySqlClient;

// :::::::::::::::
public interface IUserService
{
    AuthenticateResponse Authenticate(AuthenticateRequest model);
    UserAuth GetById(int id);
}
public class UserService : IUserService
{
    public  MySqlConnection  connection;
    private readonly AppSettings _appSettings;
    public UserAuth DbUser;
    public UserService(IOptions<AppSettings> appSettings)
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        _appSettings = appSettings.Value;
    }

    private UserAuth userAuth (string Username)
    {
        Console.WriteLine("get user");
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM users WHERE  users.USERNAME=@Username",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Username", Username);
        using var reader = command.ExecuteReader();
        var role=Role.User;
        if (reader.Read()){
            // :::::::::::::: 
            if(reader.GetInt32(7)==20){
                role=Role.Admin;
            }else if(reader.GetInt32(7)==10){
                role=Role.User;
            }
            DbUser= new UserAuth {
                Id = reader.GetInt32(0),
                Name = reader.GetString(2), 
                UserOfficialId  = reader.GetString(1),
                Username = reader.GetString(3),
                Email =reader.GetString(4),
                Password = reader.GetString(5),
                Role=role
            };            
            return DbUser;
        }
        return new UserAuth();
    }
  
    public AuthenticateResponse Authenticate(AuthenticateRequest requestModel)
    {
        // :::::::::::::: get user from database using Username
        // :::::::::::::: compare user login information
        var user= userAuth(requestModel.Username);
        // ::::::::::::: password hash check
        Console.WriteLine("found user: "+user );
        var hash=new hashPassword();
        if(!hash.verifyUserPassword(requestModel.Password,user.Password) || user == null){
            return null;
        }
        // authentication successful so generate jwt token
        var token = generateJwtToken(user);
        return new AuthenticateResponse(user, token);
    }

    public UserAuth GetById(int Id)
    {
        Console.WriteLine("get user by id: "+Id);
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM users WHERE users.ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        var role=Role.User;
        if (reader.Read()){
            // :::::::::::::: 
            if(reader.GetInt32(7)==20){
                role=Role.Admin;
            }else if(reader.GetInt32(7)==10){
                role=Role.User;
            }
            return new UserAuth {
                Id = reader.GetInt32(0),
                Name = reader.GetString(2), 
                UserOfficialId  = reader.GetString(1),
                Username = reader.GetString(3),
                Email =reader.GetString(4),
                Password = reader.GetString(5),
                Role=role
            };            
        }
        return new UserAuth();
    }
    // helper methods
    private string generateJwtToken(UserAuth user)
    {
        // generate token that is valid for 7 days
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                new[] {
                    new Claim("Id", user.Id.ToString()),            
                 }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}