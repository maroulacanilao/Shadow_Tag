using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using CustomHelpers;
using Player;
using UnityEngine;

public struct PlatformerMovementInfo
{
    public Vector2 position;
    public Vector2 velocity;
    public Player.ControllerState.ControllerTypeState controllerTypeState;
}

public class PlatformerMovementRecorder : MonoBehaviour
{
    [SerializeField] private PlatformController controller;
    [SerializeField] private Rigidbody2D rb;
    
    public static readonly List<PlatformerMovementInfo> movementInfoList = new List<PlatformerMovementInfo>();
    private Vector3 lastPosition;
    
    public static int lastIndex = 0;
    public static PlatformerMovementInfo lastMovementIndex => movementInfoList[lastIndex];
    
    private void Awake()
    {
        controller = GetComponent<PlatformController>();
    }

    private void OnEnable()
    {
        movementInfoList.Clear();
    }

    private void FixedUpdate()
    {
        var _transform = transform;

        var _position = _transform.position;
            
        if(_position.IsApproximatelyTo(lastPosition,0.01f)) return;
            
        movementInfoList.Add(new PlatformerMovementInfo()
        {
            position = _position,
            velocity = rb.velocity,
            controllerTypeState = controller.controllerTypeState
        });
        
        lastPosition = _position;
        
    }
}
