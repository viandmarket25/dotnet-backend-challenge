namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class SetBookAvailable
{
    public  MySqlConnection  connection;
    public SetBookAvailable()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
    }
    public bool setAvailable(int Id){
        Console.WriteLine("set book available: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        command  = new MySqlCommand(
            "UPDATE books SET books.IS_AVAILABLE=1 WHERE books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
        
            return true; 
        }else{
            
            return false;
        }
    }
    public bool setUnAvailable(int Id){
        Console.WriteLine("set book unavailable: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        command  = new MySqlCommand(
            "UPDATE books SET books.IS_AVAILABLE=0 WHERE books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            return true; 
        }else{
            return false;
        }
    }
}