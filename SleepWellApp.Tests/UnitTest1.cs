using SleepWellApp.Client.HttpRepository;
using RichardSzalay.MockHttp;
using System.Threading.Tasks;

namespace SleepWellApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task GetUserId()
        {
            // Arrange
            var mockHttp = new MockHttpMessageHandler();

            string testUserResponse = "";
        // 
        Assert.Pass();
        }
    }
}