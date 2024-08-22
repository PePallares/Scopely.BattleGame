using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scopely.BattleGame.Battles.Services;
using Scopely.BattleGame.JWT;
using Scopely.BattleGame.JWT.Services;
using Scopely.BattleGame.LeaderBoards;
using Scopely.BattleGame.LeaderBoards.Repository;
using Scopely.BattleGame.LeaderBoards.Services;
using Scopely.BattleGame.Players;
using Scopely.BattleGame.Players.Repository;
using Scopely.BattleGame.Players.Services;
using Scopely.BattleGame.Redis;
using Scopely.BattleGame.Repositories;
using Scopely.BattleGame.Repositories.Interfaces;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("Redis"));
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddSingleton<IBattleProcessorService, BattleProcessorService>();
builder.Services.AddSingleton<IBattleService, BattleService>();
builder.Services.AddSingleton<ILeaderBoardsService, LeaderBoardsService>();
builder.Services.AddSingleton<IPlayersService, PlayersService>();
builder.Services.AddSingleton<IJWTokenService, JWTokenService>();

builder.Services.AddSingleton<ILeaderBoardsRepository, LeaderBoardsRepository>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IRedisRepository<Player>, RedisRepository<Player>>();
builder.Services.AddSingleton<IRedisRepository<LeaderBoard>, RedisRepository<LeaderBoard>>();

var jwtKey = builder.Configuration.GetSection("JWT:Key").Get<string>();

if (jwtKey is not null) 
{
    var jwtIssuer = builder.Configuration.GetSection("JWT:Issuer").Get<string>();
    var jwtAudience = builder.Configuration.GetSection("JWT:Audience").Get<string>();

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
     .AddJwtBearer(options =>
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             ValidateIssuer = true,
             ValidateAudience = true,
             ValidateLifetime = true,
             ValidateIssuerSigningKey = true,
             ValidIssuer = jwtIssuer,
             ValidAudience = jwtAudience,
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
         };
     });
}

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "My Api", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        Array.Empty<string>()
    }});
});

builder.Services.AddLogging(config =>
{
    config.AddConsole();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
