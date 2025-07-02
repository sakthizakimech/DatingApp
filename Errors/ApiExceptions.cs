using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.SignalR;

namespace DatingApp.Errors;

public class ApiExceptions
{
    public int StatusCode { get;set;}
    public string Message{ get;set;}
    public string? Details{ get;set;}
    public ApiExceptions(int statusCode, string message, string? details)
    {
        StatusCode = statusCode;
        Message = message;
        Details = details;

    }
}
