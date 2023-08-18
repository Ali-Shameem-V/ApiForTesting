using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiForTesting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUser AppUserInterface;

        public AppUserController(IAppUser appUserInterface)
        {
            AppUserInterface = appUserInterface;
        }
        [HttpPost]
        public async Task<IActionResult> AddAppUser(appuser appuser)
        {
            try
            {
                 
                    if (appuser.UserTypeId == null)
                    {
                        
                        return BadRequest("Please give a valid foreign key");
                    }
                var appusers = await AppUserInterface.AddAppUser(appuser);

                return Ok(appusers);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllAppUser()
        {
            try
            {
                var allappusers = await AppUserInterface.GetAllAppUser();
                if (allappusers != null)
                {
                    return Ok(allappusers);
                }
                return NotFound();
            }
            catch 
            {
                // You might want to log the exception here for debugging purposes
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("usertype")]
        public async Task<IActionResult> GetAllAppUserByUserType(String usertype)
        {
            try
            {
                var appusers = await AppUserInterface.GetAllAppUserByUserType(usertype);
                if (appusers != null)
                {
                    return Ok(appusers);
                }
                return NotFound();
            }
            catch 
            {
                // You might want to log the exception here for debugging purposes
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPut]
        public async Task<IActionResult> EditAppUser(Guid id, appuser appuser)
        {
            try
            {
                var appuser_toedit = await AppUserInterface.EditAppUser(id, appuser);
                if (appuser_toedit != null)
                {
                    appuser_toedit.UserTypeId = appuser.UserTypeId ?? appuser_toedit.UserTypeId;
                    appuser_toedit.UserName = appuser.UserName ?? appuser_toedit.UserName;
                    appuser_toedit.Password = appuser.Password ?? appuser_toedit.Password;
                    appuser_toedit.IsActive = appuser.IsActive ?? appuser_toedit.IsActive;
                   /* appuser_toedit.UserType = appuser.UserType ?? appuser_toedit.UserType;
                    appuser_toedit.UserName = appuser.UserName ?? appuser.UserName;
                    appuser_toedit.UserTypeId = appuser.UserTypeId ?? appuser.UserTypeId;*/
                  
                    return Ok(appuser_toedit);
                }
                else
                {
                    return NotFound();
                }
            }
            catch 
            {
                return StatusCode(500);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            try
            {
                var appuser_todelete = await AppUserInterface.DeleteAppUser(id);
                if (appuser_todelete != null)
                {
                    return Ok(appuser_todelete);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                // You might want to log the exception here for debugging purposes
                return StatusCode(500);
            }
        }

    }

}
