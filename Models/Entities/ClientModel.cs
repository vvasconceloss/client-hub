namespace ClientHub.Models.Entities
{
  public class ClientModel
  {
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string LastName { get; set; }
    public required string FirstName { get; set; }
    public string? Address { get; set; }

    public int PostalCodeId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
  }
}