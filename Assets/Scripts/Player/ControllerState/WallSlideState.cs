using CustomHelpers;
using Managers;
using UnityEngine;

namespace Player.ControllerState
{
    public class WallSlideState : PlayerState
    {
        private readonly float defaultGravityScale;
        private readonly float defaultSpriteXPos;
        private readonly Transform spriteRendTransform;
        
        public WallSlideState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
            defaultGravityScale = stateMachine_.rb.gravityScale;
            spriteRendTransform = controller.spriteRenderer.transform;
            defaultSpriteXPos = spriteRendTransform.localPosition.x;
        }
        
        public override void Enter()
        {
            base.Enter();
            controller.animator.SetTrigger(controller.wallSlideHash);
            var _newGravity = defaultGravityScale * controller.wallSlideGravityMult;
            StateMachine.rb.gravityScale = _newGravity;
            spriteRendTransform.localPosition = spriteRendTransform.localPosition.SetX(controller.wallSlideSpriteOffsetX);
            rb.SetVelocityY(0);
        }

        public override void HandleInput()
        {
            if (!IsPushingWall())
            {
                OnNotSliding();
            }
            if(CanWallGrab()) return;
            if (InputManager.JumpAction.triggered)
            {
                StateMachine.ChangeState(StateMachine.WallJumpState);
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
            if (StateMachine.isGrounded)
            {
                StateMachine.ChangeState(StateMachine.GroundedState);
                return;
            }
        }

        public override void Exit()
        {
            base.Exit();
            controller.animator.ResetTrigger(controller.wallSlideHash);
            StateMachine.rb.gravityScale = defaultGravityScale;
            spriteRendTransform.localPosition = spriteRendTransform.localPosition.SetX(defaultSpriteXPos);
        }

        private void OnNotSliding()
        {
            if(StateMachine.isGrounded) StateMachine.ChangeState(StateMachine.GroundedState);
            else StateMachine.ChangeState(StateMachine.FallState);
        }
    }
}