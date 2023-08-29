using Microsoft.AspNetCore.Identity;
using Moq;
using SleepWellApp.Server.Models;

namespace SleepWellApp.Tests.Controllers
{
    internal class MockUserManager<T> : Mock<UserManager<ApplicationUser>>
    {
    }
}