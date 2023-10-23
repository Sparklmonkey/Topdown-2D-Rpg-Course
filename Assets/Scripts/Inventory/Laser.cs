using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private CapsuleCollider2D _capsuleCollider;
    private float _range, _speed, _capsuleSizeMultiplier; 
    private Vector2 _size, _startSize, _capsuleSize, _capsuleStartSize, _capsuleOffset, _capsuleStartOffset;
    private bool _isGrowing;

    public void SetRangeAndSpeed(float range, float speed)
    {
        _range = range;
        _speed = speed;
        _isGrowing = true;
        StartCoroutine(ExpandLaser());
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _size = _startSize = _spriteRenderer.size;
        _capsuleSize = _capsuleStartSize = _capsuleCollider.size;
        _capsuleOffset = _capsuleStartOffset = _capsuleCollider.offset;
        _capsuleSizeMultiplier =  _capsuleSize.x / _size.x;
    }

    private void Start()
    {
        FaceMouse();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Indestructible>())
        {
            _isGrowing = false;
        }
    }

    private IEnumerator ExpandLaser()
    {
        yield return null;

        float timePassed = 0;

        while (_size.x < _range && _isGrowing)
        {
            timePassed += Time.deltaTime;
            var linerT = timePassed / _speed;
            _size.x = Mathf.Lerp(_startSize.x, _range, linerT);
            _capsuleSize.x = _size.x * _capsuleSizeMultiplier;
            _capsuleOffset.x = (_size.x * _capsuleStartOffset.x) / _startSize.x;

            _spriteRenderer.size = _size;
            _capsuleCollider.size = _capsuleSize;
            _capsuleCollider.offset = _capsuleOffset;
        
            yield return null;
        }

        var myTransform = transform;
        myTransform.position = new Vector3(myTransform.position.x + myTransform.right.x * _range, 
            myTransform.position.y + myTransform.right.y * _range, myTransform.position.z);
        myTransform.right = -myTransform.right;


        timePassed = 0;
        while (_size.x > _startSize.x)
        {
            timePassed += Time.deltaTime;
            var linerT = timePassed / _speed;
            _size.x = Mathf.Lerp(_range, _startSize.x, linerT);
            _capsuleSize.x = _size.x * _capsuleSizeMultiplier;
            _capsuleOffset.x = (_size.x * _capsuleStartOffset.x) / _startSize.x;

            _spriteRenderer.size = _size;
            _capsuleCollider.size = _capsuleSize;
            _capsuleCollider.offset = _capsuleOffset;
        
            yield return null;
        }

        Destroy(gameObject);
    }
    private void FaceMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = transform.position - mousePos;
        transform.right = -direction;
    }
}
