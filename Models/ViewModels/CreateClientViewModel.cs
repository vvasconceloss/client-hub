using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientHub.Models.ViewModels
{
  public class CreateClientViewModel
  {
    [Required(ErrorMessage = "The first name is required.")]
    [MaxLength(100)]
    public string FirstName { get; set; } = "";

    [Required(ErrorMessage = "The last name is required.")]
    [MaxLength(100)]
    public string LastName { get; set; } = "";

    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [MaxLength(256)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "The phone is required.")]
    [MaxLength(30)]
    public string Phone { get; set; } = "";

    [MaxLength(200)]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Select a postal code.")]
    public int? PostalCodeId { get; set; }

    public List<SelectListItem> PostalCodeOptions { get; set; } = [];
  }
}