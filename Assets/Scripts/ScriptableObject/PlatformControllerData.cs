using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlatformControllerData", fileName = "New PlatformControllerData")]
public class PlatformControllerData : ScriptableObject
{
    [field: Header("Movement Properties")] 
    [field: SerializeField] public float movementSpeed { get; private set; } = 8;
    [field: SerializeField] public float acceleration { get; private set; } = 6;
    [field: SerializeField] public float boredWaitTime { get; private set; } = 5f;
    [field: SerializeField] public float currentMoveLerpSpeed { get; private set; } = 100;
    
    [field: Header("Jump Physics Properties")]
    [field: SerializeField] public float jumpForce { get; private set; } = 16;
    [field: SerializeField] public float fallMult { get; private set; } = 4;
    [field: SerializeField] public float jumpVelFallOff { get; private set; } = 8;
    
    [field: Header("Jump General Properties")] 
    [field: SerializeField] public float jumpBufferTime { get; private set; } = 0.1f;
    [field: SerializeField] public float coyoteTime { get; private set; } = 0.1f;
    [field: SerializeField] public float jumpMaxTime { get; private set; } = 0.35f;
    
    [field: Header("Dash Properties")] 
    [field: SerializeField] public float dashSpeed { get; private set; } = 30f;
    [field: SerializeField] public float dashDuration { get; private set; } = 0.1f;
    [field: SerializeField] public float dashCooldown { get; private set; } = 0.5f;
    
    public float timeLastDashed { get; set; }
    public float defaultGravityScale { get; set; }
    public float timeLastJumpPressed { get; set; }
    public bool isOnRoot { get; set; }
    
    public float currentAcceleration { get; set; }
    public float currentMoveSpeed { get; set; }

    public bool facingLeft { get; set; }
}
