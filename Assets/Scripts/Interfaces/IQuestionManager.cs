using System.Collections.Generic;

public interface IQuestionManager
{
    int GetCurrentAnswer();
    int[] GetCurrentQuestion();
    List<int[]> GenerateNewQuestions();
    void NextQuestion();
    int[] NextQuestionForDisplay();
    void ResetQuestions();
}