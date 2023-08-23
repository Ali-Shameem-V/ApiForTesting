using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForTesting.Service
{
    public class AppUserRepository : IAppUser
    {
        private readonly ApiDbContext apiDbContext;

        public AppUserRepository(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }

        public async Task<appuser> AddAppUser(appuser appuser)
        {
            var usertype = await apiDbContext.usertypes_shameem.FindAsync(appuser.UserTypeId);

               if(usertype != null) 
               {
                await apiDbContext.appusers_shameem.AddAsync(appuser);
                await apiDbContext.SaveChangesAsync();
                return appuser;
                }
           
                return null;    
                          
                  
        }
        public async Task<IEnumerable<appuser>> GetAllAppUser()
        {
            var appusers= await apiDbContext.appusers_shameem.ToListAsync();
            if(appusers.Count>0)
            {
                return appusers;

            }
            return null;

        }
        public async Task<IEnumerable<appuser>> GetAllAppUserByUserType(String usertype)
        {
            var appusers= await apiDbContext.appusers_shameem.Where(o=>o.UserType.UserType==usertype).ToListAsync();
            if (appusers.Count > 0)
            {
                return appusers;

            }
            return null;

        }
        public async Task<appuser> EditAppUser(Guid id, appuser appuser)
        {
            var appuser_toedit = await apiDbContext.appusers_shameem.SingleOrDefaultAsync(option => option.AppUserId == id);
            if (appuser_toedit != null)
            {
                appuser_toedit.UserTypeId = appuser.UserTypeId ?? appuser_toedit.UserTypeId;
                appuser_toedit.UserName = appuser.UserName ?? appuser_toedit.UserName;
                appuser_toedit.Password = appuser.Password ?? appuser_toedit.Password;
                appuser_toedit.IsActive = appuser.IsActive ?? appuser_toedit.IsActive;
                await apiDbContext.SaveChangesAsync();
                return appuser_toedit;
            }
            else 
            {
                return null;
            }
        }
        public async Task<appuser> DeleteAppUser([FromRoute] Guid id)
        {
            var appuser_todelete = await apiDbContext.appusers_shameem.FirstOrDefaultAsync(option => option.AppUserId == id);
            if (appuser_todelete != null)
            {
                apiDbContext.Remove(appuser_todelete);
                await apiDbContext.SaveChangesAsync();
                return appuser_todelete;
            }
            else
            {
                return null;
            }
        }
    }
}
