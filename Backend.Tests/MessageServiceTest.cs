using Backend.Application.DTO.Message;
using Backend.Application.Interfaces.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;

namespace Backend.Tests;
public class MessageServiceTests
{
    private readonly DataContext _db;
    private readonly Mock<IUserContextService> _userMock;
    private readonly MessageService _service;

    private async Task<Channel> SeedChannelAsync()
    {
        Guid _channelId = Guid.NewGuid();
        var ch = new Channel { ChannelId = _channelId, OwnerId = "user1", Messages = new() { new() { Text = "Hi", OwnerId = _channelId.ToString() } } };
        _db.Channels.Add(ch);
        await _db.SaveChangesAsync();
        return ch;
    }

    public MessageServiceTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        _db = new DataContext(options);
        _userMock = new Mock<IUserContextService>();
        _service = new MessageService(_db, _userMock.Object);
    }



    [Fact]
    public async Task GetChannelMessageAsync_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        _userMock.Setup(x => x.GetUserId()).Returns(channel.OwnerId);

        // Act
        var result = await _service.GetChannelMessageAsync(channel.ChannelId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equivalent(channel.Messages, result.Data);
    }
    [Fact]
    public async Task GetChannelMessageAsync_NotValidChannelId_ReturnsFailure()
    {
        // Act
        var result = await _service.GetChannelMessageAsync(Guid.NewGuid());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.ErrorMessage);
    }


    [Fact]
    public async Task AddMessageAsync_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        _userMock.Setup(x => x.GetUserId()).Returns(channel.OwnerId);
        _userMock.Setup(x => x.GetUserName()).Returns("Josh");
        string text = "test";

        // Act
        var result = await _service.AddMessageAsync(channel.ChannelId, new CreateMessageRequest(text));

        // Assert
        Assert.True(result.IsSuccess);
        Assert.True(await _db.Messages.AnyAsync(m => m.MessageId == result.Data!.MessageId && m.Text == text));
    }
    [Fact]
    public async Task AddMessageAsync_NotValidChannelId_ReturnsFailure()
    {
        // Arrange
        string text = "test";

        // Act
        var result = await _service.AddMessageAsync(Guid.NewGuid(), new CreateMessageRequest(text));

        // Assert
        Assert.False(result.IsSuccess);
        Assert.NotEmpty(result.ErrorMessage);
    }


    [Fact]
    public async Task EditMessageAsync_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        var messageOwnerId = channel.Messages.First().OwnerId;
        _userMock.Setup(x => x.GetUserId()).Returns(messageOwnerId);
        var messageId = channel.Messages.First().MessageId;
        string text = "test";

        // Act
        var result = await _service.EditMessageAsync(messageId, new CreateMessageRequest(text));

        // Assert
        Assert.True(result.IsSuccess);
        var messageInDb = await _db.Messages.FirstAsync(m => m.MessageId == messageId);
        Assert.Equal(text, messageInDb.Text);
    }
    [Fact]
    public async Task EditMessageAsync_NotValidMessageId_ReturnsFailure()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        var messageOwnerId = channel.Messages.First().OwnerId;
        _userMock.Setup(x => x.GetUserId()).Returns(messageOwnerId);
        string text = "test";

        // Act
        var result = await _service.EditMessageAsync(Guid.NewGuid(), new CreateMessageRequest(text));

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);
    }
    [Fact]
    public async Task EditMessageAsync_NotOwnerEdits_ReturnsFailure()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        _userMock.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());
        var messageId = channel.Messages.First().MessageId;
        string text = "test";

        // Act
        var result = await _service.EditMessageAsync(messageId, new CreateMessageRequest(text));

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);
    }


    [Fact]
    public async Task RemoveMessageAsync_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        var messageId = channel.Messages.First().MessageId;
        var messageOwnerId = channel.Messages.First().OwnerId;
        _userMock.Setup(x => x.GetUserId()).Returns(messageOwnerId);

        // Act
        var result = await _service.RemoveMessageAsync(messageId);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.False(await _db.Messages.AnyAsync(m => m.MessageId == messageId));
    }
    [Fact]
    public async Task RemoveMessageAsync_NotValidMessageId_ReturnsFailure()
    {
        // Act
        var result = await _service.RemoveMessageAsync(Guid.NewGuid());

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);
    }
    [Fact]
    public async Task RemoveMessageAsync_NotOwnerEdits_ReturnsFailure()
    {
        // Arrange
        var channel = await SeedChannelAsync();
        var messageId = channel.Messages.First().MessageId;
        _userMock.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());

        // Act
        var result = await _service.RemoveMessageAsync(messageId);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);
    }
}

