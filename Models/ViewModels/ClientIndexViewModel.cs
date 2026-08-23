using Microsoft.AspNetCore.Mvc.Rendering;

namespace ClientHub.Models.ViewModels
{
  public class ClientIndexViewModel
  {
    public List<ClientListItemViewModel> Clients { get; set; } = [];
    public string? Search { get; set; }
    public int? PostalCodeId { get; set; }
    public string? SortBy { get; set; }
    public string? SortDir { get; set; }
    public List<SelectListItem> PostalCodes { get; set; } = [];
    public int Page { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage { get; set; }
    public bool HasNextPage { get; set; }
  }
}