using UnityEngine;

public class SessionDataStore : MonoBehaviour, IDataStore
{
    private int _score = 0;
    private int _highScore = 0;

    public void UpdateHighScore()
    {
        _highScore = _score > _highScore ? _score : _highScore;
        ResetScore();
    }
    public void AddScore(int score) => _score += score;
    public int GetScore() => _score;
    public int GetHighScore() => _highScore;
    public void ResetScore() => _score = 0;
}
