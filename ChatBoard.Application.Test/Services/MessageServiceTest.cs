using Bogus;
using ChatBoard.Application.Services;
using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Repository;
using FluentAssertions;
using Moq;

namespace ChatBoard.Application.Tests.Services
{
    public class MessageServiceTest
    {
        private readonly Faker _faker = new("pt_BR");
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IMessageRepository> _messageRepositoryMock = new();

        [Fact]
        public async Task GetMessagesByGroupIdAsync_GivenValidGroupID_ShouldReturnMessages()
        {
            // Arrange
            int groupId = _faker.Random.Int(1);

            _messageRepositoryMock
                .Setup(repo => repo.GetMessagesByGroupIdAsync(groupId))
                .ReturnsAsync(
                [
                    new () { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() },
                    new () { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() }
                ]);

            MessageService messageService = new(_unitOfWorkMock.Object, _messageRepositoryMock.Object);

            // Act
            var messages = await messageService.GetAllMessagesByGroup(groupId);

            // Assert

            _messageRepositoryMock.Verify(
                repo => repo.GetMessagesByGroupIdAsync(It.IsAny<int>()),
                Times.Once);

            messages.Should().AllSatisfy(m => m.GroupId = groupId);
        }

        [Fact]
        public async Task GetMessagesByGroupIdAsync_GivenInValidGroupID_ShouldReturnEmptyList()
        {
            // Arrange
            int groupId = _faker.Random.Int(max:0);

            _messageRepositoryMock
                .Setup(repo => repo.GetMessagesByGroupIdAsync(groupId))
                .ReturnsAsync([]);

            MessageService messageService = new(_unitOfWorkMock.Object, _messageRepositoryMock.Object);

            // Act
            var messages = await messageService.GetAllMessagesByGroup(groupId);

            // Assert
            _messageRepositoryMock.Verify(
                repo => repo.GetMessagesByGroupIdAsync(It.IsAny<int>()),
                Times.Once);

            messages.Should().HaveCount(0);
        }

        [Fact]
        public async Task AddMessageToGroup_GivenValues_ShouldAddMessageToGroup()
        {
            // Arrange
            int groupId = _faker.Random.Int(1);
            string message = _faker.Random.Words();
            string UserName = _faker.Person.FullName;
            DateTimeOffset dateTime = _faker.Date.Recent();

            Message newMessage = new()
            {
                GroupId = groupId,
                Content = message,
                UserName = UserName,
                DateTime = dateTime
            };

            _messageRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Message>()))
                .ReturnsAsync((Message msg) => msg);

            MessageService messageService = new(_unitOfWorkMock.Object, _messageRepositoryMock.Object);

            // Act
            var addedMessage = await messageService.AddMessageToGroup(groupId, message, UserName, dateTime);

            // Assert
            _messageRepositoryMock.Verify(
                repo => repo.AddAsync(It.IsAny<Message>()),
                Times.Once);

            addedMessage.GroupId.Should().Be(newMessage.GroupId);
            addedMessage.Content.Should().Be(newMessage.Content);
            addedMessage.UserName.Should().Be(newMessage.UserName);
            addedMessage.DateTime.Should().Be(newMessage.DateTime);
        }

        [Fact]
        public async Task ClearMessages_WhenMessagesExist_ShouldRemoveAllMessagesAndSaveChanges()
        {
            // Arrange
            int groupId = 1;
            var messages = new List<Message>
            {
                new () { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() },
                new () { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() },
                new () { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() }
            };

            _messageRepositoryMock
                .Setup(repo => repo.GetMessagesByGroupIdAsync(groupId))
                .ReturnsAsync(messages);

            _messageRepositoryMock
                .Setup(repo => repo.Remove(It.IsAny<Message>()));

            _unitOfWorkMock
                .Setup(uow => uow.SaveChangesAsync())
                .ReturnsAsync(1);

            var service = new MessageService(_unitOfWorkMock.Object, _messageRepositoryMock.Object);

            // Act
            await service.ClearMessages(groupId);

            // Assert
            _messageRepositoryMock.Verify(
                repo => repo.GetMessagesByGroupIdAsync(It.IsAny<int>()),
                Times.Once);

            _messageRepositoryMock.Verify(
                repo => repo.Remove(It.IsAny<Message>()),
                Times.Exactly(messages.Count));

            _unitOfWorkMock.Verify(
                uow => uow.SaveChangesAsync(),
                Times.Once);
        }

    }
}