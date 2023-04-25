using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour, IUiManager
{
    //navigation menus
    [SerializeField] private GameObject _menuWindow;
    [SerializeField] private GameObject _gameOverWindow;
    [SerializeField] private GameObject _questionTemplate;

    //question colors
    private Color _colorBase;
    private Color _colorCorrect;
    private Color _colorIncorrect;
    
    //instantiated prefab collection
    private List<GameObject> _spawnedObjects = new List<GameObject>();
    //convert hex to unity colors
    private void Start()
    {
        ColorUtility.TryParseHtmlString("#FFFFFF", out _colorBase);
        ColorUtility.TryParseHtmlString("#00D90A", out _colorCorrect);
        ColorUtility.TryParseHtmlString("#CF0000", out _colorIncorrect);
    }
    //disable menues
    public void MenuesOff()
    {
        _menuWindow.SetActive(false);
        _gameOverWindow.SetActive(false);
    }
    //activate screen and show score
    public void GameOverScreen(int i)
    {
        _gameOverWindow.SetActive(true);
        _gameOverWindow.GetComponentInChildren<TextMeshProUGUI>().text = $"Game Over!\n You Scored {i} points";
    }
    //populate and spawn question
    public void SpawnQuestion(int[] question, float gameLength)
    {
        var questionToDisplay = Instantiate(_questionTemplate);
        questionToDisplay.GetComponentInChildren<TextMeshProUGUI>().text = $" { question[0] } + { question[1] }";
        questionToDisplay.GetComponentInChildren<TextScroll>().StartLerp(gameLength);
        //print($" { question[0] } + { question[1] }");
        _spawnedObjects.Add(questionToDisplay);
    }
    //stop and delete all spawned question
    public void ResetSpawnedQuestions()
    {
        StopAllCoroutines();

        foreach (var spawn in _spawnedObjects)
            Destroy(spawn);

        _spawnedObjects.Clear();
    }
    //set home menu highscore text
    public void SetHighScoreText(int i) => _menuWindow.GetComponentsInChildren<TextMeshProUGUI>()[1].text = $"HighScore: {i}";
    //return to home menu 
    public void ReturnToMenu()
    {
        MenuesOff();
        _menuWindow.SetActive(true);
    }
    //start question correct sequence
    public void DestroyQuestion() => StartCoroutine(CorrectSequence());
    //start question incorrect sequence
    public void FlashIncorrectQuestion() => StartCoroutine(IncorrectSequence());
 
    private IEnumerator CorrectSequence()
    {
        _spawnedObjects[0].GetComponentInChildren<TextMeshProUGUI>().color = _colorCorrect;
        _spawnedObjects[0].GetComponentInChildren<Transform>().transform.DOScale(0.1f, 0.5f);
        yield return new WaitForSeconds(0.5f);
        var destroy = _spawnedObjects[0];
        _spawnedObjects.Remove(destroy);
        Destroy(destroy);
    }

    private IEnumerator IncorrectSequence()
    {
        _spawnedObjects[0].GetComponentInChildren<TextMeshProUGUI>().color = _colorIncorrect;
        yield return new WaitForSeconds(0.5f);
        _spawnedObjects[0].GetComponentInChildren<TextMeshProUGUI>().color = _colorBase;
    }
}