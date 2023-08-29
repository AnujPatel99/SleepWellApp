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
    public async Task Test_GetMovies_ReturnUserAndFavoriteMovies()
    {
        // Arrange
        var mockHttp = new MockHttpMessageHandler();
        // Mock api/User end point which returns UserDto
        // also mock the OMDB api returns for individual movies
        string testUserResponse =
    "{ \"id\": \"4aedd1be-f5fa-4350-bde9-a5922922fe0e\", " +
    "\"userName\": \"test@test.com\", " +
    "\"firstName\": \"Anuj\", " +
    "\"lastName\": \"Patel\" }";


        mockHttp.When("https://localhost:7109/api/get-users/")
            .Respond("application/json", testUserResponse);

        var client = mockHttp.ToHttpClient();
        client.BaseAddress = new Uri("https://localhost:7109/");
        var userInfoHttpRepository = new UserInfoHttpRepository(client);

        // Act
        var response = await userInfoHttpRepository.GetUsers();
        var userList = response.Data;


        // Assert 
        Assert.That(userList.Count(), Is.EqualTo(1));
        /*Assert.That(movies[0].Title, Is.EqualTo("Mission: Impossible - Dead Reckoning Part One"));
        Assert.That(movies[0].Year, Is.EqualTo("2023"));
        Assert.That(movies[1].Title, Is.EqualTo("The Lighthouse"));
        Assert.That(movies[1].Year, Is.EqualTo("2019"));*/
    }
}