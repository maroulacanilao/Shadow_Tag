using CustomHelpers;
using Managers;
using UnityEngine;

namespace Player.ControllerState
{
    public class WallJumpState : PlayerState
    {
        public WallJumpState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }
        
        public override void Enter()
        {
            base.Enter();
            rb.ResetVelocity();
            controller.animator.SetTrigger(controller.jumpHash);
            var _xVel = StateMachine.facingLeft ? controller.wallJumpForce.x : -controller.wallJumpForce.x;
            rb.SetVelocity(_xVel , controller.wallJumpForce.y);
            StateMachine.facingLeft = !StateMachine.facingLeft;
            
            SetOrientation(StateMachine.facingLeft);
        }
        
        public override void HandleInput()
        {
            WillDash();
            if (IsPushingWall() && InputManager.JumpAction.triggered)
            {
                StateMachine.ChangeState(StateMachine.WallJumpState);
                return;
            }
        }

        public override void PhysicsUpdate()
        {
            if(IsPlayerGrounded()) return;
            
            if (!StateMachine.isGrounded && rb.velocity.y < 0 )
            {
                StateMachine.ChangeState(StateMachine.FallState);
                return;
            }
            FallUpdate();
        }
    }
}