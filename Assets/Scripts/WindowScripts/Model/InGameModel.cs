
public class InGameModel : BaseModel
{
    public int Score { get; private set; }

    public void IncreaseScore(int value)
    {
        Score += value;
    }

    public void ResetScore()
    {
        Score = 0;
    }
}
