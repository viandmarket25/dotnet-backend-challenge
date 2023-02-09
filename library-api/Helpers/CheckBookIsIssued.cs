namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class BookIsIssued
{
    public  MySqlConnection  connection;
    public BookIsIssued()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
        
    }
    public bool checkBookIsIssued(int Id){
        Console.WriteLine("check book issued: "+Id);
        MySqlCommand command;
        command  = new MySqlCommand(
            "SELECT * FROM books WHERE books.BOOK_ID=@Id and books.IS_AVAILABLE=1 ",this.connection);
        command.Prepare();
        command.Parameters.AddWithValue("@Id", Id);
        using var reader = command.ExecuteReader();
        if (reader.Read()){
            // :::::::::::::: 
            return true;      
        }
        return false;
    }


}