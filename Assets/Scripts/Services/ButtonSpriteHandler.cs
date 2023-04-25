using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSpriteHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite _buttonRest;
    [SerializeField] private Sprite _buttonPressed;

    private Image _thisImage;
    
    private void Start() => _thisImage = GetComponent<Image>();
    public void OnPointerDown(PointerEventData eventData) => _thisImage.sprite = _buttonPressed;
    public void OnPointerUp(PointerEventData eventData) => _thisImage.sprite = _buttonRest;
}