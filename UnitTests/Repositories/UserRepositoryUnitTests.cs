using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using Moq;
using UnitTests.Fixture;

namespace UnitTests.Repositories
{
    public sealed class UserRepositoryUnitTests : IClassFixture<TestFixture>
    {
        private readonly Mock<IUnitOfWork<DataContext>> _unitOfWorkMock;
        private readonly TestFixture _testFixture;
        private readonly IUserRepository _userRepository;

        public UserRepositoryUnitTests()
        {
            _testFixture = new TestFixture();
            _unitOfWorkMock = _testFixture.UnitOfWorkMock;
            _userRepository = _testFixture.UserRepository;
        }

        #region CRUD TESTS

        [Fact]
        public void GetAll_ShouldReturnAllUsers_NoCondition()
        {
            var response = _userRepository.GetAll();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ShouldReturnOneUser_WhenIdIsValid(int userId)
        {
            var response = _userRepository.GetById(userId);

            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_ShouldReturnNull_WhenIdIsInvalid(int userId)
        {
            var response = _userRepository.GetById(userId);
            Assert.Null(response);
        }

        [Fact]
        public void Delete_ShouldDeleteUser_WhenIdIsValid()
        {
            var user = new User
            {
                Id = 1,
                UserName = "Bob"
            };

            _userRepository.Delete(user);
            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnSuccess_WhenDataIsValid()
        {
            var user = new User
            {
                UserName = "DVD",
            };

            _userRepository.Create(user);

            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnSuccess_WhenDataIsValid()
        {
            var user = new User
            {
                Id = 1,
                UserName = "DVD",
            };

            _userRepository.Update(user);

            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        #endregion
    }
}
