using System;

public interface IInputManager
{
    event Action<int> _inputReceived;
    void EmptyInput();
    void ClearLastInput();
    void ConfirmInput();
    void RecieveInput(string input);
}
