using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Indentity;
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

    public ActionResult Detials(int id)
    {
      var thisFlavor = _db.Flavors
        .Include(flavor=>flavor.Treats)
        .ThenInclude(join=>join.Treat);
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
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      flavor.User = currentUser;
      _db.Flavors.Add(flavor);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [Authorize]
    public ActionResult Edit(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(flavor => flavor.FlavorId == id);
      if(thisFlavor == null)
      {
        return RedirectToAction("Details", new{id=id})
      }
      return View(thisFlavor);
    }

    [HttpPost]
    public ActionResult Edit(Flavor flavor )
    {
      _db.Entry(flavor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectoAction("Index")
    }

    [Authorized]
    public async Task<ActionResult> Delete(int id)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var thisFlavor = _db.Flavors.Where(entry=>entry.User.Id == currentUser.Id).FirstOrDefault(flavor=>flavor.FlavorId==id);
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
  }
}