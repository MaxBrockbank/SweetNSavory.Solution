using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Indentity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetNSavory.Controllers
{
  public class HomeController:Controller
  {
    private readonly SweetNSavoryContext _db;

    public HomeController(SweetNSavoryContext db)
    {
      _db = db;
    }

    [HttpGet("/")]
    public ActionResult Index()
    {
      
    }
  }
}