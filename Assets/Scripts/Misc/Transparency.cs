using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Transparency : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] private float alphaValue = 0.8f;
    [SerializeField] private float transitionTime = 0.4f;

    private SpriteRenderer _spriteRenderer;
    private Tilemap _tilemap;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;
        if (_spriteRenderer)
        {
            StartCoroutine(FadeCoroutine(_spriteRenderer, transitionTime, _spriteRenderer.color.a, alphaValue));
        }
        else
        {
            StartCoroutine(FadeCoroutine(_tilemap, transitionTime, _tilemap.color.a, alphaValue));
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<PlayerController>()) return;
        if (_spriteRenderer)
        {
            StartCoroutine(FadeCoroutine(_spriteRenderer, transitionTime, _spriteRenderer.color.a, 1f));
        }
        else
        {
            StartCoroutine(FadeCoroutine(_tilemap, transitionTime, _tilemap.color.a, 1f));
        }
    }

    private IEnumerator FadeCoroutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetTransparency)
    {
        var elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            spriteRenderer.color =
                new(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
            yield return null;
        }
    }
    
    private IEnumerator FadeCoroutine(Tilemap TileMap, float fadeTime, float startValue, float targetTransparency)
    {
        var elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            var newAlpha = Mathf.Lerp(startValue, targetTransparency, elapsedTime / fadeTime);
            TileMap.color =
                new(TileMap.color.r, TileMap.color.g, TileMap.color.b, newAlpha);
            yield return null;
        }
    }
}
