using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SimpleDotNetWebApiApp.Domain.Entities;
using SimpleDotNetWebApiApp.Infrastructure.Data;
using System.Security.Claims;

namespace SimpleDotNetWebApiApp.Authorization
{
    public class PermissionBasedAuthorizationFilter(GeneralAppDbContext dbContext) : IAsyncAuthorizationFilter
    {
        async Task IAsyncAuthorizationFilter.OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var attribute = (CheckPermissionAttribute)context.ActionDescriptor.EndpointMetadata.FirstOrDefault(o => o is CheckPermissionAttribute);
            if (attribute != null)
            {
                var claimIdentity = context.HttpContext.User.Identity as ClaimsIdentity;
                if (claimIdentity == null || !claimIdentity.IsAuthenticated)
                    context.Result = new ForbidResult();
                else
                {
                    var userId = int.Parse(claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    var hasPermission = await dbContext.Set<UserPermission>().AnyAsync(o => o.UserId == userId && o.PermissionId == attribute.Permission);

                    if (!hasPermission)
                        context.Result = new ForbidResult();
                }
            }
        }
    }
}
