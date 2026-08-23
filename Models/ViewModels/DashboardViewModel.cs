namespace ClientHub.Models.ViewModels
{
  public class DashboardViewModel
  {
    public int TotalClients { get; set; }
    public int ClientsThisMonth { get; set; }
    public int MyClients { get; set; }
    public List<ClientListItemViewModel> RecentClients { get; set; } = [];
  }
}