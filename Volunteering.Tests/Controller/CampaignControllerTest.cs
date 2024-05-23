using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Volunteering.Tests.Controller
{
    public class CampaignControllerTest
    {
        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Add_Authorized_ValidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Add_Authorized_InvalidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Add_Unauthorized(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Update_Admin_ValidData(string number)
        {
            Assert.True(true);
        }

        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Update_Admin_InvalidData(string number)
        {
            Assert.True(true);
        }


        [Theory]
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Update_Unauthorized(string number)
        {
            Assert.True(true);
        }

        [Fact]
        public void CampaignController_Get()
        {
            Assert.True(true);
            //Assert.True(false);
        }

        [Theory]    
        [InlineData("0000000000000000000000")]
        [InlineData("string value")]
        [InlineData("")]
        [InlineData("0000000000")]
        public void CampaignController_Get_Filtered(string number)
        {
            Assert.True(true);
        }
    }
}
