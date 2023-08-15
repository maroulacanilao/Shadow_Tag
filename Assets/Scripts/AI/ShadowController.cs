using System;
using System.Collections.Generic;
using Controller;
using CustomHelpers;
using Managers;
using UnityEngine;

namespace AI
{
    public class ShadowController : MonoBehaviour
    {
        [SerializeField] protected Animator animator;
        [SerializeField] [NaughtyAttributes.AnimatorParam("animator")] private int isIdleHash;

        public int index { get; protected set; } = 0;
        private bool isBoosting = false;
        private float boostIntervalTimer;

        protected virtual void OnEnable()
        {
            index = MovementRecorder.lastIndex;
        }
        
        private void FixedUpdate()
        {
            if(MovementRecorder.movementInfos.Count == 0) return;
            if (index >= MovementRecorder.movementInfos.Count)
            {
                index = MovementRecorder.movementInfos.Count - 1;
            }
            
            var _movementInfo = MovementRecorder.movementInfos[index];
            var _transform = transform;
            
            _transform.position = _movementInfo.position;
            _transform.rotation = _movementInfo.rotation;
            
            animator.SetBool(isIdleHash, _movementInfo.velocity.magnitude.IsApproximatelyTo(0));
            
            index++;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject);
            if(!other.gameObject.CompareTag("Player")) return;
            
            GameManager.OnShadowCollide.Invoke();
        }
    }
}
