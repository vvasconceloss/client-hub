using System.ComponentModel.DataAnnotations;

namespace ClientHub.Models.ViewModels
{
  public class RegisterViewModel
  {
    [Required(ErrorMessage = "The email is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [MaxLength(256)]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "The password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "The password must be at least {2} characters long.")]
    public string Password { get; set; } = "";

    [Required(ErrorMessage = "Confirm your password.")]
    [Compare(nameof(Password), ErrorMessage = "The passwords do not match.")]
    public string ConfirmPassword { get; set; } = "";
  }
}