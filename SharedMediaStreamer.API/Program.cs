using SharedMediaStreamer.API.Middleware;
using SharedMediaStreamer.API.Middleware.Interfaces;
using SharedMediaStreamer.API.Middleware.Repository;
using SharedMediaStreamer.API.Resolvers;
using SharedMediaStreamer.Domain;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models.Settings;
using SharedMediaStreamer.MediaDataProcessor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Allow-All",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader();
        });
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IMediaRepository, VideoRepository>();
builder.Services.AddSingleton<IMediaFileReader, VideoFileReader>();
builder.Services.AddSingleton<IMediaFileReaderResolver, MediaFileReaderResolver>();
builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();

builder.Services.Configure<MediaSettings>(builder.Configuration.GetSection("MediaSettings"));

// Add services to the container.

var app = builder.Build();

app.UseRouting();

app.UseCors("Allow-All");

app.UseWebSockets();

app.UseMiddleware<ChatroomsMiddleware>();

app.UseEndpoints(endpoints => endpoints.MapControllers());

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();