public interface IDataStore
{
    void AddScore(int score);
    void UpdateHighScore();
    int GetScore();
    int GetHighScore();
    void ResetScore();
}
