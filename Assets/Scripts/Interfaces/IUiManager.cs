public interface IUiManager
{
    void MenuesOff();
    void GameOverScreen(int i);
    void ReturnToMenu();
    void DestroyQuestion();
    void SpawnQuestion(int[] question, float gameLength);
    void ResetSpawnedQuestions();
    void FlashIncorrectQuestion();
    void SetHighScoreText(int i);
}