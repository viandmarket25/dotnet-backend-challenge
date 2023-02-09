namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class SetBookReserved
{
    public  MySqlConnection  connection;
    public SetBookReserved()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
    }
    public bool setReserved(int Id){
        Console.WriteLine("set book Reserved: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        var book = new book();
        command  = new MySqlCommand(
            "UPDATE books SET books.IS_RESERVED=1 WHERE books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success, book is now Reserved",
                Result= books,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse); 
        }else{
            requestResponse=new RequestResponse{
                Message="failed, Could not update",
                Result= books,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
    }
    public bool setUnReserved(int Id){
        Console.WriteLine("set book UnReserved: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        var book = new book();
        command  = new MySqlCommand(
            "UPDATE books SET books.IS_RESERVED=0 WHERE books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success, book is now UnReserved",
                Result= books,
                Status="success",
                Code="200"
            };
            return Ok(requestResponse); 
        }else{
            requestResponse=new RequestResponse{
                Message="failed, Could not update",
                Result= books,
                Status="failed",
                Code="200"
            };
            return Ok(requestResponse);
        }
    }
}