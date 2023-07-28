using CustomHelpers;
using NaughtyAttributes;
using UnityEngine;

public class MovementCollisionDetector : MonoBehaviour
{
    [Header("Ground")]
    [SerializeField] private Vector2[] groundOffsetArr;
    [SerializeField] private float groundDist;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] [Tag] private string movingPlatformTag;
    [SerializeField] private bool updateGrounded = true;

    [Header("Edge")]
    [SerializeField] private Vector2 edgeGroundOffset;
    [SerializeField] private float edgeGroundCheckDist = 0.05f;
    [SerializeField] private bool updateEdge = true;

    [Header("Wall")]
    [SerializeField] private Vector2 wallOffset;
    [SerializeField] private float wallCheckDist = 0.05f;
    [SerializeField] private bool useExcludeLayer = true;
    [SerializeField] [ShowIf("useExcludeLayer")] LayerMask wallExcludeLayer;
    [SerializeField] [HideIf("useExcludeLayer")] LayerMask wallLayer;
    [SerializeField] private bool updateWall = true;

    [Header("DrawGizmo")]
    public bool showGizmo = true;

    private int movingPlatformTagHash;

    public Transform GroundTransform { get; private set; }
    public bool isOnEdge { get; private set; }
    public bool isTouchingWall { get; private set; }
    public bool isGrounded { get; private set; }
    public bool isOnMovingPlatform { get; private set; }
    
    private Transform owner => transform.parent;

    private void Awake()
    {
        movingPlatformTagHash = movingPlatformTag.ToHash();
        
    }

    private void Update()
    {
        Physics2D.queriesHitTriggers = false;
        if (updateGrounded) isGrounded = IsGrounded();
        if (updateWall) isTouchingWall = IsTouchingWall();
        if (updateEdge) isOnEdge = IsOnEdge();
    }
    

    public bool IsOnEdge()
    {
        return !Physics2D.Raycast(transform.position + owner.GetOffsetOrientation(edgeGroundOffset), Vector2.down, edgeGroundCheckDist, groundLayer);
    }

    public bool IsTouchingWall()
    {
        var _dir = owner.localScale.x >0 ? Vector2.right : Vector2.left;
        if (useExcludeLayer)
        {
            return Physics2D.Raycast(transform.position + owner.GetOffsetOrientation(wallOffset), 
                _dir, wallCheckDist, ~wallExcludeLayer);
        }
        
        return Physics2D.Raycast(transform.position + owner.GetOffsetOrientation(wallOffset), 
            _dir, wallCheckDist, wallLayer);
    }

    public bool IsGrounded()
    {
        foreach (var _offset in groundOffsetArr)
        {
            var _hit = Physics2D.Raycast(transform.position + (Vector3) _offset, 
                Vector2.down, edgeGroundCheckDist, groundLayer);
            
            if (!_hit.collider)
            {
                continue;
            }
            
            var _col = _hit.collider;
            isGrounded = true;
            GroundTransform = _col.transform;
            isOnMovingPlatform = _col.CompareTagHash(movingPlatformTagHash);
            return true;
        }
        
        isGrounded = false;
        isOnMovingPlatform = false;
        return false;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmo) return;
        var _pos = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawRay(_pos + owner.GetOffsetOrientation(edgeGroundOffset), Vector2.down * edgeGroundCheckDist);

        Gizmos.color = Color.magenta;
        var _dir = owner.localScale.x >0 ? Vector2.right : Vector2.left;
        Gizmos.DrawRay(_pos + owner.GetOffsetOrientation(wallOffset), _dir * wallCheckDist);
        
        Gizmos.color = Color.blue;

        foreach (var _offset in groundOffsetArr)
        {
            Gizmos.DrawRay(transform.position + (Vector3) _offset ,Vector2.down * groundDist);
        }
    }
}
