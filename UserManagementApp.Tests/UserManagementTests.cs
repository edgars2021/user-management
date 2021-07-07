using Microsoft.AspNetCore.Mvc;
using Moq;
using UserManagementApp.Core;
using UserManagementApp.Core.Models;
using UserManagementApp.Core.Services;
using UserManagementApp.HttpApi;
using Xunit;

namespace UserManagementApp.Tests
{
    public class UserManagementTests
    {
        private Mock<IUserService> _userServiceMock = new Mock<IUserService>();
        
        [Fact]
        public async void UserController_GivenId_ReturnSuccess()
        {
            var expectedUser = new User()
            {
                Id = 1,
                FirstName = "Test name",
                LastName = "Last name"
            };
            _userServiceMock.Setup(s => s.GetByAsync(1)).ReturnsAsync(expectedUser);
            var userController = new UsersController(_userServiceMock.Object);

            var result = (await userController.GetBy(1)) as ObjectResult;
            
            Assert.NotNull(result);
            Assert.IsType<User>(result.Value);
            Assert.Equal(200, result?.StatusCode);
        }
        
        [Fact]
        public async void UserController_GivenId_ReturnNotFound()
        {
            _userServiceMock.Setup(s => s.GetByAsync(1)).ReturnsAsync((User) null);
            var userController = new UsersController(_userServiceMock.Object);

            var result = await userController.GetBy(1);
            
            Assert.Equal(404, (result as NotFoundResult)?.StatusCode);
        }
        
        [Fact]
        public async void UserController_CreateUser_ReturnSuccess()
        {
            var givenUser = new UserModelDto()
            {
                FirstName = "Test name",
                LastName = "Last name"
            };
            var expectedUser = new User()
            {
                Id = 1,
                FirstName = "Test name",
                LastName = "Last name"
            };
            
            _userServiceMock.Setup(s => s.CreateAsync(givenUser)).ReturnsAsync(expectedUser);
            var userController = new UsersController(_userServiceMock.Object);

            var result = (await userController.Create(givenUser)) as CreatedAtActionResult;
            
            Assert.NotNull(result);
            Assert.IsType<User>(result.Value);
            Assert.Equal(201, result?.StatusCode);
        }

        [Fact]
        public async void UserController_UpdateUser_ReturnSuccess()
        {
            var givenUser = new UserModelDto()
            {
                FirstName = "Test name",
                LastName = "Last name"
            };
            var expectedUser = new User()
            {
                Id = 1,
                FirstName = "Test name",
                LastName = "Last name"
            };
            
            _userServiceMock.Setup(s => s.UpdateAsync(1, givenUser)).ReturnsAsync(expectedUser);
            var userController = new UsersController(_userServiceMock.Object);

            var result = (await userController.Update(1, givenUser)) as ObjectResult;
            
            Assert.NotNull(result);
            Assert.IsType<User>(result.Value);
            Assert.Equal(200, result?.StatusCode);
        }
        
        [Fact]
        public async void UserController_UpdateUser_ReturnNotFound()
        {
            
            _userServiceMock.Setup(s => s.UpdateAsync(1, new UserModelDto())).ReturnsAsync((User) null);
            var userController = new UsersController(_userServiceMock.Object);

            var result = await userController.Update(1, new UserModelDto());
            
            Assert.Equal(404, (result as NotFoundResult)?.StatusCode);
        }
        
        [Fact]
        public async void UserController_DeleteUser_ReturnSuccess()
        {
            _userServiceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(true);
            var userController = new UsersController(_userServiceMock.Object);

            var result = (await userController.Delete(1)) as NoContentResult;
            
            Assert.NotNull(result);
            Assert.Equal(204, result?.StatusCode);
        }
        
        [Fact]
        public async void UserController_DeleteUser_ReturnNotFound()
        {
            _userServiceMock.Setup(s => s.DeleteAsync(1)).ReturnsAsync(false);
            var userController = new UsersController(_userServiceMock.Object);

            var result = (await userController.Delete(1)) as NotFoundResult;
            
            Assert.NotNull(result);
            Assert.Equal(404, result?.StatusCode);
        }
    }
}