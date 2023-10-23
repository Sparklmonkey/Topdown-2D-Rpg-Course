using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float flashTime = 0.2f;

    private SpriteRenderer _spriteRenderer;
    private Material _normalMat;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalMat = _spriteRenderer.material;
    }

    public IEnumerator WhiteFlashRoutine(Action endRoutineMethod)
    {
        _spriteRenderer.material = whiteFlashMat;
        yield return new WaitForSeconds(flashTime);
        _spriteRenderer.material = _normalMat;
        endRoutineMethod();
    }
}
