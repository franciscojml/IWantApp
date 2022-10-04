using Microsoft.AspNetCore.Identity;

namespace IWantApp.Endpoints.Employees;

public class EmployeePost
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(EmployeeRequest request, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser { UserName = request.Email, Email = request.Email};
        var result = userManager.CreateAsync(user).Result;

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.First());
        
        return Results.Created($"/employees/{user.Id}", user.Id);
    }
}
