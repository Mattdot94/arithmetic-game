using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour, IQuestionManager
{
    private List<int[]> Questions = new List<int[]>();

    private int _displayCount = 0; // index for display spawning
    private int _progressCount = 0; //index for actual progress
    private int _questionCount = 25; //size of question batch 

    private int[] CurrentQuestion; //current question values for display
    private int CurrentAnswer; //current answer for validation
    
    //generate questions and set first answer
    void Start()
    {
        Questions = GenerateNewQuestions();
        NextQuestion();
    }
    //get answer
    public int GetCurrentAnswer() => CurrentAnswer;
    //get question
    public int[] GetCurrentQuestion() => CurrentQuestion;

    //Generate a set of questions
    public List<int[]> GenerateNewQuestions()
    {
        var newQuestions = new List<int[]>();

        while (newQuestions.Count < _questionCount)
        {
            int firstValue = Random.Range(1, 9);
            int secondValue = Random.Range(1, 9);
            var question = new int[] { firstValue, secondValue };
            newQuestions.Add(question);
        }

        return newQuestions;
    }
    //Next question to answer
    public void NextQuestion()
    {
        CurrentQuestion = Questions[_progressCount++];
        CurrentAnswer = CurrentQuestion[0] + CurrentQuestion[1];
    }
    //Next question to begin lerping
    public int[] NextQuestionForDisplay()
    {
        if (_displayCount >= _questionCount)
        {
            _questionCount += _questionCount;
            Questions.AddRange(GenerateNewQuestions());
            //print("added new questions");
        }

        return Questions[_displayCount++];
    }
    //regenerate question list, 
    public void ResetQuestions()
    {
        _displayCount = 0;
        _progressCount = 0;
        Questions.Clear();
        Questions = GenerateNewQuestions();

        NextQuestion();
    }
}