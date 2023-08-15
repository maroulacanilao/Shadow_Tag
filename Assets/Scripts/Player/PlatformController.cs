using NaughtyAttributes;
using Player.ControllerState;
using UnityEngine;

namespace Player
{
    public class PlatformController : UnitController
    {
        #region Movement Properties


        [field: Header("Movement Properties")] 
        [field: SerializeField] public float movementSpeed { get; private set; } = 8;
        [field: SerializeField] public float acceleration { get; private set; } = 6;
        [field: SerializeField] public float boredWaitTime { get; private set; } = 5f;
        [field: SerializeField] public float currentMoveLerpSpeed { get; private set; } = 100;


        #endregion
    
        #region Jump Properties

        [Header("Jump Physics Properties")] 
        
        [SerializeField] public float jumpForce = 16;
        [SerializeField] public float fallMult = 4;
        [SerializeField] public float jumpVelFallOff = 8;

        [Header("Jump General Properties")] 
        [SerializeField] public float jumpBufferTime = 0.1f;
        [SerializeField] public float coyoteTime = 0.1f;
        [SerializeField] public float jumpMaxTime = 0.35f;

        #endregion

        #region Dash Properties

        [field: Header("Dash Properties")] 
        [field: SerializeField] public Vector2 dashForce { get; private set; }
        [field: SerializeField] public float dashDuration { get; private set; } = 0.1f;
        [field: SerializeField] public float dashCooldown { get; private set; } = 0.5f;
    
        #endregion

        #region Wall Slide

        [field: Header("Wall Slide Properties")]
        [field: SerializeField] public float wallSlideGravityMult { get; private set; } = 0.5f;
        [field: SerializeField] public Vector2 wallJumpForce { get; private set; } = new Vector2(10, 20);
        [field: SerializeField] public float wallSlideSpriteOffsetX { get; private set; } = 0.41f;
        [field: SerializeField] public float maxWallGrabTime { get; private set; } = 2f;

        #endregion
    
        #region Animation Hash
        
        [field: Header("Animation Hash")]

        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int xSpeedHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int groundedHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int jumpHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int fallHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int attackHash { get; private set; }

        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int dashHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int wallSlideHash { get; private set; }
    
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int runningAttackHash { get; private set; }
        
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int hitHash { get; private set; }
        
        [field: AnimatorParam("animator")] [field: Foldout("Animation Hash")]
        [field: SerializeField]  public int wallGrabHash { get; private set; }
    
        #endregion

        #region Animation Event
        
        [field: Foldout("Animation Event")]
        [field: SerializeField]  public string attackAnimEndEvent { get; private set; }
        
        [field: Foldout("Animation Event")]
        [field: SerializeField]  public string hitAnimEndEvent { get; private set; }
        
        #endregion

        public ControllerTypeState controllerTypeState
        {
            get
            {
                var _playerStateMachine = StateMachine as PlayerStateMachine;
                if(_playerStateMachine == null) return 0;
                return _playerStateMachine.CurrentControllerTypeState;
            }
        }

        private void Awake()
        {
            StateMachine = new PlayerStateMachine(this);
        }

        private void OnEnable()
        {
            StateMachine.Initialize();
        }
    }
}
