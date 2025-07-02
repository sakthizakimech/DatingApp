using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace DatingApp.Controllers;

public class BuggyController : BaseApiController
{
    
[HttpGet("throw")]
public IActionResult Throw()
{
    throw new Exception("Test exception middleware");
}
}
