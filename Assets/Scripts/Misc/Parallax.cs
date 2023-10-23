using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxOffset = -0.15f;
    private Camera _camera;
    private Vector2 _startPosition;
    private Vector2 _travel => (Vector2)_camera.transform.position - _startPosition;
    private void Awake()
    {
        _camera = Camera.main;
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position = _startPosition + _travel * parallaxOffset;
    }
}
