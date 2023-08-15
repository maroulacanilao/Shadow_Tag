using System;
using NaughtyAttributes;
using Player.ControllerState;
using UnityEngine;

namespace AI
{
    public class PlatformerShadowController : ShadowController
    {
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
        
        PlatformerMovementInfo movementInfo => PlatformerMovementRecorder.movementInfoList[index];
        private ControllerTypeState lastControllerTypeState = ControllerTypeState.Grounded;

        protected override void OnEnable()
        {
            index = PlatformerMovementRecorder.lastIndex;
        }

        private void FixedUpdate()
        {
            var _transform = transform;
            
            _transform.position = movementInfo.position;
            UpdateAnimation();
            
            index++;
        }

        private void UpdateAnimation()
        {
            animator.SetFloat(xSpeedHash, movementInfo.velocity.x);
            
            UpdateOrientation();
            
            var _state = movementInfo.controllerTypeState;
            
            if(_state == lastControllerTypeState) return;
            
            ResetHash();
            lastControllerTypeState = _state;

            switch (_state)
            {
                case ControllerTypeState.Grounded:
                    animator.SetTrigger(groundedHash);
                    break;
                case ControllerTypeState.Jump:
                    animator.SetTrigger(jumpHash);
                    break;
                case ControllerTypeState.Fall:
                    animator.SetTrigger(fallHash);
                    break;
                case ControllerTypeState.Dash:
                    animator.SetTrigger(dashHash);
                    break;
                case ControllerTypeState.WallSlide:
                    animator.SetTrigger(wallSlideHash);
                    break;
                case ControllerTypeState.WallJump:
                    animator.SetTrigger(jumpHash);
                    break;
                case ControllerTypeState.WallGrab:
                    animator.SetTrigger(wallGrabHash);
                    break;
                default:
                    animator.SetTrigger(groundedHash);
                    break;
            }
        }
        
        private void ResetHash()
        {
            animator.ResetTrigger(groundedHash);
            animator.ResetTrigger(jumpHash);
            animator.ResetTrigger(fallHash);
            animator.ResetTrigger(dashHash);
            animator.ResetTrigger(wallSlideHash);
            animator.ResetTrigger(wallGrabHash);
        }
        
        private void UpdateOrientation()
        {
            var _transform = transform;
            var _localScale = _transform.localScale;
            var _velocity = movementInfo.velocity;
            
            if (_velocity.x > 0)
            {
                _localScale.x = 1;
            }
            else if (_velocity.x < 0)
            {
                _localScale.x = -1;
            }

            _transform.localScale = _localScale;
        }
    }
}