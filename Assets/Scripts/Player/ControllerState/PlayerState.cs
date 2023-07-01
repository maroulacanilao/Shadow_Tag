using CustomHelpers;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.ControllerState
{
    public abstract class PlayerState : UnitState
    {
        protected readonly PlayerStateMachine StateMachine;
        protected readonly PlatformController controller;

        protected readonly Rigidbody2D rb;
        protected readonly int animEndEventHash;

        protected PlayerState(PlayerStateMachine stateMachine_, string animEndEventId_ = null)
        {
            StateMachine = stateMachine_;
            controller = stateMachine_.controller;
            rb = stateMachine_.rb;
            animEndEventHash = animEndEventId_ == null ? 0 : animEndEventId_.ToHash();
            controller.animationEventInvoker.OnAnimationEvent.AddListener(AnimEnd);
        }

        public virtual void HandleInput() { }

        protected virtual void OnAnimationEnd() {}
    
        protected void AnimationEnd(string animEventId_)
        {
            if(animEventId_.ToHash() != animEndEventHash) return;

            OnAnimationEnd();
        }

        protected void FallUpdate()
        {
            if(StateMachine.isGrounded) return;
            var _fallMult = StateMachine.playerVel.y < 0 ? controller.jumpVelFallOff : controller.fallMult;
            rb.velocity += Vector2.up * (_fallMult * Physics2D.gravity.y * Time.deltaTime);
        }

        public void AnimParamUpdate(bool useInput_ = true)
        {
            var _isIdle = useInput_ ? IsInputIdle() : StateMachine.input.x== 0;
            
            controller.animator.SetFloat(controller.xSpeedHash, Mathf.Abs(StateMachine.input.x));

            var _prevFacingLeft = StateMachine.facingLeft;
            var _xVal = useInput_ ? StateMachine.input.x : StateMachine.playerVel.x;
            StateMachine.facingLeft = _isIdle ? StateMachine.facingLeft : StateMachine.input.x < 0;
        
            if(_prevFacingLeft == StateMachine.facingLeft) return;
            
            SetOrientation(StateMachine.facingLeft);
        }
        
        protected void AnimEnd(string eventId_)
        {
            if(eventId_.ToHash() != animEndEventHash) return;
            OnAnimEnd();
        }
        
        protected virtual void OnAnimEnd() {}

        protected void SetOrientation(bool isFacingLeft_)
        {
            var _scale = controller.transform.localScale;
            _scale.x = Mathf.Abs(_scale.x);
        
            controller.transform.localScale = isFacingLeft_ ? 
                new Vector3(-_scale.x, _scale.y, _scale.z) 
                : new Vector3(_scale.x, _scale.y, _scale.z);
        }

        protected void MovementUpdate()
        {
            Vector3 newVelocity = new Vector3(StateMachine.input.x * StateMachine.currentMoveSpeed, rb.velocity.y);
        
            rb.velocity = Vector2.Lerp
            (StateMachine.playerVel, newVelocity, 
                controller.currentMoveLerpSpeed * Time.deltaTime);
        }

        protected bool CanJump()
        {
            if (Time.time > StateMachine.timeLastJumpPressed + controller.jumpBufferTime) return false;
            if (Time.time > StateMachine.timeLastJumpPressed + controller.coyoteTime) return false;
        
            return true;
        }
    
        protected bool IsPlayerFalling()
        {
            if(StateMachine.isGrounded) return false;
        
            StateMachine.ChangeState(StateMachine.FallState);
            return true;
        }

        protected bool WillDash()
        {
            if(StateMachine.hasDashed) return false;
            if (!InputManager.DashAction.triggered) return false;
            if (Time.time < StateMachine.timeLastDashed + controller.dashCooldown)
            {
                return false;
            }
        
            StateMachine.ChangeState(StateMachine.DashState);
            return true;
        }

        protected bool IsOnMovingPlatform()
        {
            if (!StateMachine.isOnMovingPlatform)
            {
                if (!StateMachine.isOnRoot)
                {
                    ControllerResetParent();
                }
                SetRigidBodyInterpolation(RigidbodyInterpolation2D.Interpolate);
                return false;
            }

            SetRigidBodyInterpolation(IsInputIdle() ? RigidbodyInterpolation2D.Interpolate : RigidbodyInterpolation2D.Extrapolate);

            if (!StateMachine.isOnRoot) return true;
            
            controller.transform.SetParent(controller.collisionDetector.GroundTransform);
            StateMachine.isOnRoot = false;
            return true;
        }

        protected void ControllerResetParent()
        {
            controller.transform.SetParent(null);
            StateMachine.isOnRoot = true;
        }

        protected bool IsInputIdle()
        {
            return StateMachine.input.x.IsApproximatelyTo(0);
        }
        
        protected void SetRigidBodyInterpolation(RigidbodyInterpolation2D interpolation_)
        {
            if (rb.interpolation == interpolation_) return;
            rb.interpolation = interpolation_;
        }

        protected bool IsPushingWall()
        {
            if (!controller.collisionDetector.isTouchingWall)
            {
                // Debug.LogError("Not Touching Wall");
                return false;
            }
            
            var _xInput = StateMachine.input.x;

            if (_xInput == 0) 
            {
                // Debug.LogError("input is approximately 0");
                return false;
            }

            if (StateMachine.facingLeft && _xInput > 0 || !StateMachine.facingLeft && _xInput < 0)
            {
                // Debug.LogError($"Wrong Input Direction== left: {StateMachine.facingLeft} input: {_xInput}");
                return false;
            }

            return true;
        }

        protected bool IsPlayerGrounded(bool willChangeState_ = true)
        {
            if (!StateMachine.isGrounded) return false;
            if(willChangeState_) StateMachine.ChangeState(StateMachine.GroundedState);
            return true;
        }

        protected bool CanWallGrab(bool willChangeState_ = true)
        {
            if (InputManager.GrabAction.phase != InputActionPhase.Started &&
                InputManager.GrabAction.phase != InputActionPhase.Performed)
            {
                return false;
            }
            
            if (!IsPushingWall()) return false;
            
            if(willChangeState_) StateMachine.ChangeState(StateMachine.WallGrabState);
            return true;
        }

        protected void DefaultState()
        {
            if(StateMachine.isGrounded) StateMachine.ChangeState(StateMachine.GroundedState);
            else StateMachine.ChangeState(StateMachine.FallState);
        }
    }
}
 
 