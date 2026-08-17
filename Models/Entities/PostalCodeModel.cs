namespace ClientHub.Models.Entities
{
  public class PostalCode
  {
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string City { get; set; }

    public ICollection<Client> Clients { get; set; } = [];
  }
}