using Backend.Application.DTO.Channel;
using Backend.Application.Interfaces.Services;
using Backend.Domain.Models;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Backend.Tests;
public class ChannelServiceTest
{
    [Fact]
    public async Task AddChannelAsync_ValidRequest_ReturnsSuccess()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);


        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(Guid.Empty.ToString());

        var service = new ChannelService(context, userMock.Object);

        var result = await service.AddChannelAsync(new ChannelRequest("1"));

        Assert.True(result.IsSuccess);
        Assert.Equal("1", result.Data!.ChannelName);

        Assert.Equal(1, context.Channels.Count());
    }


    [Fact]
    public async Task DeleteChannelAsync_OwnerDeletes_ReturnsSuccess()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);


        Channel newChannel = new()
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = "1377",
            OwnerId = Guid.NewGuid().ToString()
        };
        context.Channels.Add(newChannel);
        await context.SaveChangesAsync();

        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(newChannel.OwnerId);

        Assert.Equal(1, context.Channels.Count());

        var service = new ChannelService(context, userMock.Object);
        var result = await service.DeleteChannelAsync(newChannel.ChannelId);

        Assert.True(result.IsSuccess);
        Assert.Equal(0, context.Channels.Count());
    }
    [Fact]
    public async Task DeleteChannelAsync_NotOwnerDeletes_ReturnsFailure()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);


        Channel newChannel = new()
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = "1377",
            OwnerId = Guid.NewGuid().ToString()
        };
        context.Channels.Add(newChannel);
        await context.SaveChangesAsync();

        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());



        var service = new ChannelService(context, userMock.Object);
        var result = await service.DeleteChannelAsync(newChannel.ChannelId);



        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);
        Assert.Equal(1, context.Channels.Count());
    }


    [Fact]
    public async Task EditChannelAsync_OwnerEdits_ReturnsSuccess()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);

        Channel newChannel = new()
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = "1377",
            OwnerId = Guid.NewGuid().ToString()
        };
        context.Channels.Add(newChannel);
        await context.SaveChangesAsync();

        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(newChannel.OwnerId);



        var service = new ChannelService(context, userMock.Object);
        var result = await service.EditChannelAsync(newChannel.ChannelId, new ChannelRequest("new"));



        Assert.True(result.IsSuccess);

        var channelInDb = await context.Channels.FindAsync(newChannel.ChannelId);
        Assert.NotNull(channelInDb);

        Assert.Equal("new", channelInDb.ChannelName);
        Assert.Equal(1, context.Channels.Count());
    }
    
    [Fact]
    public async Task EditChannelAsync_NotOwnerEdits_ReturnsFailure()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;
        var context = new DataContext(options);

        Channel newChannel = new()
        {
            ChannelId = Guid.NewGuid(),
            ChannelName = "1377",
            OwnerId = Guid.NewGuid().ToString()
        };
        context.Channels.Add(newChannel);
        await context.SaveChangesAsync();

        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(Guid.NewGuid().ToString());



        var service = new ChannelService(context, userMock.Object);
        var result = await service.EditChannelAsync(newChannel.ChannelId, new ChannelRequest("new"));



        Assert.False(result.IsSuccess);
        Assert.Equal("no permission", result.ErrorMessage);

        var channelInDb = await context.Channels.FindAsync(newChannel.ChannelId);
        Assert.NotNull(channelInDb);

        Assert.Equal("1377", channelInDb.ChannelName);
        Assert.Equal(1, context.Channels.Count());
    }

}