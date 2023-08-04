using System;
using CustomHelpers;
using NaughtyAttributes;
using UnityEngine;

namespace AI
{
    public class ShadowAnim : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private Animator animator;
        [SerializeField] [AnimatorParam("animator")] private int IsIdleHash;

        private void Awake()
        {
            if(rb == null) rb = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate()
        {
            var _isIdle = (rb.velocity.x.IsApproximatelyTo(0) && rb.velocity.y.IsApproximatelyTo(0));
            Debug.Log($"{gameObject.name} velocity: {rb.velocity}");
            animator.SetBool(IsIdleHash, _isIdle);
        }
    }
}
