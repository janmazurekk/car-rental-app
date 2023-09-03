using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalACC.Data;
using Microsoft.AspNetCore.Identity;

public class UsersService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UsersService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<ApplicationUser>> GetUsersAsync()
    {
        return _userManager.Users.Select(x => new ApplicationUser
        {
            Id = x.Id,
            UserName = x.UserName,
            Email = x.Email,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            TwoFactorEnabled = x.TwoFactorEnabled,
        }); ;
    }

    public async Task<ApplicationUser> GetUserAsync(string userId)
    {
        return await _userManager.FindByIdAsync(userId);
    }

    public async Task<bool> EditUser(string userId, string role, string email, string phoneNumber, string firstName, string lastName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        user.Email = email;
        user.PhoneNumber = phoneNumber;
        user.FirstName = firstName;
        user.LastName = lastName;

        var currentRoles = await _userManager.GetRolesAsync(user);
        await _userManager.RemoveFromRolesAsync(user, currentRoles);

        if (!string.IsNullOrEmpty(role))
        {
            if(role == "Użytkownik")
            {
                await _userManager.RemoveFromRoleAsync(user, "Administrators");
            }
            else
            {
                await _userManager.AddToRoleAsync(user, "Administrators");
            }
        }

        return true;
    }

    public async Task<bool> DeleteUserAsync(String userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var deleteResult = await _userManager.DeleteAsync(user);

        return deleteResult.Succeeded;
    }

    public async Task<bool> IsUserInAdministratorsRoleAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false;
        }

        var isAdmin = await _userManager.IsInRoleAsync(user, "Administrators");
        return isAdmin;
    }

}
