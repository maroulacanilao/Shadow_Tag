using System.Collections;
using CustomHelpers;
using Managers;
using UnityEngine;

namespace Player.ControllerState
{
    public class DashState : PlayerState
    {
        public DashState(PlayerStateMachine stateMachine_, string animEndEventId_ = null) : base(stateMachine_, animEndEventId_)
        {
        }
    
        public override void Enter()
        {
            base.Enter();
            controller.animator.SetTrigger(controller.dashHash);
            controller.StartCoroutine(Co_Dash());
        }

        IEnumerator Co_Dash()
        {
            // var _dashDir = StateMachine.inputNormalized;
            var _dashDir = InputManager.MoveDelta.Normalized;
            
            if (_dashDir == Vector2.zero) _dashDir = StateMachine.facingLeft ? Vector2.left : Vector2.right;
        
            // Dash
            rb.gravityScale = 0;
            var _dashVel = _dashDir * controller.dashForce;
            
            _dashVel.y = Mathf.Clamp(_dashVel.y, -10f, 10f);
            StateMachine.hasDashed = true;
            rb.velocity = _dashVel;
            AnimParamUpdate();
            yield return new WaitForSeconds(controller.dashDuration);
        
            StateMachine.timeLastDashed = Time.time;
            rb.gravityScale = StateMachine.defaultGravityScale;
        
            if(IsPlayerFalling()) yield break;
            StateMachine.ChangeState(StateMachine.GroundedState);
        }
    
        public override void Exit()
        {
            base.Exit();
            rb.ResetVelocity();
            controller.StopCoroutine(Co_Dash());
            controller.animator.ResetTrigger(controller.dashHash);
        }
    }
}
