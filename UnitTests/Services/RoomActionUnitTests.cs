using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Services;
using Domain.DTOs.Request;
using Domain.Entities;
using Domain.Enums;
using FluentValidation.TestHelper;
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

        #region CRUD TESTS
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
        #endregion

        #region PUBLIC METHODS

        [Fact]
        public void EnterTheRoom_ShouldRegisterEnterTheRoomAction_NoCondition()
        {
            var request = new EnterTheRoomRequest
            {
                UserName = "Henry"
            };

            var validator = new EnterTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            _roomActionService.EnterTheRoom(request);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void EnterTheRoom_ShouldThrowException_WhenUserNotElegibleForEntering()
        {
            var request = new EnterTheRoomRequest
            {
                UserName = "Bob"
            };

            var validator = new EnterTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            Assert.Throws<InvalidOperationException>(() => _roomActionService.EnterTheRoom(request));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void EnterTheRoom_ShouldThrowValidationException_WhenUserNameIsNull()
        {
            var request = new EnterTheRoomRequest
            {
                UserName = null
            };

            var validator = new EnterTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Any());
        }

        [Fact]
        public void LeaveTheRoom_ShouldRegisterLeaveTheRoomAction_NoCondition()
        {
            var request = new LeaveTheRoomRequest
            {
                UserId = 1
            };

            var validator = new LeaveTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            _roomActionService.LeaveTheRoom(request);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.AtLeast(2));
        }

        [Fact]
        public void LeaveTheRoom_ShouldNotPassValidation_WhenIdIsInvalid()
        {
            var request = new LeaveTheRoomRequest
            {
                UserId = 0
            };

            var validator = new LeaveTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Any());
        }

        [Fact]
        public void LeaveTheRoom_ShouldThrowInvalidOperationExcepetion_WhenUserIsNotElegibleToLeave()
        {
            var request = new LeaveTheRoomRequest
            {
                UserId = 3
            };

            var validator = new LeaveTheRoomRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            Assert.Throws<InvalidOperationException>(() => _roomActionService.LeaveTheRoom(request));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void Comment_ShouldRegisterCommentAction_WhenCommentIsValid()
        {
            var request = new CommentRequest
            {
                UserId = 1,
                Comment = "New comment"
            };

            var validator = new CommentRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            _roomActionService.Comment(request);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void Comment_ShouldFailValidation_WhenUserIdIsInvalid()
        {
            var request = new CommentRequest
            {
                UserId = 0
            };

            var validator = new CommentRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Any());
        }

        [Fact]
        public void Comment_ShouldFailValidation_WhenCommentIsInvalid()
        {
            var request = new CommentRequest
            {
                Comment = string.Empty
            };

            var validator = new CommentRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Any());
        }

        [Fact]
        public void Comment_ShouldThrowInvalidOperationExcepetion_WhenUserDoesNotExist()
        {
            var request = new CommentRequest
            {
                UserId = 3,
                Comment = "New Comment"
            };

            var validator = new CommentRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            Assert.Throws<InvalidOperationException>(() => _roomActionService.Comment(request));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        #endregion
    }
}
