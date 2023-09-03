using System.Collections.Generic;
using System.Threading.Tasks;
using CarRentalACC.Data;
using Microsoft.AspNetCore.Identity;

public interface IUserService
{
    Task<IEnumerable<ApplicationUser>> GetUsersAsync();
    Task<ApplicationUser> GetUserAsync(string userId);
    Task<bool> EditUser(string userId, string role, string email, string phoneNumber, string firstName, string lastName);
    Task<bool> DeleteUserAsync(String userId);
    Task<bool> IsUserInAdministratorsRoleAsync(string userId);

}
