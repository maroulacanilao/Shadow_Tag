using Managers;
using Player.ControllerState;
using UnityEngine;

namespace Player
{
    [System.Serializable]
    public class PlayerStateMachine : UnitStateMachine
    {
        public Rigidbody2D rb { get; private set; }
        public PlatformController controller { get; private set; }

        public Vector2 playerVel
        {
            get => rb.velocity;
            set => rb.velocity = value;
        }

        public Vector2 input;
        public Vector2 inputNormalized => input.normalized;
        public bool isGrounded => controller.collisionDetector.isGrounded;
        public bool isOnMovingPlatform => controller.collisionDetector.isOnMovingPlatform;
    
        public float timeLastDashed;
        public float defaultGravityScale;
        public float timeLastJumpPressed;
        public bool isOnRoot;
        public bool hasDashed;
    
        public float currentAcceleration { get; private set; }
        public float currentMoveSpeed { get; private set; }

        public bool facingLeft;

        #region States

        public GroundedState GroundedState { get; private set; }
        public JumpState JumpState { get; private set; }
        public FallState FallState { get; private set; }
    
        public DashState DashState { get; private set; }

        public WallSlideState WallSlideState { get; private set; }

        public WallJumpState WallJumpState { get; private set; }
        
        public HitState HitState { get; private set; }
        
        public WallGrabState WallGrabState { get; private set; }


        #endregion
        
        private PlayerState CurrentPlayerState => CurrentState as PlayerState;
        
        public PlayerStateMachine(PlatformController controller_)
        {
            controller = controller_;
            rb = controller_.rb;
            defaultGravityScale = rb.gravityScale;
            isOnRoot = true;
        }

        public override void Initialize()
        {
            timeLastJumpPressed = -1;
        
            GroundedState = new GroundedState(this);
            JumpState = new JumpState(this);
            FallState = new FallState(this);
            DashState = new DashState(this);
            WallSlideState = new WallSlideState(this);
            WallJumpState = new WallJumpState(this);
            HitState = new HitState(this, controller.hitAnimEndEvent);
            WallGrabState = new WallGrabState(this);

            CurrentState = GroundedState;
            CurrentState.Enter();
        }

        public override void StateUpdate()
        {
            InputUpdate();
            CurrentPlayerState.HandleInput();
            CurrentState.LogicUpdate();
        }
    
        public override void StateFixedUpdate()
        {
            CurrentState.PhysicsUpdate();
        }

        private void InputUpdate()
        {
            input = InputManager.MoveDelta.Raw;

            currentAcceleration = isGrounded ? controller.acceleration : controller.acceleration / 2f;
            currentMoveSpeed = isGrounded ? controller.movementSpeed : controller.movementSpeed / 2f;
        
            // input smoothing
            switch (input.x)
            {
                case < 0:
                {
                    if (playerVel.x > 0) input.x = 0;
                    input.x = Mathf.Lerp(input.x, -1, currentAcceleration * Time.deltaTime);
                    break;
                }
                case > 0:
                {
                    if (input.x < 0) input.x = 0;
                    input.x = Mathf.Lerp(input.x, 1, currentAcceleration * Time.deltaTime);
                    break;
                }
                default:
                {
                    input.x = Mathf.Lerp(input.x, 0, currentAcceleration * Time.deltaTime);
                    break;
                }
            }

            // jump input
            if (InputManager.JumpAction.triggered)
            {
                timeLastJumpPressed = Time.time;
            }
        }
    }
}
