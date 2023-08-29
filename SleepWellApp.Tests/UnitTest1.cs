using RichardSzalay.MockHttp;
using SleepWellApp.Client.HttpRepository;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SleepWellApp.Client.Test;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task SleepWell_ReturnUser()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        // Mock api/User end point which returns UserDto
        // also mock the OMDB api returns for individual user
        string testUserResponse =
    "[{ \"id\": \"041ef466-6ab1-47de-bdb4-fbc0beaf7c51\", " +
    "\"userName\": \"elgomatim@gmail.com\", " +
    "\"firstName\": \"Malik\", " +
    "\"lastName\": \"Elgomati\" }]";

        //https://localhost:7109/api/get-users
        mockHttp.When("https://localhost:7109/api/get-users")
            .Respond("application/json", testUserResponse);

        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri("https://localhost:7109/");
        var userInfoHttpRepository = new UserInfoHttpRepository(client);

        // Act
        var response = await userInfoHttpRepository.GetUsers();
        var userList = response.Data;


        // Assert 
        Assert.That(userList.Count(), Is.EqualTo(1));
       
    }
}