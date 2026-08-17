namespace ClientHub.Models.Entities
{
  public class PostalCodeModel
  {
    public int Id { get; set; }
    public required string Code { get; set; }
    public required string City { get; set; }

    public ICollection<ClientModel> Clients { get; set; } = [];
  }
}