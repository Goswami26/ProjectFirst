using System.Data;
using Microsoft.EntityFrameworkCore;
using ProjectFirst.Models;

namespace ProjectFirst.Data
{
    public class DbSeeder
    {

        public static async Task SeedAsync(AppDbContext _db)
        {
            await _db.Database.MigrateAsync();

            //seed roles
            if(!_db.Roles.Any()) 
            {
                var roles = new List<Role> {
                 
                      new Role { RoleName = "Admin" },
                      new Role { RoleName = "Devloper" },
                      new Role { RoleName = "Manager" },

                };

                await _db.AddRangeAsync(roles);
                await _db.SaveChangesAsync();
            }

            //var admin = await _db.Users
            //  .FirstOrDefaultAsync(x => x.Username == "Admin");

            //if (admin != null)
            //{
            //    admin.MobileNo = "8961236547";

            //    await _db.SaveChangesAsync();
            //}

            //seed Users
            if (!_db.Users.Any()) 
            {
                var admin = new User
                {
                    Username = "Admin",
                    Useremail = "Admin@system.com",
                    MobileNo = "8961236547"
                };

                await _db.AddAsync(admin);

                var adminrole = await _db.Roles.FirstOrDefaultAsync(role => role.RoleName == "Admin");

                var newrole = new UserRole
                {
                    user = admin,
                    Role = adminrole
                };

                await _db.userRoles.AddAsync(newrole);
                await _db.SaveChangesAsync();
            }

            
        }
    }
}
