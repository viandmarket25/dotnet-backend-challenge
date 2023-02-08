namespace library_api.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using library_api.Services;

public class hashPassword
{

    public hashPassword()
    {
        
    }
    public string hashUserPassword(string Password){
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(Password);
        return passwordHash;
    }
    public bool verifyUserPassword(string RequestPassword,string DbPassword){
        bool verified = BCrypt.Net.BCrypt.Verify(RequestPassword, DbPassword);
        return verified;
    }

}