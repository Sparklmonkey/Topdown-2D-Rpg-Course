using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UiFade : SingletonMono<UiFade>
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeSpeed;

    private IEnumerator _fadeRoutine;

    public void StartFadeToBlack(Action endRoutineMethod)
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(1f, endRoutineMethod);
        StartCoroutine(_fadeRoutine);
    }

    public void StartFadeToClear()
    {
        if (_fadeRoutine != null)
        {
            StopCoroutine(_fadeRoutine);
        }

        _fadeRoutine = FadeRoutine(0f, null);
        StartCoroutine(_fadeRoutine);
    }
    
    private IEnumerator FadeRoutine(float targetAlpha, Action endRoutineMethod)
    {
        while (!Mathf.Approximately(fadeImage.color.a, targetAlpha))
        {
            var newAlplha = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, fadeSpeed * Time.deltaTime);
            fadeImage.color = new(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, newAlplha);
            yield return null;
        }

        endRoutineMethod?.Invoke();
    }
}
