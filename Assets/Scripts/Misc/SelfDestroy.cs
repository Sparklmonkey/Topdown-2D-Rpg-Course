using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (_particleSystem && !_particleSystem.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
