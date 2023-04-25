using System;
using UnityEngine;

public class Main : MonoBehaviour
{
    private float GameLength = 7f; //game length
    private float _timeRemaining; //time left in game

    private float QuestionInterval = 2f; //question spawn frequency
    private float _questionIntervalTimer; //spawn countdown timer

    private GameState _gameState = GameState.Inactive;
    
    //generic services
    
    private IInputManager _inputManager;
    private IQuestionManager _questionManager;
    private IUiManager _uiManager;
    private static IDataStore _dataStore; 
    
    void Start()
    {
        //initialize services
        _dataStore = GetComponent<SessionDataStore>();
        _inputManager = GetComponent<InputManager>();
        _questionManager = GetComponent<QuestionManager>();
        _uiManager = GetComponent<UiManager>();
        
        //input listener
        _inputManager._inputReceived += AssessInput;

        _questionIntervalTimer = 0;
        _timeRemaining = GameLength;
    }

    //Game loop
    void Update()
    {
        if (_gameState == GameState.Active && _timeRemaining > 0)
        {
            _timeRemaining -= Time.deltaTime;
            //question spawn timer
            if (_questionIntervalTimer > 0)
            {
                _questionIntervalTimer -= Time.deltaTime;
            }
            else
            {
                var questionContainer = _questionManager.NextQuestionForDisplay();
                if (questionContainer != null)
                    _uiManager.SpawnQuestion(questionContainer, GameLength);
                
                _questionIntervalTimer = QuestionInterval;
            }
        }
        else if (_gameState == GameState.Active)
        {
            EndGame();
        }
    }

    //initalize game start sequence
    public void StartGame()
    {
        //print("game start");
        _uiManager.MenuesOff();
        _gameState = GameState.Active;
    }

    //initalize game end sequence
    private void EndGame()
    {
        _gameState = GameState.Inactive;
        _uiManager.ResetSpawnedQuestions();
        _questionManager.ResetQuestions();
        _timeRemaining = GameLength;
        _questionIntervalTimer = 0;
        _inputManager.EmptyInput();
        _uiManager.GameOverScreen(_dataStore.GetScore());
        _dataStore.UpdateHighScore();
        _uiManager.SetHighScoreText(_dataStore.GetHighScore());
        //print("game end");
    }
    
    // listening for input
    private void AssessInput(int i)
    {
        if (_questionManager.GetCurrentAnswer().Equals(i))
        {
            //print("Correct Answer");
            _questionManager.NextQuestion();
            _timeRemaining += QuestionInterval;
            _dataStore.AddScore(1);
            _uiManager.DestroyQuestion();
        }
        else
        {
            //print("Incorrect Answer");
            _uiManager.FlashIncorrectQuestion();
        }
    }
    //close application
    public void CloseApp() => Application.Quit();
}