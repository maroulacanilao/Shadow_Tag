using CustomHelpers;
using Managers;
using UnityEngine;

namespace Player.ControllerState
{
    [System.Serializable]
    public class GroundedState : PlayerState
    {
        public bool isGroundedState;
        public GroundedState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }

        public override void Enter()
        {
            base.Enter();
            StateMachine.hasDashed = false;
            controller.animator.SetTrigger(controller.groundedHash);
        }
    
        public override void HandleInput()
        {
            if(WillDash()) return;
            if (CanJump())
            {
                StateMachine.ChangeState(StateMachine.JumpState);
                return;
            }
        
            if (IsPlayerFalling()) return;
            rb.SetVelocityY(0);
        }

        public override void LogicUpdate()
        {
            AnimParamUpdate();
        }

        public override void PhysicsUpdate()
        {
            IsOnMovingPlatform();
            MovementUpdate();
        }
    
        public override void Exit()
        {
            base.Exit();
            controller.animator.ResetTrigger(controller.groundedHash);
            ControllerResetParent();
        }
    }
}
