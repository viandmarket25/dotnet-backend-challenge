
namespace library_api.Models;
public class RequestResponse
{
    public string Message { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
    public List<dynamic> Result{ get; set; }
}