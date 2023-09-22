using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using G_C_Sharp.Models;

namespace G_C_Sharp.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private MyContext _context;

    public UserController(ILogger<UserController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("")]
    public IActionResult Landing()
    {
        return View();
    }

    [HttpPost("user/write")]
    // Register partial
    public IActionResult WriteUser(User newUser)
    {
        if(ModelState.IsValid)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            newUser.Password = hasher.HashPassword(newUser, newUser.Password);
            _context.Add(newUser);
            _context.SaveChanges();
            // write user into session on registration
            HttpContext.Session.SetInt32("UserID", newUser.UserID);
            HttpContext.Session.SetString("UserFirstName", newUser.Username);
                                    // ! Route from this controller
            return RedirectToAction("Dashboard", "Home");
        }else{
            return View("Landing");
        }
    }

    [HttpPost("user/login")]
    public IActionResult Login(Login userLogin)
    {
        if(ModelState.IsValid)
        {
            User? userInDB = _context.Users.SingleOrDefault(user => user.Username == userLogin.LogUsername);
            if(userInDB == null)
            {
                ModelState.AddModelError("LogUsername","Invalid Email or Password");
                return View("Landing");
            }
            PasswordHasher<Login> hasher = new PasswordHasher<Login>();
            var result = hasher.VerifyHashedPassword(userLogin, userInDB.Password, userLogin.LogPassword);
            if(result == 0)
            {
                ModelState.AddModelError("LogPassword", "Invalid Email or Password");
                return View("Landing");
            }
            // Add user on login
            HttpContext.Session.SetInt32("UserID", userInDB.UserID);
            HttpContext.Session.SetString("UserFirstName", userInDB.Username);
                                // ! Route from this controller
            return RedirectToAction("Dashboard", "Home");
        }else{
            return View("Landing");
        }
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Landing");
    }
}