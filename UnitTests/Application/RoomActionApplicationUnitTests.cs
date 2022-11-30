using Arch.EntityFrameworkCore.UnitOfWork;
using Domain.Contracts.Application;
using Domain.Contracts.Repositories;
using Domain.DTOs.Request;
using FluentValidation.TestHelper;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            _unitOfWorkMock.Verify(f => f.SaveChanges(false), Times.Once);
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

        #endregion
    }
}
