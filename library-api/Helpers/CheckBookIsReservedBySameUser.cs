namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Models;
using library_api.Services;
using library_api.Config;
public class BookIsReservedBySameUser
{
    public  MySqlConnection  connection;
    public BookIsReservedBySameUser()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        
    }
    public bool checkBookIsReservedBySameUser(IssueReserveBook issueReserveBook){
        Console.WriteLine("check book reserved: "+  issueReserveBook issueReserveBook.BookId);
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE books.BOOK_ID=@Id and books.IS_RESERVED=1 and books.RESERVED_FOR=@UserId ",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", issueReserveBook.BookId);
        command.Parameters.AddWithValue("@UserId", issueReserveBook.UserId);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: 
            return true;      
        }
        return false;
    }


}