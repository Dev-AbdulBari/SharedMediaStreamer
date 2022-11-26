using SharedMediaStreamer.API.Middleware;
using SharedMediaStreamer.API.Middleware.Interfaces;
using SharedMediaStreamer.API.Middleware.Repository;
using SharedMediaStreamer.API.Resolvers;
using SharedMediaStreamer.Domain;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.Domain.Models.Settings;
using SharedMediaStreamer.MediaDataProcessor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IMediaRepository, VideoRepository>();
builder.Services.AddSingleton<IMediaFileReader, VideoFileReader>();
builder.Services.AddSingleton<IMediaFileReaderResolver, MediaFileReaderResolver>();
builder.Services.AddSingleton<IRoomsRepository, RoomsRepository>();

builder.Services.Configure<MediaSettings>(builder.Configuration.GetSection("MediaSettings"));

var corsPolicyOrigin = builder.Configuration.GetSection("CorsPolicyOrigins").Value;

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder => builder.WithOrigins(corsPolicyOrigin).AllowAnyHeader().AllowAnyMethod());
});

// Add services to the container.

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseWebSockets();

app.UseMiddleware<ChatroomsMiddleware>();

app.UseEndpoints(endpoints => endpoints.MapControllers());

// Configure the HTTP request pipeline.


app.Run();