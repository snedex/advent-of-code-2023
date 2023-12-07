
public class GameParser
{
    public IEnumerable<GameRecord> ParseGameLine(string gameLine)
    {
        var gameSegments = gameLine.Split(new char[] {':'}, StringSplitOptions.TrimEntries);
        var gameId = int.Parse(gameSegments[0].Replace("Game ", string.Empty));
        
        var gameRounds = gameSegments[1].Split(new char[] {';'}, StringSplitOptions.TrimEntries); 
        var rounds = new List<GameRecord>();

        foreach (var part in gameRounds)
        {
            int r = 0, g = 0, b = 0;
            foreach(var cubeDraw in part.Split(",", StringSplitOptions.TrimEntries))
            {
                if(cubeDraw.EndsWith("red"))
                {
                    r += int.Parse(cubeDraw.Replace("red", string.Empty));
                }
                if(cubeDraw.EndsWith("green"))
                {
                    g += int.Parse(cubeDraw.Replace("green", string.Empty));
                }
                if(cubeDraw.EndsWith("blue"))
                {
                    b += int.Parse(cubeDraw.Replace("blue", string.Empty));
                }
            }       
            rounds.Add(new GameRecord(gameId, r, g, b));    
        }

        return rounds;
    }
}