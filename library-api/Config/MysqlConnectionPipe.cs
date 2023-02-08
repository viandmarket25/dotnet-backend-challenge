
using System;
using System.Data;
using MySql.Data.MySqlClient;
namespace library_api.Config;

public class MysqlConnectionPipe : IDisposable
{
  public string connstring;
  public MySqlConnection connection;
  public string InitMysqlConnectionPipe ()
  {
    this.connstring = string.Format("Server=localhost; database={0}; UID=root; password=123456; SslMode = none", "dotnet_library");
    this.connection = new MySqlConnection(this.connstring);
    this.connection.Open(); 
    return "";
  }
  public MySqlConnection GetMysqlConnectionPipe ()
  {
    return this.connection; 
  }
  public void Dispose()
  {
    this.connection.Close();
  }
}




/*
// execute the command and read the results
using var reader = await command.ExecuteReaderAsync();
while (reader.Read())
{
	var id = reader.GetInt32("order_id");
	var date = reader.GetDateTime("order_date");
	// ...
}
*/