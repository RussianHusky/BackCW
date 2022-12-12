using System.Diagnostics;
using BackCW.Data;
using BackCW.Migrations;
using Microsoft.AspNetCore.Mvc;
using BackCW.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql.Replication.TestDecoding;

namespace BackCW.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ApplicationDbContext dbContext;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
    {
        this.logger = logger; 
        this.dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var christmas = new DateTime(2022, 12, 31);
        var timeLeft = christmas - DateTime.UtcNow;
        var countdown = ToReadableString(timeLeft);

        bool countEnd = christmas < DateTime.Now;

        string ToReadableString(TimeSpan span)
        {
            string formatted = string.Format("{0}{1}{2}{3}",
                span.Duration().Days > 0 ? string.Format("{0:0} day{1}, ", span.Days, span.Days == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Hours > 0 ? string.Format("{0:0} hour{1}, ", span.Hours, span.Hours == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Minutes > 0 ? string.Format("{0:0} minute{1}, ", span.Minutes, span.Minutes == 1 ? string.Empty : "s") : string.Empty,
                span.Duration().Seconds > 0 ? string.Format("{0:0} second{1}", span.Seconds, span.Seconds == 1 ? string.Empty : "s") : string.Empty);

            if (formatted.EndsWith(", ")) formatted = formatted.Substring(0, formatted.Length - 2);

            if (string.IsNullOrEmpty(formatted)) formatted = "0 seconds";

            return formatted;
        }
        
        return View(new IndexViewModel{Countdown = countdown, User = GetLoggedInUser(), CountEnd = countEnd});
    }

    public IActionResult Privacy()
    {
        return View(new BaseViewModel{User = GetLoggedInUser()});
    }

    public IActionResult SignUp()
    {
        return View(new BaseViewModel{User = GetLoggedInUser()});
    }
    
    
    public IActionResult SignIn()
    {
        return View(new BaseViewModel{User = GetLoggedInUser()});
    }
    
    [HttpPost]
    public IActionResult LoggedIn(string email, string password)
    {
        var user = dbContext.SecretSantas.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            return BadRequest("Invalid user");
        }
        else
        {
            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                user.Token = Guid.NewGuid().ToString();
                dbContext.SaveChanges();
                Response.Cookies.Append("token", user.Token);
                return RedirectToAction("Index", "Home");
            }
            return BadRequest("Wrong password");
        }
    }

    [HttpPost]
    public IActionResult SignedUp(string name, string email, string password, string likes)
    {
        if (dbContext.SecretSantas.Any(s => s.Email == email))
        {
            return BadRequest("You already submitted a secret santa thing");
        }
        
        var hashed = BCrypt.Net.BCrypt.HashPassword(password);

        dbContext.SecretSantas.Add(new SecretSanta
        {
            Name = name,
            Likes = likes,
            Email = email,
            PasswordHash = hashed,
        }); 

        dbContext.SaveChanges();

        return LoggedIn(email, password);
    }

    public IActionResult LogOut()
    {
        Response.Cookies.Delete("token");
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult GeneratePartner()
    {
        bool exist = false;
        var users = dbContext.SecretSantas
            .Include(s => s.Partner).ToList();
        foreach (var user in users)
        {
            if(user == GetLoggedInUser())
                continue;
            foreach (var allUser in users)
            {
                if (user.Id == allUser.Partner?.Id)
                {
                    exist = true;
                    break;
                }
            }

            if (exist == false)
            {
                GetLoggedInUser().Partner = user;
                break;
            }
        }
        
        dbContext.SaveChanges();
        
        return RedirectToAction("Index", "Home");
    }
    
    private SecretSanta? GetLoggedInUser()
    {
        if (Request.Cookies["token"] == null)
            return null;

        var token = Request.Cookies["token"];
        return dbContext.SecretSantas.Include(s => s.Partner).FirstOrDefault(u => u.Token == token);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, User = GetLoggedInUser()});
    }

}