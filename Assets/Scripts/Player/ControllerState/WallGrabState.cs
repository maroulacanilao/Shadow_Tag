using CustomHelpers;
using Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.ControllerState
{
    public class WallGrabState : PlayerState
    {
        private readonly float defaultGravityScale;
        private readonly float defaultSpriteXPos;
        private readonly Transform spriteRendTransform;
        private float wallGrabTimer;
        
        public WallGrabState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
            defaultGravityScale = stateMachine_.rb.gravityScale;
            spriteRendTransform = controller.spriteRenderer.transform;
            defaultSpriteXPos = spriteRendTransform.localPosition.x;
        }
        
        public override void Enter()
        {
            base.Enter();
            controller.animator.SetTrigger(controller.wallGrabHash);
            controller.rb.velocity = Vector2.zero;
            controller.rb.gravityScale = 0;
            spriteRendTransform.localPosition = spriteRendTransform.localPosition.SetX(controller.wallSlideSpriteOffsetX);
            wallGrabTimer = 0;
            StateMachine.CurrentControllerTypeState = ControllerTypeState.WallGrab;
        }
        
        public override void HandleInput()
        {
            if(InputManager.GrabAction.phase != InputActionPhase.Started &&
               InputManager.GrabAction.phase != InputActionPhase.Performed) DefaultState();

            if (!InputManager.JumpAction.triggered) return;

            StateMachine.ChangeState(StateMachine.WallJumpState);
        }
        
        public override void LogicUpdate()
        {
            wallGrabTimer += Time.deltaTime;
            
            if(wallGrabTimer >= controller.maxWallGrabTime)
            {
                DefaultState();
            }
        }
        
        public override void Exit()
        {
            controller.animator.ResetTrigger(controller.wallGrabHash);
            controller.rb.velocity = Vector2.zero;
            controller.rb.gravityScale = StateMachine.defaultGravityScale;
            spriteRendTransform.localPosition = spriteRendTransform.localPosition.SetX(defaultSpriteXPos);
            wallGrabTimer = 0;
        }
    }
}