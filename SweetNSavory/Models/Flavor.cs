using System.Collections.Generic;

namespace SweetNSavory.Models
{
  public class Flavor()
  {
    public Flavor()
    {
      this.Treats = new HashSet<FlavorTreat>;
    }

    public string Name {get; set;}
    public int FlavorId {get; set;}
    public ApplicationUser User {get; set;}
    public ICollection <FlavorTreat> Treats {get; set;}
  }
}