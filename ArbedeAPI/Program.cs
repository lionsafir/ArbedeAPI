using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore; // Firestore kullan�m� i�in
using Microsoft.AspNetCore.Builder.Extensions;
using System.IO;
using ArbedeAPI.Services;
using ArbedeAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// 1) serviceAccountKey.json dosyas�n�n tam yolunu belirle
var serviceAccountPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "serviceAccountKey.json");

// 2) Firestore'un kimlik do�rulamas� i�in Environment Variable ayarla
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", serviceAccountPath);

// 3) Firebase Admin SDK ba�latma
FirebaseApp.Create(new AppOptions
{
    Credential = GoogleCredential.FromFile(serviceAccountPath),
});

// 4) FirestoreDb �rne�ini DI konteynerine ekle
builder.Services.AddSingleton(provider =>
{
    // Firestore projenizin kimli�ini buraya girin
    return FirestoreDb.Create("yonet-ve-fethet");
});

// 5) Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// UnitService'i ekleyin

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();


builder.Services.AddScoped<IUnitRepository, UnitRepository>();

builder.Services.AddScoped<IRaceRepository, RaceRepository>();
builder.Services.AddScoped<IRaceService, RaceService>();

builder.Services.AddScoped<IPlayerStatsRepository, PlayerStatsRepository>();
builder.Services.AddScoped<IPlayerStatsService, PlayerStatsService>();




var app = builder.Build();

// 6) Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
