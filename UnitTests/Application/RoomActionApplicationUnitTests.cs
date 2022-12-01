using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Application;
using Domain.DTOs.Request;
using FluentValidation.TestHelper;
using Infrastructure.Data;
using Moq;
using UnitTests.Fixture;

namespace UnitTests.Application
{
    public sealed class RoomActionApplicationUnitTests : IClassFixture<TestFixture>
    {
        private readonly Mock<IUnitOfWork<DataContext>> _unitOfWorkMock;
        private readonly TestFixture _testFixture;
        private readonly IRoomActionApplication _roomActionApplication;

        public RoomActionApplicationUnitTests()
        {
            _testFixture = new TestFixture();
            _unitOfWorkMock = _testFixture.UnitOfWorkMock;
            _roomActionApplication = _testFixture.RoomActionApplication;
        }

        #region CRUD TESTS

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

            _roomActionApplication.EnterTheRoom(request);
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

            Assert.Throws<InvalidOperationException>(() => _roomActionApplication.EnterTheRoom(request));
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

            _roomActionApplication.LeaveTheRoom(request);
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

            Assert.Throws<InvalidOperationException>(() => _roomActionApplication.LeaveTheRoom(request));
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

            _roomActionApplication.Comment(request);
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

            Assert.Throws<InvalidOperationException>(() => _roomActionApplication.Comment(request));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        [Fact]
        public void HighFive_ShouldFailValidation_WhenBothUserMeetRequirements()
        {
            var request = new HighFiveRequest
            {
                UserIdFrom = 1,
                UserIdTo = 2
            };

            var validator = new HighFiveRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            _roomActionApplication.HighFive(request);
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
        }

        [Fact]
        public void HighFive_ShouldFailValidation_WhenOneOfTheUsersIsNotValid()
        {
            var request = new HighFiveRequest
            {
                UserIdFrom = 1,
                UserIdTo = 0
            };

            var validator = new HighFiveRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.False(validation.IsValid);
            Assert.True(validation.Errors.Any());
        }

        [Fact]
        public void HighFive_ShouldThrowInvalidOperationExcepetion_WhenOneOfTheUsersDoesNotExist()
        {
            var request = new HighFiveRequest
            {
                UserIdFrom = 4,
                UserIdTo = 1
            };

            var validator = new HighFiveRequestValidation();
            var validation = validator.TestValidate(request);

            Assert.True(validation.IsValid);
            Assert.False(validation.Errors.Any());

            Assert.Throws<InvalidOperationException>(() => _roomActionApplication.HighFive(request));
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Never);
        }

        #endregion
    }
}
