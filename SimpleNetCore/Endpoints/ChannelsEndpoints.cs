using MiniValidation;
using SimpleNetCore.DTO.Channel;
using SimpleNetCore.DTO.Message;

namespace SimpleNetCore.Endpoints;


public static class ChannelsEndpoints
{
    private static List<ChannelDto> Channels = new();
    

    public static void MapChannelsEndpoints(this WebApplication app)
    {
        var api = app.MapGroup("a");

        var channels = api.MapGroup("channels");



        // Удаляет канал
        //channels.MapDelete("/{id:guid}", (Guid id) =>
        //{
        //    if (Channels.FindIndex(c => c.ChannelId == id) is var index and not -1)
        //    {
        //        Channels.RemoveAt(index);
        //        return Results.Ok();
        //    }
        //    return Results.NotFound();
        //});
        
        
        
        
        // ОТДАЁТ WWWROOT ФАЙЛ
        app.MapGet("/", (IWebHostEnvironment env) =>
            Results.File(Path.Combine(env.WebRootPath, "index.html"), "text/html"));
        //app.MapGet("/", () => "Hello ma niggas");




        // Выводит Сообщения канала
        channels.MapGet("/{id:guid}/messages", (Guid id) =>
            Channels.FirstOrDefault(c => c.ChannelId == id) is { } channel
                ? Results.Ok(channel)
                : Results.NotFound("channel not found"));


        // Создаёт сообщения в канале
        channels.MapPost("/{id:guid}/messages", (Guid id, CreateMessageRequest request) =>
        {
            // Валидация данных
            if (!MiniValidator.TryValidate(request, out var errors))
            {
                return Results.ValidationProblem(errors);
            }



            if (Channels.FirstOrDefault(c => c.ChannelId == id) is not { } channel)
                return Results.NotFound();

            MessageDto messageDto = new MessageDto
            {
                SenderName = request.SenderName,
                Text = request.Text,
            };

            channel.Messages.Add(messageDto);

            return Results.Ok(messageDto);
        });

        // Редактирует сообщение
        channels.MapPut("/{channelId:guid}/messages/{messageId:guid}", (Guid channelId, Guid messageId, CreateMessageRequest request) =>
        {
            // Валидация данных
            if (!MiniValidator.TryValidate(request, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            if (Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { } channel)
                return Results.NotFound();

            if (channel.Messages.FindIndex(c => c.MessageId == messageId) is var index and not -1)
            {
                channel.Messages[index].SenderName = request.SenderName;
                channel.Messages[index].Text = request.Text;

                return Results.Ok();
            }

            return Results.NotFound();
        });

        // Удаляет сообщение
        channels.MapDelete("/{channelId:guid}/messages/{messageId:guid}", (Guid channelId, Guid messageId) =>
        {
            if (Channels.FirstOrDefault(c => c.ChannelId == channelId) is not { } channel)
                return Results.NotFound();

            if (channel.Messages.FindIndex(c => c.MessageId == messageId) is var index and not -1)
            {
                channel.Messages.RemoveAt(index);
                return Results.Ok();
            }
            return Results.NotFound();
        });





    }





}

