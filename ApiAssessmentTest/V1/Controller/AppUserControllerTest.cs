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
        public async void AddAppUser_ValidAppUser_ReturnsOkResult()
        {
            // Arrange
            var appuser = fixture.Create<appuser>();
            var expectedAppUser = fixture.Create<appuser>();

            appUserInterface.Setup(x => x.AddAppUser(appuser))
                                .ReturnsAsync(expectedAppUser);


            // Act
            var result =  await _sut.AddAppUser(appuser);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<IActionResult>();
            result.Should().BeAssignableTo<OkObjectResult>().Subject.Value.Should().Be(expectedAppUser);

            appUserInterface.Verify(r=>r.AddAppUser(appuser),Times.Once());
           

        }
        [Fact]
        public void AddAppUser_WhenUserTypeIdNull_ShouldReturnBadRequest()
        {
            //Arrange
            var user = fixture.Create<appuser>();
            user.UserTypeId = null;
            //var returnData = fixture.Create<appuser>();
            appUserInterface.Setup(c => c.AddAppUser(user)).ReturnsAsync((appuser)null);
            //Act
            var result = _sut.AddAppUser(user);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
            appUserInterface.Verify(r => r.AddAppUser(user), Times.Once());


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
             result.Should().BeOfType<StatusCodeResult>().Subject
                .StatusCode.Should().Be(500);
            appUserInterface.Verify(r => r.AddAppUser(appuser), Times.Once());

        }
        [Fact]
        public async Task GetAllAppUser_AppUsersFopund_ReturnsOkResult()
        {
            // Arrange
            var expectedAppUsers = fixture.Create<List<appuser>>();

            appUserInterface.Setup(x => x.GetAllAppUser())
                                .ReturnsAsync(expectedAppUsers);


            // Act
            var result = await _sut.GetAllAppUser();

            // Assert
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                  .Should().BeAssignableTo<List<appuser>>().Subject
                  .Should().BeEquivalentTo(expectedAppUsers);
            appUserInterface.Verify(r => r.GetAllAppUser(), Times.Once());

        }

        [Fact]
        public async Task GetAllAppUser_NoAppUsersFound_ReturnsNotFoundResult()
        {
            // Arrange
            appUserInterface.Setup(x => x.GetAllAppUser())
                                .ReturnsAsync((List<appuser>)null);


            // Act
            var result = await _sut.GetAllAppUser();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            appUserInterface.Verify(r => r.GetAllAppUser(), Times.Once());

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
            appUserInterface.Verify(r => r.GetAllAppUser(), Times.Once());

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
            result.Should().BeOfType<OkObjectResult>().Subject.Value
                  .Should().BeAssignableTo<IEnumerable<appuser>>().Subject
                  .Should().BeEquivalentTo(expectedAppUsers);
            appUserInterface.Verify(r => r.GetAllAppUserByUserType(userType), Times.Once());

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
            appUserInterface.Verify(r => r.GetAllAppUserByUserType(userType), Times.Once());

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
            result.Should().BeOfType<StatusCodeResult>().Subject
                  .StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            appUserInterface.Verify(r => r.GetAllAppUserByUserType(userType), Times.Once());

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
             result.Should().BeOfType<OkObjectResult>().Subject
                   .Value.Should().BeAssignableTo<appuser>();
            appUserInterface.Verify(r => r.EditAppUser(appUserId,appuser), Times.Once());



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
            appUserInterface.Verify(r => r.EditAppUser(appUserId, appuser), Times.Once());

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
            
            result.Should().BeOfType<StatusCodeResult>().Subject
                  .StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
            appUserInterface.Verify(r => r.EditAppUser(id, appuser), Times.Once());

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
            result.Should().BeOfType<OkObjectResult>().Subject
                    .Value.Should().BeAssignableTo<appuser>().Subject
                    .Should().BeEquivalentTo(expectedDeletedUser);
            appUserInterface.Verify(r => r.DeleteAppUser(idToDelete), Times.Once());

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
            appUserInterface.Verify(r => r.DeleteAppUser(idToDelete), Times.Once());

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
             result.Should().BeOfType<StatusCodeResult>().Subject
                   .StatusCode.Should().Be(500);
            appUserInterface.Verify(r => r.DeleteAppUser(idToDelete), Times.Once());

        }
    }
}




