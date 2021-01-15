using System.ComponentModel.DataAnnotations;

namespace ToDoList.ViewModels
{
  public class RegisterViewModel
  {
    [Required]
    [EmailAddress]
    [Display(Name="Email")]
    public string Email {get;set;}

    [Required]
    [DataType(DataType.Password)]
    [DisplayName(Name="Password")]
    public string Password {get;set;}

    [DataType(DataType.Password)]
    [DisplayName(Name="Confirm Password")]
    [Compare(Password, ErrorMessage="Passwords do not match. Try again.")]
    public string ConfirmPassword {get;set;}
  }
}