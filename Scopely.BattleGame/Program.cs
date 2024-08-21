using Scopely.BattleGame.Battles.Services;
using Scopely.BattleGame.LeaderBoards;
using Scopely.BattleGame.LeaderBoards.Repository;
using Scopely.BattleGame.LeaderBoards.Services;
using Scopely.BattleGame.Players;
using Scopely.BattleGame.Players.Repository;
using Scopely.BattleGame.Players.Services;
using Scopely.BattleGame.Redis;
using Scopely.BattleGame.Repositories;
using Scopely.BattleGame.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<RedisSettings>(builder.Configuration.GetSection("Redis"));

builder.Services.AddSingleton<IBattleProcessorService, BattleProcessorService>();

builder.Services.AddSingleton<IBattleService, BattleService>();
builder.Services.AddSingleton<ILeaderBoardsService, LeaderBoardsService>();
builder.Services.AddSingleton<IPlayersService, PlayersService>();

builder.Services.AddSingleton<ILeaderBoardsRepository, LeaderBoardsRepository>();
builder.Services.AddSingleton<IPlayerRepository, PlayerRepository>();
builder.Services.AddSingleton<IRedisRepository<Player>, RedisRepository<Player>>();
builder.Services.AddSingleton<IRedisRepository<LeaderBoard>, RedisRepository<LeaderBoard>>();



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
