using Microsoft.AspNetCore.Mvc;
using SweetNSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetNSavory.Controllers
{
  public class TreatsController:Controller
  {
    private readonly SweetNSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public TreatsController (SweetNSavoryContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }
    
    //Non-Authorized Routes
    public ActionResult Index()
    {
      return View(_db.Treats.ToList());
    }

    public ActionResult Details(int id)
    {
      var thisTreat = _db.Treats
        .Include(treat => treat.Flavors)
        .ThenInclude(join=> join.Flavor)
        .FirstOrDefault(treat=>treat.TreatId == id);
      return View(thisTreat);
    }
    //Authorized Routes
  }

}