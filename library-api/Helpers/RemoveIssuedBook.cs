namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class RemoveIssuedBook
{
    public  MySqlConnection  connection;
    public RemoveIssuedBook()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
    }
    public bool removeIssued(int Id){
        Console.WriteLine("remove issued book: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        var book = new book();
        command  = new MySqlCommand(
            "DELETE FROM issued_books WHERE issued_books.BOOK_ID=@Id",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (command.ExecuteNonQuery()>0){
            // :::::::::::::: Create a book Object to hold db data
            requestResponse=new RequestResponse{
                Message="success, book is now Available",
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