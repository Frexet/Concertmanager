using ConcertManagement.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddLogging();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

try
{
    ConcertManager.LoadFromFile();
}
catch (Exception ex)
{
    app.Logger.LogError(ex, "Failed to load concert data.");
    throw;
}

app.UseExceptionHandler("/error");
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "welcome.html" }
});
app.UseStaticFiles();

app.MapControllers();

app.Run();