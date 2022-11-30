using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Services;
using Infrastructure.Data;
using Moq;
using UnitTests.Fixture;

namespace UnitTests.Services
{
    public sealed class RoomActionServiceUnitTests : IClassFixture<TestFixture>
    {
        private readonly Mock<IUnitOfWork<DataContext>> _unitOfWorkMock;
        private readonly TestFixture _testFixture;
        private readonly IRoomActionService _roomActionService;

        public RoomActionServiceUnitTests()
        {
            _testFixture = new TestFixture();
            _unitOfWorkMock = _testFixture.UnitOfWorkMock;
            _roomActionService = _testFixture.RoomActionService;
        }

        [Fact]
        public void GetAll_ShouldReturnAllRoomActions_NoCondition()
        {
            var response = _roomActionService.GetAll();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ShouldReturnOneRoomAction_WhenIdIsValid(int userId)
        {
            var response = _roomActionService.GetById(userId);

            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_ShouldReturnNull_WhenIdIsInvalid(int userId)
        {
            var response = _roomActionService.GetById(userId);

            Assert.Null(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Delete_ShouldDeleteRoomAction_WhenIdIsValid(int userId)
        {
            _roomActionService.Delete(userId);
            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Once);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void Delete_ShouldReturnError_WhenIdIsInvalid(int userId)
        {
            Assert.Throws<InvalidOperationException>(() => _roomActionService.Delete(userId));
            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void Create_ShouldCreateRoomAction_WhenDataIsValid()
        {
            var roomAction = new RoomAction
            {
                ActionId = (int) ActionEnum.ENTER_THE_ROOM,
                User = new User
                {
                    UserName = "Bob",
                }
            };

            _roomActionService.Create(roomAction);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Create_ShouldReturnError_WhenUserNameIsNull()
        {
            var roomAction = new RoomAction
            {
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                User = new User
                {
                    UserName = null
                }
            };

            Assert.Throws<InvalidOperationException>(() => _roomActionService.Create(roomAction));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void Update_ShouldUpdateRoomAction_WhenDataIsValid()
        {
            var roomAction = new RoomAction
            {
                Id = 1,
                ActionId = (int)ActionEnum.LEAVE_THE_ROOM,
            };

            _roomActionService.Update(roomAction);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Update_ShouldReturnError_WhenIdIsInvalid()
        {
            var roomAction = new RoomAction
            {
                Id = 0,
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
            };

            Assert.Throws<InvalidOperationException>(() => _roomActionService.Update(roomAction));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }
    }
}
