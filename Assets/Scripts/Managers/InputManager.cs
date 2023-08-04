using CustomEvent;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Managers
{
    public class InputDelta
    {
        public Vector2 Normalized;
        public Vector2 Raw;
    }

    public class InputManager : SingletonPersistent<InputManager>, PlatformInput.IPlatformActions
    {
        private PlayerInput _playerInput;
        public static InputAction MoveAction { get; private set; }
        public static InputAction JumpAction { get; private set; }
        public static InputAction DashAction { get; private set; }
        public static InputAction AttackAction { get; private set; }
        public static InputAction GrabAction { get; private set; }

        public static InputDelta MoveDelta { get; private set; }
        public static InputDelta LookDelta { get; private set; }
        

        #region Events

        public static readonly Evt<InputAction.CallbackContext> OnMoveAction = new Evt<InputAction.CallbackContext>();
        public static readonly Evt<InputAction.CallbackContext> OnJumpAction = new Evt<InputAction.CallbackContext>();
        public static readonly Evt<InputAction.CallbackContext> OnDashAction = new Evt<InputAction.CallbackContext>();
        public static readonly Evt<InputAction.CallbackContext> OnAttackAction = new Evt<InputAction.CallbackContext>();
        public static readonly Evt<InputAction.CallbackContext> OnGrabAction = new Evt<InputAction.CallbackContext>();
        public static readonly Evt<InputAction.CallbackContext> OnPauseAction = new Evt<InputAction.CallbackContext>();

        #endregion

        protected override void Awake()
        {
            base.Awake();
            MoveDelta = new InputDelta();
            LookDelta = new InputDelta();
            _playerInput = GetComponent<PlayerInput>();
            MoveAction = _playerInput.actions["Move"];
            JumpAction = _playerInput.actions["Jump"];
            AttackAction = _playerInput.actions["Attack"];
            DashAction = _playerInput.actions["Dash"];
            GrabAction = _playerInput.actions["Grab"];
        }

        public void OnMove(InputAction.CallbackContext context_)
        {
            MoveDelta.Raw = context_.ReadValue<Vector2>();
            MoveDelta.Normalized = MoveDelta.Raw.normalized;
            OnMoveAction.Invoke(context_);
        }

        public void OnJump(InputAction.CallbackContext context_)
        {
            OnJumpAction.Invoke(context_);
        }
        
        public void OnDash(InputAction.CallbackContext context_)
        {
            OnDashAction.Invoke(context_);
        }
        
        public void OnAttack(InputAction.CallbackContext context_)
        {
            OnAttackAction.Invoke(context_);
        }
        
        public void OnGrab(InputAction.CallbackContext context_)
        {
            OnGrabAction.Invoke(context_);
        }
        public void OnPause(InputAction.CallbackContext context)
        {
            OnPauseAction.Invoke(context);
        }
    }
}