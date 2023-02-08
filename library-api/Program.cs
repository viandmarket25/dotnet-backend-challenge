using System;
using System.Data;
using MySql.Data.MySqlClient;
using Microsoft.IdentityModel.Tokens;
using library_api.Helpers;
using library_api.Services;
using System.Text.Json.Serialization;
// :::::::::::::::::::::::
//var builder = WebApplication.CreateBuilder(args);
var webApplicationOptions = new WebApplicationOptions
{
    ContentRootPath = AppContext.BaseDirectory,
    Args = args,
};    

var builder = WebApplication.CreateBuilder(webApplicationOptions);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Configure the HTTP request pipeline.
{
    builder.Services.AddCors();
    builder.Services.AddControllers().AddJsonOptions(x =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
    builder.Services.AddScoped<IUserService, UserService>();
}
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.MapGet("/", () => "Hello World!");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
// configure HTTP request pipeline
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.MapControllers();
app.Run();
//app.Run("http://localhost:4000");