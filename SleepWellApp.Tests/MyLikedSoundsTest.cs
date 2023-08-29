
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SleepWellApp.Server.Controllers;
using SleepWellApp.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using SleepWellApp.Server.Data;

namespace SleepWellApp.Tests.Controllers
{
    public static class DbSetExtensions
    {
        public static Mock<DbSet<TEntity>> BuildMockDbSet<TEntity>(this IEnumerable<TEntity> data) where TEntity : class
        {
            var queryableData = data.AsQueryable();
            var likedSoundsData = new List<TEntity>();
            var mockSet = new Mock<DbSet<TEntity>>();

            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<TEntity>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            // Setup DbSet methods for mocking data manipulation
            //mockSet.Setup(m => m.Add(It.IsAny<TEntity>())).Callback<TEntity>(entity => likedSoundsData.Add(entity));
            //mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<TEntity>>())).Callback<IEnumerable<TEntity>>(entities => likedSoundsData.AddRange(entities));
            //mockSet.Setup(m => m.Remove(It.IsAny<TEntity>())).Callback<TEntity>(entity => likedSoundsData.Remove(entity));
            //mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Callback<IEnumerable<TEntity>>(entities => likedSoundsData.RemoveAll(entity => entities.Contains(entity)));

            mockSet.Setup(m => m.Add(It.IsAny<TEntity>())).Callback<TEntity>(entity => queryableData = queryableData.Concat(new[]{ entity })); 
            mockSet.Setup(m => m.AddRange(It.IsAny<IEnumerable<TEntity>>())).Callback<IEnumerable<TEntity>>(entities => queryableData = queryableData.Concat(entities)); 
            mockSet.Setup(m => m.Remove(It.IsAny<TEntity>())).Callback<TEntity>(entity => queryableData = queryableData.Except(new[] { entity })); 
            mockSet.Setup(m => m.RemoveRange(It.IsAny<IEnumerable<TEntity>>())).Callback<IEnumerable<TEntity>>(entities => queryableData = queryableData.Except(entities));


            return mockSet;
        }
    }

    [TestFixture]
    public class LikedSoundsControllerTests
    {
        private Mock<ApplicationDbContext> _contextMock;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;

        [SetUp]
        public void Setup()
        {
            _contextMock = new Mock<ApplicationDbContext>();
            _userManagerMock = new MockUserManager<ApplicationUser>();
        }

        private UserManager<ApplicationUser> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new UserManager<ApplicationUser>((IUserStore<ApplicationUser>)store.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task GetAudioIds_ReturnsOkResult()
        {
            // Arrange
            var controller = new LikedSoundsController(_contextMock.Object, _userManagerMock.Object);

            // Mock the user manager behavior
            var user = new ApplicationUser { Id = "someUserId" };
            _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
                            .ReturnsAsync(user);

            // Mock the database behavior
            var likedSoundsData = new List<LikedSounds> { new LikedSounds { ApplicationUserId = user.Id, Sound_Id = 1 } };
            var likedSoundsMockSet = likedSoundsData.AsQueryable().BuildMockDbSet();
            _contextMock.Setup(c => c.LikedSound).Returns(likedSoundsMockSet.Object);

            // Act
            var result = await controller.GetAudioIds();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okResult = (OkObjectResult)result.Result;
            Assert.IsInstanceOf<List<int>>(okResult.Value);
            var likedSoundIds = (List<int>)okResult.Value;
            Assert.AreEqual(1, likedSoundIds.Count);
            Assert.AreEqual(1, likedSoundIds[0]);
        }
    }
}

    //private Mock<DbSet<T>> MockUserDbSet<T>(List<T> data) where T : class
    //{
    //    var queryableData = data.AsQueryable();
    //    var mockSet = new Mock<DbSet<T>>();

    //    mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
    //    mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
    //    mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
    //    mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

    //    return mockSet;
    //}

    //private Mock<ApplicationDbContext> _contextMock;
    //private Mock<UserManager<ApplicationUser>> _userManagerMock;

    //[SetUp]
    //public void Setup()
    //{
    //    _contextMock = new Mock<ApplicationDbContext>();
    //    _userManagerMock = new MockUserManager<ApplicationUser>();
    //}



    //[Test]
    //public async Task LikedSound_LikeAction_AddsLikedSound()
    //{
        

    //// Arrange
    //var controller = new LikedSoundsController(_contextMock.Object, _userManagerMock.Object);

    //    var audioId = 123; // Replace with a valid audio ID
    //    var liked = true;

    //    // Mock the user manager behavior
    //    var user = new ApplicationUser { Id = "someUserId" };
    //    _userManagerMock.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
    //                    .ReturnsAsync(user);

    //    // Mock the database behavior
    //    var likedSoundsData = new List<LikedSounds>();
    //    var likedSoundsMockSet = likedSoundsData.BuildMockDbSet();
    //    _contextMock.Setup(c => c.Users).Returns(MockUserDbSet(new List<ApplicationUser> { user }).Object);
    //    _contextMock.Setup(c => c.LikedSound).Returns(likedSoundsMockSet.Object);

    //    // Act
    //    var result = await controller.LikedSound(audioId, liked);

    //    // Assert
    //    _contextMock.Verify(c => c.SaveChanges(), Times.Once);
    //    Assert.IsInstanceOf<OkResult>(result);
    //}





