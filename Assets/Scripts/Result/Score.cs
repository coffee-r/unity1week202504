public class Score : IScore
{
    float score;

    public Score(float value)
    {
        score = value;
    }

    public float GetScore() => score;
}
