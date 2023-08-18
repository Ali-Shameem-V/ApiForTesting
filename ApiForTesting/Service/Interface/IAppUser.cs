using ApiForTesting.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiForTesting.Service.Interface
{
    public interface IAppUser
    {
        public Task<appuser> AddAppUser(appuser appuser);
        public  Task<IEnumerable<appuser>> GetAllAppUser();
        public Task<IEnumerable<appuser>> GetAllAppUserByUserType(String usertype);
        public  Task<appuser> EditAppUser(Guid id, appuser appuser);
        public Task<appuser> DeleteAppUser([FromRoute] Guid id);




    }
}
