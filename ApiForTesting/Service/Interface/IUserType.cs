using ApiForTesting.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiForTesting.Service.Interface
{
    public interface IUserType
    {
        public Task<usertype> AddUserType(usertype usertype);
        public Task<IEnumerable<usertype>> GetAllUserTypes();
        public  Task<usertype> EditUserType(Guid id, usertype usertype);



    }
}
