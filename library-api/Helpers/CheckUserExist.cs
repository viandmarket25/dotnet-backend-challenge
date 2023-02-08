namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class UserExist
{
    public  MySqlConnection  connection;
    public UserExist()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        
    }
    public bool checkUserExist(string Username){
        Console.WriteLine("get user by Username: "+Username);
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM users WHERE users.USERNAME=@Username",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Username);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: 
            return true;      
        }
        return false;
    }


}