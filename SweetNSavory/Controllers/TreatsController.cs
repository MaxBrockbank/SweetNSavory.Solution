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

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Treat treat, int FlavorId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      if(FlavorId != 0)
      {
        var relationship = _db.FlavorTreat
          .Any(entry=>entry.TreatId == treat.TreatId && entry.FlavorId == FlavorId);
        if(!relationship)
        {
          _db.FlavorTreat.Add(new FlavorTreat(){FlavorId = FlavorId, TreatId = treat.TreatId});
        }
      }
      _db.Treats.Add(treat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }

}