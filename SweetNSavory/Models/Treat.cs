using System.Collections.Generic;

namespace SweetNSavory.Models
{
  public class Treat
  {
    public Treat()
    {
      this.Flavors = new HashSet <FlavorTreat>();
    }

    public string Name {get; set;}
    public int TreatId {get; set;}
    public ApplicationUser User {get;set;}
    public ICollection <FlavorTreat> Flavors {get; set;} 
  }
}