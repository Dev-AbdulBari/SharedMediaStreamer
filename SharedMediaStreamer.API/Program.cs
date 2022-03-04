using SharedMediaStreamer.Domain;
using SharedMediaStreamer.Domain.Interfaces;
using SharedMediaStreamer.MediaDataProcessor;
using SharedMediaStreamer.MediaDataProcessor.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IMediaRepository, VideoRepository>();
builder.Services.AddSingleton<IMediaFileReader, VideoFileReader>();

// Add services to the container.

var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapControllers());

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.Run();