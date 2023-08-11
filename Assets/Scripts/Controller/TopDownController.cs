using Managers;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    public bool isDiagonal = true;

    private Rigidbody2D rb;
    private Vector2 lastRotation;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        lastRotation = transform.up;
    }

    private void Update()
    {
        var _input = InputManager.MoveDelta;
        
        var _rot = _input.Raw;
        
        if(isDiagonal) DiagonalMovement(_rot);
        else FourDirectionMovement(_rot); 
    }

    private void DiagonalMovement(Vector2 input_)
    {
        if (input_ != Vector2.zero)
        {
            var _angle = Mathf.Atan2(input_.y, input_.x) * Mathf.Rad2Deg;
            rb.rotation = _angle;
        }
        
        rb.velocity = transform.right * movementSpeed;
    }

    private void FourDirectionMovement(Vector2 input_)
    {
        Vector2 rotationDirection = Vector2.zero;

        if (input_.y > 0)
        {
            rotationDirection = Vector2.left;
        }
        else if (input_.y < 0)
        {
            rotationDirection = Vector2.right;
        }
        else if (input_.x > 0)
        {
            rotationDirection = Vector2.up;
        }
        else if (input_.x < 0)
        {
            rotationDirection = Vector2.down;
        }

        // Store the last rotation direction
        if (rotationDirection != Vector2.zero)
        {
            lastRotation = rotationDirection;
        }

        // Rotate the GameObject
        if (lastRotation != Vector2.zero)
        {
            var _angle = Mathf.Atan2(lastRotation.y,lastRotation.x) * Mathf.Rad2Deg;
            rb.rotation = _angle - 90f;
        }
        
        rb.velocity = transform.right * movementSpeed;
    }
}
