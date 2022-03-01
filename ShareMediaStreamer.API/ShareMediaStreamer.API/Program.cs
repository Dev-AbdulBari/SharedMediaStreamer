var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.MapGet("/sharedmediastreamer", () =>
{
    var greetingMessage = "Hello World";
    return greetingMessage;
});

app.Run();