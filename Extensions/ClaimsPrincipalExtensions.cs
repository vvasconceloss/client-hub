using System.Security.Claims;

namespace ClientHub.Extensions
{
  public static class ClaimsPrincipalExtensions
  {
    public static Guid GetUserId(this ClaimsPrincipal user)
    {
      return Guid.Parse(user.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
  }
}