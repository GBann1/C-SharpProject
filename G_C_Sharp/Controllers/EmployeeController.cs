using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using G_C_Sharp.Models;

namespace G_C_Sharp.Controllers;

public class EmployeeController : Controller
{
    private readonly ILogger<EmployeeController> _logger;
    private MyContext _context;

    public EmployeeController(ILogger<EmployeeController> logger, MyContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("admin")]
    public IActionResult AdminLanding()
    {
        return View();
    }


    [HttpPost("admin/login")]
    public IActionResult Login(Employee userLogin)
    {
        if(ModelState.IsValid)
        {
            Employee? userInDB = _context.Employees.SingleOrDefault(user => user.IdentNum == userLogin.IdentNum);
            if(userInDB == null)
            {
                ModelState.AddModelError("IdentNum","Invalid ID or Password");
                return View("AdminLanding");
            }
            PasswordHasher<Employee> hasher = new PasswordHasher<Employee>();
            var result = hasher.VerifyHashedPassword(userLogin, userInDB.Password, userLogin.Password);
            if(result == 0)
            {
                ModelState.AddModelError("Password", "Invalid ID or Password");
                return View("AdminLanding");
            }
            // Add user on login
            HttpContext.Session.SetInt32("UserID", userInDB.EmployeeID);
            if(userInDB.Name != null)
            {
                HttpContext.Session.SetString("E_Name", userInDB.Name);
            }else{
                HttpContext.Session.SetString("E_Name", "N/A");
            }
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