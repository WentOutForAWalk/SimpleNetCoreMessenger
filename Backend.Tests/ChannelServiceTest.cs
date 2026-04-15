using Backend.Application.DTO.Channel;
using Backend.Application.Interfaces.Services;
using Backend.Infrastructure.Data;
using Backend.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Backend.Tests;
public class ChannelServiceTest
{
    [Fact]
    public async Task AddChannel_Test()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase("SimpleDb").Options;
        var context = new DataContext(options);
        context.Database.EnsureDeleted();


        var userMock = new Mock<IUserContextService>();
        userMock.Setup(x => x.GetUserId()).Returns(Guid.Empty.ToString());

        var service = new ChannelService(context, userMock.Object);

        var result = await service.AddChannelAsync(new ChannelRequest("1"));
        
        Assert.True(result.IsSuccess);
        Assert.Equal("1", result.Data!.ChannelName);

        Assert.Equal(1, context.Channels.Count());
    }
}

