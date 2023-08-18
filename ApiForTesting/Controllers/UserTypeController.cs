using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTypeController : ControllerBase
    {
        private readonly IUserType UserTypeInterface;

        public UserTypeController(IUserType userTypeInterface)
        {
            UserTypeInterface = userTypeInterface;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserType([FromBody] usertype usertype)
        {
            try
            {

                if (usertype.UserTypeId != null)
                {
                    var usertype_detail = await UserTypeInterface.AddUserType(usertype);

                    return Ok(usertype_detail);
                }
                return BadRequest("UserType-ID cant be Null");
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserTypes()
        {
            try
            {
                var allusertype = await UserTypeInterface.GetAllUserTypes();
                if (allusertype != null)
                {
                    return Ok(allusertype);
                }
                return NotFound();
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditUserType(Guid id, usertype usertype)
        {
            try
            {
                    var usertype_toedit = await UserTypeInterface.EditUserType(id, usertype);
                    if (usertype_toedit != null)
                    {
                        return Ok(usertype_toedit);
                    }
                    return NotFound();
                
            }
            catch 
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


    }
}
