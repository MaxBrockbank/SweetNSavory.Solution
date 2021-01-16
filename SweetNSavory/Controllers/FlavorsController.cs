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
  public class FlavorsController:Controller
  {
    private readonly SweetNSavoryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public FlavorsController(SweetNSavoryContext db, UserManager<ApplicationUser> userManager)
    {
      _db = db;
      _userManager = userManager;
    }

    //Non-Authorized Routes

    public ActionResult Index()
    {
      return View(_db.Flavors.ToList());
    }

    public ActionResult Details(int id)
    {
      var thisFlavor = _db.Flavors
        .Include(flavor=>flavor.Treats)
        .ThenInclude(join=>join.Treat)
        .FirstOrDefault(flavor=>flavor.FlavorId == id);
      return View(thisFlavor);
    }

    //Authorized Routes

    [Authorize]
    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Flavor flavor)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(flavor => flavor.FlavorId == id);
      if(thisFlavor == null)
      {
        return RedirectToAction("Details", new{id=id});
      }
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor )
    {
      if(_db.Entry(flavor).State == EntityState.Modified)
      {
        _db.Entry(flavor).State = EntityState.Modified;
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(flavor=>flavor.FlavorId==id);
      if(thisFlavor == null)
      {
        return RedirectToAction("Details", new{id=id});
      }
      return View(thisFlavor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisFlavor = _db.Flavors.FirstOrDefault(flavor => flavor.FlavorId == id);
      _db.Flavors.Remove(thisFlavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public async Task<ActionResult> AddTreat(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(flavor=>flavor.FlavorId == id);
      if(thisFlavor == null)
      {
        return RedirectToAction("Details", new{id=id});
      }
      ViewBag.TreatId = new SelectList(_db.Treats, "TreatId", "Name");
      ViewBag.Treats = _db.Treats.ToList();
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult AddTreat(Flavor flavor, int TreatId)
    {
      if(TreatId != 0)
      {
        var relationship = _db.FlavorTreat
          .Any(entry=>entry.FlavorId == flavor.FlavorId && entry.TreatId == TreatId);
        if(!relationship)
        {
          _db.FlavorTreat.Add(new FlavorTreat(){FlavorId = flavor.FlavorId, TreatId = TreatId});
        }
      }
      _db.SaveChanges();
      return RedirectToAction("Details", new{id=flavor.FlavorId});
    }

    [HttpPost]
    public ActionResult DeleteTreat(int joinId, int FlavorId)
    {
      var relationship = _db.FlavorTreat.FirstOrDefault(entry=>entry.FlavorTreatId == joinId);
      _db.FlavorTreat.Remove(relationship);
      _db.SaveChanges();
      return RedirectToAction("Details", new {id=FlavorId});
    }
  }
}