using System;
using System.Collections;
using System.Collections.Generic;
using CustomHelpers;
using NaughtyAttributes;
using UnityEngine;

public class GroundedCheck : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] [Tag] private string movingPlatformTag;
    [SerializeField] private bool drawSphere = true;

    private bool isGrounded;
    private bool isOnMovingPlatform;
    private int movingPlatformTagHash;
    public bool IsGrounded => isGrounded;
    public bool IsOnMovingPlatform => isOnMovingPlatform;
    public Transform GroundTransform { get; private set; }
    private void Awake()
    {
        movingPlatformTagHash = movingPlatformTag.ToHash();
    }
    private void Update()
    {
        // isGrounded = Physics2D.CircleCast(transform.position, radius, groundLayer);
        var _col = Physics2D.OverlapCircle(transform.position, radius, groundLayer);
        
        if (!_col)
        {
            isGrounded = false;
            isOnMovingPlatform = false;
            return;
        }
        isGrounded = true;
        GroundTransform = _col.transform;
        isOnMovingPlatform = _col.CompareTagHash(movingPlatformTagHash);
    }


    private void OnDrawGizmos()
    {
        if (!drawSphere) return;
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, radius);
    }
}