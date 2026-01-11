using Xunit;
using Moq;
using Skins.Core.Services;
using Skins.Core.Models.User;
using Skins.Core.Exceptions;
using Skins.Infrastructure.Common;
using Skins.Infrastructure.Data.Models;
using System.Collections.Generic;
using System.Linq;

namespace Skins.Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IRepository> _mockRepo;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mockRepo = new Mock<IRepository>();
            _userService = new UserService(_mockRepo.Object);
        }

        [Fact]
        public void Add_WithValidUser_ShouldAddUser()
        {
            // Arrange
            var registerModel = new UserRegisterViewModel
            {
                Username = "testuser",
                Email = "test@example.com",
                Password = "Password123!"
            };

            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(new List<User>().AsQueryable());

            // Act
            _userService.Add(registerModel);

            // Assert
            _mockRepo.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Add_WithDuplicateEmail_ShouldThrowException()
        {
            // Arrange
            var registerModel = new UserRegisterViewModel
            {
                Username = "newuser",
                Email = "existing@example.com",
                Password = "Password123!"
            };

            var existingUsers = new List<User>
            {
                new User { Username = "existinguser", Email = "existing@example.com" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(existingUsers);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _userService.Add(registerModel));
        }

        [Fact]
        public void Add_WithDuplicateUsername_ShouldThrowException()
        {
            // Arrange
            var registerModel = new UserRegisterViewModel
            {
                Username = "testuser",
                Email = "new@example.com",
                Password = "Password123!"
            };

            var existingUsers = new List<User>
            {
                new User { Username = "testuser", Email = "existing@example.com" }
            }.AsQueryable();

            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(existingUsers);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _userService.Add(registerModel));
        }

        [Fact]
        public void GetById_WithValidId_ShouldReturnUser()
        {
            // Arrange
            var userId = "1";
            var user = new User { Id = userId, Username = "testuser", Email = "test@example.com" };

            _mockRepo.Setup(r => r.GetById<User>(userId))
                .Returns(user);

            // Act
            var result = _userService.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(userId, result.Id);
        }

        [Fact]
        public void GetById_WithNullId_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _userService.GetById(null));
        }

        [Fact]
        public void GetById_WithEmptyId_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _userService.GetById(""));
        }

        [Fact]
        public void GetById_WithNonexistentId_ShouldThrowException()
        {
            // Arrange
            _mockRepo.Setup(r => r.GetById<User>(It.IsAny<string>()))
                .Returns((User)null);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _userService.GetById("999"));
        }

        [Fact]
        public void Remove_WithValidId_ShouldReturnTrue()
        {
            // Arrange
            var userId = "1";

            _mockRepo.Setup(r => r.Delete<User>(userId));
            _mockRepo.Setup(r => r.SaveChanges());

            // Act
            var result = _userService.Remove(userId);

            // Assert
            Assert.True(result);
            _mockRepo.Verify(r => r.Delete<User>(userId), Times.Once);
        }

        [Fact]
        public void Remove_WithInvalidId_ShouldReturnFalse()
        {
            // Arrange
            _mockRepo.Setup(r => r.Delete<User>(It.IsAny<string>()))
                .Throws<NullReferenceException>();

            // Act
            var result = _userService.Remove("999");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Update_WithValidData_ShouldUpdateUser()
        {
            // Arrange
            var userId = "1";
            var user = new User { Id = userId, Username = "olduser", Email = "old@example.com" };
            var updateModel = new UserUpdateViewModel
            {
                Username = "newuser",
                Email = "new@example.com",
                Password = null
            };

            _mockRepo.Setup(r => r.GetById<User>(userId))
                .Returns(user);

            // Act
            _userService.Update(userId, updateModel);

            // Assert
            _mockRepo.Verify(r => r.Update(It.IsAny<User>()), Times.Once);
            _mockRepo.Verify(r => r.SaveChanges(), Times.Once);
        }

        [Fact]
        public void Update_WithNullId_ShouldThrowException()
        {
            // Act & Assert
            Assert.Throws<NotFoundException>(() => _userService.Update(null, new UserUpdateViewModel()));
        }

        [Fact]
        public void VerifyPassword_WithCorrectPassword_ShouldReturnTrue()
        {
            // Arrange
            var username = "testuser";
            var password = "Password123!";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, PasswordHash = hashedPassword };

            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(new List<User> { user }.AsQueryable());

            // Act
            var result = _userService.VerifyPassword(username, password);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPassword_ShouldReturnFalse()
        {
            // Arrange
            var username = "testuser";
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword("CorrectPassword123!");
            var user = new User { Username = username, PasswordHash = hashedPassword };

            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(new List<User> { user }.AsQueryable());

            // Act
            var result = _userService.VerifyPassword(username, "WrongPassword123!");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void VerifyPassword_WithNonexistentUser_ShouldReturnFalse()
        {
            // Arrange
            _mockRepo.Setup(r => r.AllAsNoTracking<User>())
                .Returns(new List<User>().AsQueryable());

            // Act
            var result = _userService.VerifyPassword("nonexistent", "Password123!");

            // Assert
            Assert.False(result);
        }
    }
}