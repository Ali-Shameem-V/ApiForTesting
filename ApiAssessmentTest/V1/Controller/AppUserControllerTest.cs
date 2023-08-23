using ApiForTesting.Controllers;
using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace ApiAssessmentTest.V1.Controller
{
    public class AppUserControllerTest
    {

        private readonly IFixture fixture;
        private readonly Mock<IAppUser> appUserInterface;
        private readonly AppUserController _sut;
        public AppUserControllerTest()
        {
            fixture = new Fixture();
            appUserInterface = fixture.Freeze<Mock<IAppUser>>();
            _sut = new AppUserController(appUserInterface.Object);
        }

        [Fact]
        public async Task AddAppUser_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var appuser = fixture.Create<appuser>();
            var expectedAppUser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.AddAppUser(appuser))
                                .ReturnsAsync(expectedAppUser);


            // Act
            var result =  _sut.AddAppUser(appuser);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<OkObjectResult>();
        }
        [Fact]
        public void AddAppUser_WhenUserTypeIdNull_ShouldReturnBadRequest()
        {
            //Arrange
            var user = fixture.Create<appuser>();
            user.UserTypeId = null;
            var expectedExceptionMessage = "Please give a valid foreign key";
            //var returnData = fixture.Create<appuser>();
            appUserInterface.Setup(c => c.AddAppUser(user)).ReturnsAsync((appuser)null);
            //Act
            var result = _sut.AddAppUser(user);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

       


        [Fact]
        public async Task AddAppUser_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var appuser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.AddAppUser(It.IsAny<appuser>()))
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.AddAppUser(appuser);

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
         
            statusCodeResult.StatusCode.Should().Be(500);
        }
        [Fact]
        public async Task GetAllAppUser_ValidData_ReturnsOkResult()
        {
            // Arrange
            var expectedAppUsers = fixture.Create<List<appuser>>();

            appUserInterface.Setup(x => x.GetAllAppUser())
                                .ReturnsAsync(expectedAppUsers);


            // Act
            var result = await _sut.GetAllAppUser();

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            var resultValue = okResult.Value.Should().BeAssignableTo<List<appuser>>().Subject;

            resultValue.Should().BeEquivalentTo(expectedAppUsers);
        }

        [Fact]
        public async Task GetAllAppUser_NoData_ReturnsNotFoundResult()
        {
            // Arrange
            appUserInterface.Setup(x => x.GetAllAppUser())
                                .ReturnsAsync((List<appuser>)null);


            // Act
            var result = await _sut.GetAllAppUser();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAllAppUser_Exception_ReturnsBadRequestResult()
        {
            // Arrange
            appUserInterface.Setup(x => x.GetAllAppUser())
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.GetAllAppUser();

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
        [Fact]
        public async Task GetAllAppUserByUserType_ValidUserType_ReturnsOkResult()
        {
            // Arrange
            var userType = fixture.Create<string>();
            var expectedAppUsers = fixture.CreateMany<appuser>();

            appUserInterface.Setup(x => x.GetAllAppUserByUserType(It.IsAny<string>()))
                                .ReturnsAsync(expectedAppUsers);


            // Act
            var result = await _sut.GetAllAppUserByUserType(userType);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<IEnumerable<appuser>>().Subject;

            resultValue.Should().BeEquivalentTo(expectedAppUsers);
        }

        [Fact]
        public async Task GetAllAppUserByUserType_InvalidUserType_ReturnsNotFoundResult()
        {
            // Arrange
            var userType = fixture.Create<string>();

            appUserInterface.Setup(x => x.GetAllAppUserByUserType(It.IsAny<string>()))
                                .ReturnsAsync((IEnumerable<appuser>)null);


            // Act
            var result = await _sut.GetAllAppUserByUserType(userType);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAllAppUserByUserType_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var userType = fixture.Create<string>();

            appUserInterface.Setup(x => x.GetAllAppUserByUserType(It.IsAny<string>()))
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.GetAllAppUserByUserType(userType);

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task EditAppUser_ValidAppUserId_ReturnsOkResultWithModifiedData()
        {
            // Arrange
            var appUserId = Guid.NewGuid();
            var appuser = fixture.Create<appuser>();
            var existingAppUser = fixture.Create<appuser>();
            

            appUserInterface.Setup(x => x.EditAppUser(appUserId, appuser))
                                .ReturnsAsync(existingAppUser);


            // Act
            var result = await _sut.EditAppUser(appUserId, appuser);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            okResult.Value.Should().BeAssignableTo<appuser>();


        }

        [Fact]
        public async Task EditAppUser_InvalidAppUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var appUserId = Guid.NewGuid();
            var appuser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.EditAppUser(appUserId, appuser))
                                .ReturnsAsync((appuser)null);


            // Act
            var result = await _sut.EditAppUser(appUserId, appuser);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task EditAppUser_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var id = Guid.NewGuid();
            var appuser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.EditAppUser(id, appuser))
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.EditAppUser(id, appuser);

            // Assert
            
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task DeleteAppUser_ValidAppUserId_ReturnsOkResult()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();
            var expectedDeletedUser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.DeleteAppUser(idToDelete))
                                .ReturnsAsync(expectedDeletedUser);


            // Act
            var result = await _sut.DeleteAppUser(idToDelete);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<appuser>().Subject;

            resultValue.Should().BeEquivalentTo(expectedDeletedUser);
        }

        [Fact]
        public async Task DeleteAppUser_InvalidAppUserId_ReturnsNotFoundResult()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();

            appUserInterface.Setup(x => x.DeleteAppUser(idToDelete))
                                .ReturnsAsync((appuser)null);


            // Act
            var result = await _sut.DeleteAppUser(idToDelete);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteAppUser_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var idToDelete = Guid.NewGuid();

            appUserInterface.Setup(x => x.DeleteAppUser(idToDelete))
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.DeleteAppUser(idToDelete);

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(500);
        }
    }
}




