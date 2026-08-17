namespace ClientHub.Models.Entities
{
  public class UserModel
  {
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }

    public DateTime CreatedAt { get; set; }
  }
}