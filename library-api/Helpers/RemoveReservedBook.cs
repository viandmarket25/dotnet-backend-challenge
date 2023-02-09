namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using MySql.Data.MySqlClient;
using library_api.Services;
using library_api.Config;
public class RemoveReservedBook
{
    public  MySqlConnection  connection;
    public RemoveReservedBook()
    {
        var mysqlConnection=new MysqlConnectionPipe();
        mysqlConnection.InitMysqlConnectionPipe ();
        this. connection = mysqlConnection.GetMysqlConnectionPipe();
    }
    public bool removeReserved(int Id){
        Console.WriteLine("remove reserved book: "+Id);
        MySqlCommand command;
        List<dynamic> books=new List<dynamic>();
        command = new MySqlCommand(
            "DELETE FROM reserved_books WHERE reserved_books.BOOK_ID=@Id",this.connection);
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