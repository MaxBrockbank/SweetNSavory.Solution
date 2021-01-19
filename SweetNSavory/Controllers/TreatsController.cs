using Microsoft.AspNetCore.Mvc;
using SweetNSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
      treat.User = currentUser;
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

    [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(treat=>treat.TreatId == id);
      if(thisTreat == null)
      {
        return RedirectToAction("Details", new{id=id});
      }
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult Edit(Treat treat, int TreatId)
    {
      if(treat != null)
      {
        _db.Entry(treat).State = EntityState.Modified;
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new {id=TreatId});
    }

    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(treat=>treat.TreatId == id);
      if(thisTreat == null)
      {
        return RedirectToAction("Details", new{id=id});
      }
      return View(thisTreat);
    }
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisTreat = _db.Treats.FirstOrDefault(treat=>treat.TreatId == id);
      _db.Treats.Remove(thisTreat);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> AddFlavor(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisTreat = _db.Treats.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(treat=>treat.TreatId == id);
        if(thisTreat == null)
        {
          return RedirectToAction("Details", new{id=id});
        }
      ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
      ViewBag.Flavors= _db.Flavors.ToList();
      return View(thisTreat);
    }

    [HttpPost]
    public ActionResult AddFlavor(Treat treat, int FlavorId)
    {
      if(FlavorId != 0)
      {
        var relationship = _db.FlavorTreat
          .Any(entry=>entry.TreatId == treat.TreatId && entry.FlavorId == FlavorId);
        if(!relationship)
        {
          _db.FlavorTreat.Add(new FlavorTreat(){TreatId = treat.TreatId, FlavorId = FlavorId});
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new{id=treat.TreatId});
    }

    [HttpPost]
    public ActionResult DeleteFlavor(int joinId, int TreatId)
    {
      var relationship = _db.FlavorTreat.FirstOrDefault(entry=>entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(relationship);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}