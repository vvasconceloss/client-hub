namespace ClientHub.Models.ViewModels
{
  public class ClientListItemViewModel
  {
    public int Id { get; set; }
    public required string FullName { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }
    public required string PostalCode { get; set; }
  }
}
