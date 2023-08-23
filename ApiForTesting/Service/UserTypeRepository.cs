using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiForTesting.Service
{
    public class UserTypeRepository : IUserType
    {
        private readonly ApiDbContext apiDbContext;

        public UserTypeRepository(ApiDbContext apiDbContext)
        {
            this.apiDbContext = apiDbContext;
        }
        public async Task<usertype> AddUserType([FromBody] usertype usertype)
        {           
                await apiDbContext.usertypes_shameem.AddAsync(usertype);
                await apiDbContext.SaveChangesAsync();
                return usertype;                     
        }
        public async Task<IEnumerable<usertype>> GetAllUserTypes()
        {
            var Usertypes =await apiDbContext.usertypes_shameem.Include(o=>o.appusers).ToListAsync();
            if (Usertypes.Count > 0)
            {
                return Usertypes;

            }
            return null;
        }
        public async Task<usertype> EditUserType(Guid id, usertype usertype)
        {
            var Usertype = await apiDbContext.usertypes_shameem.FindAsync(id);
            if (Usertype != null)
            {
                Usertype.UserTypeId = usertype.UserTypeId ?? Usertype.UserTypeId;
                Usertype.UserType = usertype.UserType?? Usertype.UserType;
                Usertype.Description = usertype.Description ?? Usertype.Description;
                Usertype.IsActive = usertype.IsActive ?? Usertype.IsActive ;
                await apiDbContext.SaveChangesAsync();
                return Usertype;
            }
            return null;

        }
    }
}
