using ApiForTesting.Controllers;
using ApiForTesting.Data;
using ApiForTesting.Service.Interface;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ApiAssessmentTest
{
    public class UserTypeControllerTest
    {

        private readonly IFixture fixture;
        private readonly Mock<IUserType> usertypeInterface;
        private readonly UserTypeController _sut;

        public UserTypeControllerTest()
        {
            fixture = new Fixture();
            usertypeInterface = fixture.Freeze<Mock<IUserType>>();
            _sut =new UserTypeController(usertypeInterface.Object);

        }

        [Fact]
        public async Task AddUserType_ValidInput_ReturnsOkResult()
        {
            // Arrange
            var usertype = fixture.Create<usertype>();
            var expectedUserTypeDetail = fixture.Create<usertype>();

            usertypeInterface.Setup(x => x.AddUserType(usertype))
                                .ReturnsAsync(expectedUserTypeDetail);


            // Act
            var result = await _sut.AddUserType(usertype);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<usertype>().Subject;

            resultValue.Should().BeEquivalentTo(expectedUserTypeDetail);
        }
        [Fact]
        public void AddUserType_ShouldReturnBadRequest_WhenUserTypeIdNull()
        {
            //Arrange
            var user = fixture.Create<usertype>();
            user.UserTypeId = null;
            var returnData = fixture.Create<usertype>();
            usertypeInterface.Setup(c => c.AddUserType(user)).ReturnsAsync((usertype)null);
            //Act
            var result = _sut.AddUserType(user);
            //Assert
            result.Should().NotBeNull();
            result.Should().BeAssignableTo<Task<IActionResult>>();
            result.Result.Should().BeAssignableTo<BadRequestObjectResult>();
        }

        

        [Fact]
        public async Task AddUserType_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var usertype = fixture.Create<usertype>();

            usertypeInterface.Setup(x => x.AddUserType(usertype))
                                .ThrowsAsync(new Exception());


            // Act
            var result = await _sut.AddUserType(usertype);

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task GetAllUserTypes_NotEmptyList_ReturnsOkResultWithUserTypes()
        {
                // Arrange
                var expectedUserTypes = fixture.Create<IEnumerable<usertype>>();

                usertypeInterface.Setup(x => x.GetAllUserTypes())
                                    .ReturnsAsync(expectedUserTypes);


                // Act
                var result = await _sut.GetAllUserTypes();

                // Assert
                var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
                var resultValue = okResult.Value.Should().BeAssignableTo<IEnumerable<usertype>>().Subject;

                resultValue.Should().BeEquivalentTo(expectedUserTypes);
        }

        [Fact]
        public async Task GetAllUserTypes_EmptyList_ReturnsNotFoundResult()
        {
            // Arrange

            usertypeInterface.Setup(x => x.GetAllUserTypes())
                .ReturnsAsync((IEnumerable<usertype>)null);


            // Act
            var result = await _sut.GetAllUserTypes();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetAllUserTypes_Exception_ReturnsInternalServerError()
        {
            // Arrange
            usertypeInterface.Setup(x => x.GetAllUserTypes())
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.GetAllUserTypes();

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
        [Fact]
        public async Task EditUserType_ValidUserTypeId_ReturnsOkResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usertype = fixture.Create<usertype>();
            var expectedUserTypeToEdit = fixture.Create<usertype>();

            usertypeInterface.Setup(x => x.EditUserType(id,usertype))
                                .ReturnsAsync(expectedUserTypeToEdit);


            // Act
            var result = await _sut.EditUserType(id, usertype);

            // Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var resultValue = okResult.Value.Should().BeAssignableTo<usertype>().Subject;

            resultValue.Should().BeEquivalentTo(expectedUserTypeToEdit);
        }

        [Fact]
        public async Task EditUserType_NullUserTypeToEdit_ReturnsNotFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();
            usertypeInterface.Setup(x => x.EditUserType(id, It.IsAny<usertype>()))
                                .ReturnsAsync((usertype)null);


            // Act
            var result = await _sut.EditUserType(id, null);

            // Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task EditUserType_Exception_ReturnsInternalServerError()
        {
            // Arrange
            var id = Guid.NewGuid();
            var usertype = fixture.Create<usertype>();

            usertypeInterface.Setup(x => x.EditUserType(It.IsAny<Guid>(), It.IsAny<usertype>()))
                                .ThrowsAsync(new Exception("Test exception"));


            // Act
            var result = await _sut.EditUserType(id, usertype);

            // Assert
            var statusCodeResult = result.Should().BeOfType<StatusCodeResult>().Subject;
            statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }
    }
}


