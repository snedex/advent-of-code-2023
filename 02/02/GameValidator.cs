


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

    public (int requiredRed, int requiredGreen, int requiredBlue) MinCubesToPlay(IEnumerable<GameRecord> rounds)
    {
       int requiredRed = 0, requiredGreen = 0, requiredBlue = 0;

       foreach(var round in rounds)
       {
            if(round.RedCubes > requiredRed)
            {
                requiredRed = round.RedCubes;
            }
            if(round.GreenCubes > requiredGreen)
            {
                requiredGreen = round.GreenCubes;
            }
            if(round.BlueCubes > requiredBlue)
            {
                requiredBlue = round.BlueCubes;
            }
       }

       return (requiredRed, requiredGreen, requiredBlue);
    }
}