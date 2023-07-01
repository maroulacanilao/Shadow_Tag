using CustomHelpers;

namespace Player.ControllerState
{
    public class HitState : PlayerState
    {
        public HitState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }
        
        
        public override void Enter()
        {
            base.Enter();
            rb.ResetVelocity();
            rb.gravityScale = 0;
            controller.animator.SetTrigger(controller.hitHash);
        }
        
        public override void Exit()
        {
            base.Exit();
            controller.animator.ResetTrigger(controller.hitHash);
            rb.gravityScale = StateMachine.defaultGravityScale;
        }

        protected override void OnAnimEnd()
        {

            if(StateMachine.isGrounded)
            {
                StateMachine.ChangeState(StateMachine.GroundedState);
            }
            else StateMachine.ChangeState(StateMachine.FallState);
        }
    }
}