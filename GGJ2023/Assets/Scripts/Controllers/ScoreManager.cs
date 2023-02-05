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
    

    private void Init()
    {
        CurrentScore = 0;
    }

    public void IncreaseScore()
    {
        _currentScore += 1;
        GameController.Instance.OnScoreChange?.Invoke(_currentScore);
    }
    
    public void IncreaseScoreValue(int value)
    {
        _currentScore += value;
        GameController.Instance.OnScoreChange?.Invoke(_currentScore);

    }
    
    public void DecreaseScore()
    {
        _currentScore -= 1;
        GameController.Instance.OnScoreChange?.Invoke(_currentScore);

    }
    
    public void DecreaseScoreValue(int value)
    {
        _currentScore -= value;
        GameController.Instance.OnScoreChange?.Invoke(_currentScore);

    }
    
}
