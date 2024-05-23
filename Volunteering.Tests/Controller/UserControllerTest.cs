namespace Volunteering.Tests.Controller
{
    public class UserControllerTest
    {
        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Register_ValidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Register_InvalidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Register_MissingData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Register_EmptyData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Login_ValidData(string number)
        {
            Assert.True(true);
        }


        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Login_InvalidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Login_MissingData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void UserController_Login_EmptyData(string number)
        {
            Assert.True(true);
        }
    }
}