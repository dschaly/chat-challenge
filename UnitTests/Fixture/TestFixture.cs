using Application;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using Domain.Contracts.Application;
using Domain.Contracts.Repositories;
using Domain.Contracts.Services;
using Domain.DTOs.Response;
using Domain.Entities;
using Domain.Enums;
using Domain.Services.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace UnitTests.Fixture
{
    public sealed class TestFixture
    {
        #region Properties
        public IRoomActionApplication RoomActionApplication { get; set; }
        public IRoomActionService RoomActionService { get; set; }
        public IRoomActionRepository RoomActionRepository { get; set; }
        public IUserService UserService { get; set; }
        public IUserRepository UserRepository { get; set; }
        public Mock<IUnitOfWork<DataContext>> UnitOfWorkMock { get; private set; }
        public IMapper Mapper { get; private set; }
        #endregion

        public TestFixture()
        {
            CreateUnityOfWork();

            CreateMockMapper();

            CreateRepositories();

            CreateServices();

            CreateApplications();
        }

        protected class ModelSampleList
        {
            public ModelSampleList()
            {
                RoomActions = new List<RoomAction>();
                Users = new List<User>();
                Comments = new List<Comment>();
                HighFives = new List<HighFive>();
            }

            public List<RoomAction> RoomActions { get; set; }
            public List<User> Users { get; set; }
            public List<Comment> Comments { get; set; }
            public List<HighFive> HighFives { get; set; }
        }

        private ModelSampleList CreateModelSampleLists()
        {
            #region Models
            var sampleRoomAction1 = new RoomAction
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
                    UserName = "Bob",
                    IsOnline = true
                },
                Comment = null,
                HighFive = null
            };

            var sampleRoomAction2 = new RoomAction
            {
                Id = 2,
                ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                UserId = 2,
                CommentId = null,
                HighFiveId = null,
                ActionDate = new DateTime(2022, 11, 26, 5, 10, 0),
                User = new User
                {
                    Id = 2,
                    UserName = "Kate",
                    IsOnline = true
                },
                Comment = null,
                HighFive = null
            };

            var sampleRoomAction3 = new RoomAction
            {
                Id = 3,
                ActionId = (int)ActionEnum.COMMENT,
                UserId = 2,
                CommentId = null,
                HighFiveId = null,
                ActionDate = new DateTime(2022, 11, 26, 5, 12, 0),
                Comment = new Comment
                {
                    Id = 1,
                    Message = "TEST",
                    UserId = 2,
                },
                HighFive = null,
                User = new User
                {
                    Id = 2,
                    UserName = "Kate",
                    IsOnline = true
                },
            };

            var sampleRoomAction4 = new RoomAction
            {
                Id = 4,
                ActionId = (int)ActionEnum.HIGH_FIVE,
                UserId = 1,
                HighFiveId = 1,
                ActionDate = new DateTime(2022, 11, 26, 5, 13, 0),
                HighFive = new HighFive
                {
                    Id = 1,
                    UserIdTo = 2,
                    UserNameTo = "Kate"
                },
                User = new User
                {
                    Id = 1,
                    UserName = "Bob",
                    IsOnline = true
                },
            };

            var sampleRoomAction5 = new RoomAction
            {
                Id = 5,
                ActionId = (int)ActionEnum.HIGH_FIVE,
                UserId = 1,
                HighFiveId = 2,
                ActionDate = new DateTime(2022, 11, 26, 5, 15, 0),
                HighFive = new HighFive
                {
                    Id = 2,
                    UserIdTo = 2,
                    UserNameTo = "Kate"
                },
                User = new User
                {
                    Id = 1,
                    UserName = "Bob",
                    IsOnline = true
                },
            };

            var sampleRoomAction6 = new RoomAction
            {
                Id = 6,
                ActionId = (int)ActionEnum.HIGH_FIVE,
                UserId = 1,
                HighFiveId = 3,
                ActionDate = new DateTime(2022, 11, 26, 5, 25, 0),
                HighFive = new HighFive
                {
                    Id = 3,
                    UserIdTo = 3,
                    UserNameTo = "Robert"
                },
                User = new User
                {
                    Id = 1,
                    UserName = "Bob",
                    IsOnline = true
                },
            };

            var sampleUser1 = new User
            {
                Id = 1,
                UserName = "Bob",
                IsOnline = true,
                RoomActions = new List<RoomAction>
                {
                    new RoomAction
                    {
                        Id = 1,
                        ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                        UserId = 1,
                    }
                }
            };

            var sampleUser2 = new User
            {
                Id = 2,
                UserName = "Kate",
                IsOnline = true,
                RoomActions = new List<RoomAction>
                {
                    new RoomAction
                    {
                        Id = 2,
                        ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                        UserId = 2,
                    },
                    new RoomAction
                    {
                        Id = 3,
                        ActionId = (int)ActionEnum.COMMENT,
                        UserId = 2,
                    }
                },
                Comments = new List<Comment>
                {
                    new Comment
                    {
                        Id = 1,
                        Message = "TEST",
                        UserId = 2,
                    }
                }
            };

            var sampleUser3 = new User
            {
                Id = 3,
                UserName = "Robert",
                IsOnline = true,
                RoomActions = new List<RoomAction>
                {
                    new RoomAction
                    {
                        Id = 1,
                        ActionId = (int)ActionEnum.ENTER_THE_ROOM,
                        UserId = 1,
                    }
                }
            };

            var sampleComment1 = new Comment
            {
                Id = 1,
                UserId = 1,
                Message = "Test"
            };

            var sampleComment2 = new Comment
            {
                Id = 2,
                UserId = 2,
                Message = "Test 2"
            };

            var sampleHighFive1 = new HighFive
            {
                Id = 1,
                UserIdTo = 1
            };

            var sampleHighFive2 = new HighFive
            {
                Id = 2,
                UserIdTo = 2
            };

            #endregion

            var response = new ModelSampleList();

            response.RoomActions.AddRange(new[] {
                sampleRoomAction1,
                sampleRoomAction2, 
                sampleRoomAction3,
                sampleRoomAction4,
                sampleRoomAction5,
                sampleRoomAction6,
            });
            response.Users.AddRange(new[] { sampleUser1, sampleUser2 });
            response.Comments.AddRange(new[] { sampleComment1, sampleComment2 });
            response.HighFives.AddRange(new[] { sampleHighFive1, sampleHighFive2 });

            return response;
        }

        private void CreateUnityOfWork()
        {
            #region Create DBSets

            var samples = CreateModelSampleLists();

            var dbSetMockRoomAction = new Mock<DbSet<RoomAction>>();
            dbSetMockRoomAction.As<IQueryable<RoomAction>>().Setup(x => x.Provider).Returns(samples.RoomActions.AsQueryable().Provider);
            dbSetMockRoomAction.As<IQueryable<RoomAction>>().Setup(x => x.Expression).Returns(samples.RoomActions.AsQueryable().Expression);
            dbSetMockRoomAction.As<IQueryable<RoomAction>>().Setup(x => x.ElementType).Returns(samples.RoomActions.AsQueryable().ElementType);
            dbSetMockRoomAction.As<IQueryable<RoomAction>>().Setup(x => x.GetEnumerator()).Returns(samples.RoomActions.AsQueryable().GetEnumerator());

            var dbSetMockUser = new Mock<DbSet<User>>();
            dbSetMockUser.As<IQueryable<User>>().Setup(x => x.Provider).Returns(samples.Users.AsQueryable().Provider);
            dbSetMockUser.As<IQueryable<User>>().Setup(x => x.Expression).Returns(samples.Users.AsQueryable().Expression);
            dbSetMockUser.As<IQueryable<User>>().Setup(x => x.ElementType).Returns(samples.Users.AsQueryable().ElementType);
            dbSetMockUser.As<IQueryable<User>>().Setup(x => x.GetEnumerator()).Returns(samples.Users.AsQueryable().GetEnumerator());

            var dbSetMockComment = new Mock<DbSet<Comment>>();
            dbSetMockComment.As<IQueryable<Comment>>().Setup(x => x.Provider).Returns(samples.Comments.AsQueryable().Provider);
            dbSetMockComment.As<IQueryable<Comment>>().Setup(x => x.Expression).Returns(samples.Comments.AsQueryable().Expression);
            dbSetMockComment.As<IQueryable<Comment>>().Setup(x => x.ElementType).Returns(samples.Comments.AsQueryable().ElementType);
            dbSetMockComment.As<IQueryable<Comment>>().Setup(x => x.GetEnumerator()).Returns(samples.Comments.AsQueryable().GetEnumerator());

            var dbSetMockHighFive = new Mock<DbSet<HighFive>>();
            dbSetMockHighFive.As<IQueryable<HighFive>>().Setup(x => x.Provider).Returns(samples.HighFives.AsQueryable().Provider);
            dbSetMockHighFive.As<IQueryable<HighFive>>().Setup(x => x.Expression).Returns(samples.HighFives.AsQueryable().Expression);
            dbSetMockHighFive.As<IQueryable<HighFive>>().Setup(x => x.ElementType).Returns(samples.HighFives.AsQueryable().ElementType);
            dbSetMockHighFive.As<IQueryable<HighFive>>().Setup(x => x.GetEnumerator()).Returns(samples.HighFives.AsQueryable().GetEnumerator());
            #endregion

            #region Create Context

            var context = new Mock<DataContext>();
            context.Setup(x => x.Set<RoomAction>()).Returns(dbSetMockRoomAction.Object);
            context.Setup(x => x.Set<User>()).Returns(dbSetMockUser.Object);
            context.Setup(x => x.Set<Comment>()).Returns(dbSetMockComment.Object);
            context.Setup(x => x.Set<HighFive>()).Returns(dbSetMockHighFive.Object);


            var repositoryRoomAction = new Repository<RoomAction>(context.Object);
            var repositoryUser = new Repository<User>(context.Object);
            var repositoryComment = new Repository<Comment>(context.Object);
            var repositoryHighFive = new Repository<HighFive>(context.Object);
            #endregion

            #region Create Unity of Work

            UnitOfWorkMock = new Mock<IUnitOfWork<DataContext>>();
            UnitOfWorkMock.Setup(wow => wow.DbContext.Set<RoomAction>()).Returns(context.Object.Set<RoomAction>());
            UnitOfWorkMock.Setup(wow => wow.DbContext.Set<User>()).Returns(context.Object.Set<User>());

            UnitOfWorkMock.Setup(uow => uow.DbContext).Returns(context.Object);
            UnitOfWorkMock.Setup(uow => uow.GetRepository<RoomAction>(false)).Returns(repositoryRoomAction);
            UnitOfWorkMock.Setup(uow => uow.GetRepository<User>(false)).Returns(repositoryUser);
            UnitOfWorkMock.Setup(uow => uow.GetRepository<Comment>(false)).Returns(repositoryComment);
            UnitOfWorkMock.Setup(uow => uow.GetRepository<HighFive>(false)).Returns(repositoryHighFive);
            #endregion
        }

        private void CreateMockMapper()
        {
            var mapperMock = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomAction, RoomActionResponse>();
                cfg.CreateMap<User, UserResponse>();
                cfg.CreateMap<Comment, CommentResponse>();
                cfg.CreateMap<HighFive, HighFiveResponse>();
            });

            Mapper = mapperMock.CreateMapper();
        }
        private void CreateRepositories()
        {
            RoomActionRepository = new RoomActionRepository(UnitOfWorkMock.Object);
            UserRepository = new UserRepository(UnitOfWorkMock.Object);
        }

        private void CreateServices()
        {
            UserService = new UserService(UserRepository, Mapper);
            RoomActionService = new RoomActionService(RoomActionRepository, UserService, Mapper);
        }

        private void CreateApplications()
        {
            RoomActionApplication = new RoomActionApplication(RoomActionService, UserService);
        }
    }
}
