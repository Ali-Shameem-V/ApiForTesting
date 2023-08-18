using ApiForTesting.Controllers;
using ApiForTesting.Service.Interface;
using AutoFixture;
using Moq;

namespace ApiAssessmentTest
{
    public class AppUserControllerTest
    {

        private readonly IFixture _fixture;
        private AppUserController _sut;
        private Mock<IAppUser> appuserInterface;

        public AppUserControllerTest()
        {
            _fixture = new Fixture();
            appuserInterface= _fixture.Freeze<Mock<IAppUser>>();
            _sut = new AppUserController(appuserInterface.Object);
        }

        [Fact]
        public void Test1()
        {

        }
    }
}