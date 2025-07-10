using AuthECAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace AuthECAPI.Controllers
{
    public static class AccountEndpoints
    {
        public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("/userprofile", GetUserProfile);
            return app;
        }

        [Authorize]
        private static async Task<IResult> GetUserProfile(
          ClaimsPrincipal user,
          UserManager<AppUser> userManager)
        {
            var userIDClaim = user.Claims.FirstOrDefault(static x => x.Type == "userID");
            if (userIDClaim == null)
            {
                return Results.BadRequest(new { Error = "User ID claim is missing." });
            }

            string userID = userIDClaim.Value;
            var userDetails = await userManager.FindByIdAsync(userID);
            if (userDetails == null)
            {
                return Results.NotFound(new { Error = "User not found." });
            }

            return Results.Ok(
              new
              {
                  Email = userDetails.Email,
                  FullName = userDetails.FullName,
              });
        }
    }
}
