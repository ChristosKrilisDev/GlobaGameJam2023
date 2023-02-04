public class ScoreManager
{
    private int _currentScore = 0;
    private int _radarLimit;
    
    
    public int CurrentScore
    {
        get => _currentScore;
        set => _currentScore = value;
    }
    
    public ScoreManager(int radarLimit)
    {
        _radarLimit = radarLimit;
        Init();
    }

    private void AddActions()
    {
        
    }

    private void Init()
    {
        CurrentScore = 0;
    }

    private void CalculateScore() //fix
    {
        _currentScore += - 1;
        // OnScoreChanged?.Invoke(1);
    }
    
}
