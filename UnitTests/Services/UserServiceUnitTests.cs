using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Services;
using Domain.Entities;
using Infrastructure.Data;
using Moq;
using UnitTests.Fixture;

namespace UnitTests.Services
{
    public sealed class UserServiceUnitTests : IClassFixture<TestFixture>
    {
        private readonly Mock<IUnitOfWork<DataContext>> _unitOfWorkMock;
        private readonly TestFixture _testFixture;
        private readonly IUserService _userService;

        public UserServiceUnitTests()
        {
            _testFixture = new TestFixture();
            _unitOfWorkMock = _testFixture.UnitOfWorkMock;
            _userService = _testFixture.UserService;
        }

        [Fact]
        public void GetAll_ShouldReturnAllUsers_NoCondition()
        {
            var response = _userService.GetAll();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ShouldReturnOneUser_WhenIdIsValid(int userId)
        {
            var response = _userService.GetById(userId);

            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_ShouldReturnNull_WhenIsIsInvalid(int userId)
        {
            var response = _userService.GetById(userId);

            Assert.Null(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Delete_ShouldDeleteRecord_WhenIdIsValid(int userId)
        {
            var user = new User
            {
                Id = 1,
                UserName = "Bob"
            };

            _userService.Delete(userId);

            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Once);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Delete_ShouldReturnError_WhenIdIsInvalid(int userId)
        {
            Assert.Throws<InvalidOperationException>(() => _userService.Delete(userId));

            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenDataIsValid()
        {
            var user = new User
            {
                UserName = "DVD",
            };

            _userService.Create(user);

            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenUserNameIsNull()
        {
            var user = new User
            {
                UserName = null,
            };

            Assert.Throws<InvalidOperationException>(() => _userService.Create(user));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void Update_ShouldReturnSuccess_WhenDataIsValid()
        {
            var user = new User
            {
                Id = 1,
                UserName = "DVD",
            };

            _userService.Update(user);

            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnError_WhenUserNameIsNull()
        {
            var user = new User
            {
                UserName = null,
            };

            Assert.Throws<InvalidOperationException>(() => _userService.Update(user));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

    }
}
