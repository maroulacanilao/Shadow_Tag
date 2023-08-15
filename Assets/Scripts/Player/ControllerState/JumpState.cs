using CustomHelpers;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.ControllerState
{
    [System.Serializable]
    public class JumpState : PlayerState
    {
        private bool isHoldingButton;
        private float jumpTimer;
    
        public JumpState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }

        public override void Enter()
        {
            base.Enter();
            controller.animator.SetTrigger(controller.jumpHash);
            rb.SetVelocity(rb.velocity.x, controller.jumpForce);
            jumpTimer = 0;
            StateMachine.CurrentControllerTypeState = ControllerTypeState.Jump;
        }

        public override void HandleInput()
        {
            if(WillDash()) return;
            if(CanWallGrab()) return;
            isHoldingButton = InputManager.JumpAction.phase == InputActionPhase.Performed;
            if (IsPushingWall() && InputManager.JumpAction.triggered)
            {
                StateMachine.ChangeState(StateMachine.WallJumpState);
                return;
            }
        }

        public override void LogicUpdate()
        {
            AnimParamUpdate();

            jumpTimer += Time.deltaTime;
        
            if(jumpTimer < controller.jumpMaxTime) return;
            
            StateMachine.ChangeState(StateMachine.FallState);
        }

        public override void PhysicsUpdate()
        {
            if (!isHoldingButton) FallUpdate();

            if (!StateMachine.isGrounded && rb.velocity.y < 0 )
            {
                StateMachine.ChangeState(StateMachine.FallState);
                return;
            }
            if (IsPlayerGrounded(false) && rb.velocity.y < 0)
            {
                StateMachine.ChangeState(StateMachine.GroundedState);
            }
            MovementUpdate();
        }
    
        public override void Exit()
        {
            base.Exit();
            controller.animator.ResetTrigger(controller.jumpHash);
            rb.gravityScale = StateMachine.defaultGravityScale;
            isHoldingButton = false;
            jumpTimer = 0;
        }
    }

    [System.Serializable]
    public class FallState : PlayerState
    {
        public bool Fall;
        public FallState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }

        public override void Enter()
        {
            base.Enter();
            controller.animator.SetTrigger(controller.fallHash);
            StateMachine.CurrentControllerTypeState = ControllerTypeState.Fall;
        }

        public override void HandleInput()
        {
            if(WillDash()) return;
            if(CanWallGrab()) return;
            if (IsPushingWall())
            {
                if (InputManager.JumpAction.triggered)
                {
                    StateMachine.ChangeState(StateMachine.WallJumpState);
                    return;
                }
                StateMachine.ChangeState(StateMachine.WallSlideState);
                return;
            }
        }
    
        public override void LogicUpdate()
        {
            AnimParamUpdate();
        }
    
        public override void PhysicsUpdate()
        {
            if(IsPlayerGrounded()) return;
            MovementUpdate();
            FallUpdate();
        }
    
        public override void Exit()
        {
            base.Exit();
            controller.animator.ResetTrigger(controller.fallHash);
        }
    }
}