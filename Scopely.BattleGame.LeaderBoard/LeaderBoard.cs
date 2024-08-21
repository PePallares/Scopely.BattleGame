namespace Scopely.BattleGame.LeaderBoards
{
    public class LeaderBoard
    {
        public required string Name { get; set; }
        public IEnumerable<LeaderBoardPlayer> LeaderBoardRanking { get; set; }

        public LeaderBoard() 
        {
            LeaderBoardRanking = [];
        }
    }
}
