using Bogus;
using ChatBoard.Application.Services;
using ChatBoard.Domain.Entity;
using ChatBoard.Domain.Interface.Repository;
using FluentAssertions;
using Moq;

namespace ChatBoard.Application.Tests.Services
{
    public class GroupServiceTest
    {
        private readonly Faker _faker = new("pt_BR");
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
        private readonly Mock<IGroupRepository> _groupRepositoryMock = new();

        [Fact]
        public async Task GetGroupByName_GivenValidGroupName_ShouldReturnGroupId()
        {
            // Arrange
            string groupName = _faker.Random.Word();
            int expectedGroupId = _faker.Random.Int(1);

            _groupRepositoryMock
                .Setup(repo => repo.GetGroupByName(groupName))
                .ReturnsAsync(expectedGroupId);

            GroupService groupService = new(_unitOfWorkMock.Object, _groupRepositoryMock.Object);

            // Act
            var groupId = await groupService.GetGroupByName(groupName);

            // Assert
            groupId.Should().Be(expectedGroupId);
        }

        [Fact]
        public async Task CreateGroup_GivenValidGroupName_ShouldCreateAndReturnGroup()
        {
            // Arrange
            string groupName = _faker.Random.Word();

            _groupRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Group>()))
                .ReturnsAsync((Group group) => group);
            _unitOfWorkMock
                .Setup(uow => uow.SaveChangesAsync())
                .ReturnsAsync(1);
            GroupService groupService = new(_unitOfWorkMock.Object, _groupRepositoryMock.Object);

            // Act
            var createdGroup = await groupService.CreateGroup(groupName);

            // Assert
            createdGroup.Should().NotBeNull();
            createdGroup.Name.Should().Be(groupName);
        }   
    }
}
