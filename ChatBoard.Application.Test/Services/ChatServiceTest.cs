using Bogus;
using ChatBoard.Application.DTO.Services.ChatService;
using ChatBoard.Application.Services;
using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Services;
using FluentAssertions;
using Moq;

namespace ChatBoard.Application.Tests.Services
{
    public class ChatServiceTest
    {
        private readonly Faker _faker = new("pt_BR");
        private readonly Mock<IMessageService> _messageServiceMock = new();
        private readonly Mock<IGroupService> _groupServiceMock = new();

        [Fact]
        public async Task JoinChat_GivenValidRoomName_ShouldReturnJoinChatWithGroupIdAndMessages()
        {
            // Arrange
            string roomName = _faker.Random.Word();
            int groupId = _faker.Random.Int(1);
            var messages = new List<Message>
            {
                new() { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() },
                new() { GroupId = groupId, Content = _faker.Random.Words(), UserName = _faker.Person.FullName, DateTime = _faker.Date.Recent() }
            };
            _groupServiceMock
                .Setup(service => service.GetGroupByName(roomName))
                .ReturnsAsync(groupId);
            _messageServiceMock
                .Setup(service => service.GetAllMessagesByGroup(groupId))
                .ReturnsAsync(messages);
            var chatService = new ChatService(_groupServiceMock.Object, _messageServiceMock.Object);
            // Act
            var result = await chatService.JoinChat(roomName);
            // Assert
            result.groupID.Should().Be(groupId);
            result.messages.Should().BeEquivalentTo(messages);
        }

        [Fact]
        public async Task JoinChat_GivenNewRoomName_ShouldReturnJoinChatWithGroupIdAndMessages()
        {
            // Arrange
            string roomName = _faker.Random.Word();
            int groupId = _faker.Random.Int(1);
            var messages = new List<Message>{};

            _groupServiceMock
                .Setup(service => service.GetGroupByName(roomName))
                .ReturnsAsync(0);

            _groupServiceMock
                .Setup(service => service.CreateGroup(roomName)).ReturnsAsync(new Group { Id = groupId, Name = roomName });

            _messageServiceMock
                .Setup(service => service.GetAllMessagesByGroup(groupId))
                .ReturnsAsync(messages);

            var chatService = new ChatService(_groupServiceMock.Object, _messageServiceMock.Object);
            // Act
            var result = await chatService.JoinChat(roomName);
            // Assert
            result.groupID.Should().Be(groupId);
            result.messages.Should().BeEquivalentTo(messages);
        }

        [Fact]
        public async Task AddMessageToGroup_GivenValidValues_ShouldCallMessageServiceAddMessageToGroup()
        {
            // Arrange
            int groupId = _faker.Random.Int(1);
            string message = _faker.Random.Words();
            string userName = _faker.Person.FullName;
            DateTimeOffset dateTime = _faker.Date.Recent();
            var chatService = new ChatService(_groupServiceMock.Object, _messageServiceMock.Object);
            // Act
            await chatService.AddMessageToGroup(groupId, message, userName, dateTime);
            // Assert
            _messageServiceMock.Verify(service => service.AddMessageToGroup(groupId, message, userName, dateTime), Times.Once);
        }

        [Fact]
        public async Task ClearMessages_GivenValidGroupId_ShouldCallMessageServiceClearMessages()
        {
            // Arrange
            int groupId = _faker.Random.Int(1);
            var chatService = new ChatService(_groupServiceMock.Object, _messageServiceMock.Object);
            // Act
            await chatService.ClearMessages(groupId);
            // Assert
            _messageServiceMock.Verify(service => service.ClearMessages(groupId), Times.Once);
        }

    }
}
