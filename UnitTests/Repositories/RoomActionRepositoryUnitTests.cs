using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Repositories;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using Moq;
using UnitTests.Fixture;

namespace UnitTests.Repositories
{
    public sealed class RoomActionRepositoryUnitTests : IClassFixture<TestFixture>
    {
        private readonly Mock<IUnitOfWork<DataContext>> _unitOfWorkMock;
        private readonly TestFixture _testFixture;
        private readonly IRoomActionRepository _roomActionRepository;

        public RoomActionRepositoryUnitTests()
        {
            _testFixture = new TestFixture();
            _unitOfWorkMock = _testFixture.UnitOfWorkMock;
            _roomActionRepository = _testFixture.RoomActionRepository;
        }

        [Fact]
        public void GetAll_ShouldReturnAllRoomActions_NoCondition()
        {
            var response = _roomActionRepository.GetAll();

            Assert.NotNull(response);
            Assert.NotEmpty(response);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_ShouldReturnOneRoomAction_WhenIdIsValid(int userId)
        {
            var response = _roomActionRepository.GetById(userId);

            Assert.NotNull(response);
            Assert.Equal(userId, response.Id);
        }

        [Theory]
        [InlineData(3)]
        [InlineData(4)]
        public void GetById_ShouldReturnNull_WhenIdIsInvalid(int userId)
        {
            var response = _roomActionRepository.GetById(userId);

            Assert.Null(response);
        }

        [Fact]
        public void Delete_ShouldDeleteRoomAction_WhenIdIsValid()
        {
            var roomAction = new RoomAction
            {
                Id = 1,
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                UserId = 1,
                CommentId = null,
                HighFiveId = null,
                ActionDate = new DateTime(2022, 11, 26, 5, 0, 0),
                User = new User
                {
                    Id = 1,
                    UserName = "Bob"
                },
                Comment = null,
                HighFive = null
            };

            _roomActionRepository.Delete(roomAction);
            _unitOfWorkMock.Verify(x => x.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Create_ShouldCreateRoomAction_WhenDataIsValid()
        {
            var roomAction = new RoomAction
            {
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                User = new User
                {
                    UserName = "Bob",
                }
            };

            _roomActionRepository.Create(roomAction);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Update_ShouldUpdateRoomAction_WhenDataIsValid()
        {
            var roomAction = new RoomAction
            {
                Id = 1,
                ActionId = (int)ActionEnum.LEAVE_THE_ROOM,
            };

            _roomActionRepository.Update(roomAction);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }
    }
}
