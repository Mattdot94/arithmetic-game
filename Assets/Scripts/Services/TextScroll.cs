using UnityEngine;
using System.Collections;

public class TextScroll : MonoBehaviour
{
    [SerializeField] private GameObject _text;
    public Vector3 positionToMoveTo;

    internal void StartLerp(float i) => StartCoroutine(LerpPosition(positionToMoveTo, i));
    //lerp question text to target
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = _text.transform.localPosition;
        while (time < duration)
        {
            _text.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        //force position once lerped
        _text.transform.localPosition = targetPosition;
    }
}