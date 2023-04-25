using System;
using TMPro;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    [SerializeField] private TMP_InputField _inputField;
    
    public event Action<int> _inputReceived;    //event to trigger answer comparison in Main

    public void EmptyInput() => _inputField.text = "";
    public void ClearLastInput() => _inputField.text = _inputField.text.Remove(_inputField.text.ToCharArray().Length < 2 ? 0 : 1);
    
    //add input if length < 2
    public void RecieveInput(string input)
    {
        if (_inputField.text.ToCharArray().Length < 2)
            _inputField.text += input;
    }
    //convert answer to int and send to main
    public void ConfirmInput()
    {
        int answerToProcess;

        var valid = int.TryParse(_inputField.text, out answerToProcess);
        if (valid)
            _inputReceived?.Invoke(answerToProcess);
        else
            _inputReceived?.Invoke(0);

        EmptyInput();
    }
}