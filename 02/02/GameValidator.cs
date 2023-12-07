

public class GameValidator
{
    private int RedCubes { get; set; }

    private int BlueCubes { get; set; }

    private int GreenCubes { get; set; }

    public GameValidator(int redCubes, int greenCubes, int blueCubes)
    {
        RedCubes = redCubes;
        GreenCubes = greenCubes;
        BlueCubes = blueCubes;
    }

    public bool IsGameValid(IEnumerable<GameRecord> gameRounds)
    {
        foreach(var gameRound in gameRounds)
        {
            if(gameRound.RedCubes > RedCubes)
            {    
                return false;
            }

            if(gameRound.GreenCubes > GreenCubes)
            {    
                return false;
            }

            if(gameRound.BlueCubes > BlueCubes)
            {    
                return false;
            }
        }
        
        return true;
    }
}