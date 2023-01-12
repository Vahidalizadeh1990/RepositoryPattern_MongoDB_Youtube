using MainApp.Data.MusicRepo;
using MainApp.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Add connection string
builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Register mongoDb configuration as a singleton object
builder.Services.AddSingleton<IMongoDatabase>(options=>{
    var settings= builder.Configuration.GetSection("MongoDBSettings").Get<MongoDbSettings>();
    var client = new MongoClient(settings.ConnectionString);
    return client.GetDatabase(settings.DatabaseName);
});

// Register music service
builder.Services.AddSingleton<IMusicService, MusicService>();

// Register autoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
