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
        
        var _rot = _input.Raw;

        if (!isDiagonal && _rot.x != 0f && _rot.y != 0f)
        {
            _rot.x = 0f;
        }

        // Rotate the character to face the movement direction
        if (_rot != Vector2.zero)
        {
            var _angle = Mathf.Atan2(_rot.y, _rot.x) * Mathf.Rad2Deg;
            rb.rotation = _angle;
        }
        
        rb.velocity = transform.right * movementSpeed;
    }
}
