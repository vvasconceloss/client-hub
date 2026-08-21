namespace ClientHub.Models.ViewModels
{
  public class ClientDetailsViewModel
  {
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public string? Address { get; set; }
    public required string PostalCode { get; set; }
    public required string City { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}
