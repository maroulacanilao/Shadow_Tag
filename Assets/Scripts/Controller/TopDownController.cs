using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public bool isDiagonal = true;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var _input = InputManager.MoveDelta;
        
        var _movement = _input.Raw;

        if (isDiagonal && _movement.x != 0f && _movement.y != 0f)
        {
            _movement.x = 0f;
        }

        _movement = _movement.normalized;
        rb.velocity = _movement * movementSpeed;

        // Rotate the character to face the movement direction
        if (_movement != Vector2.zero)
        {
            var _angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;
            rb.rotation = _angle;
        }
    }
    
    
}
