namespace Scopely.BattleGame.JWT.Services
{
    public  interface IJWTokenService
    {
        Task<string> GetJWToken(string playerId);
    }
}
