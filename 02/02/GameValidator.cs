

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
        //The sum of the cubes drawn must not exceed the setup of the game
        if(gameRounds.Sum(r => r.RedCubes) > RedCubes)
        {    
            return false;
        }

        if(gameRounds.Sum(r => r.GreenCubes) > GreenCubes)
        {    
            return false;
        }

        if(gameRounds.Sum(r => r.BlueCubes) > BlueCubes)
        {    
            return false;
        }

        return true;
    }
}