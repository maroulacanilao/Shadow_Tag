using System;
using System.Collections.Generic;
using CustomHelpers;
using Managers;
using UnityEngine;

namespace Controller
{
    public struct MovementInfo
    {
        public Vector2 position;
        public Vector2 velocity;
        public Quaternion rotation;
        public float time;
    }
    public class MovementRecorder : MonoBehaviour
    {
        private Rigidbody2D rb;
        public static readonly List<MovementInfo> movementInfos = new List<MovementInfo>();
        public static int lastIndex = 0;
        public static MovementInfo lastMovementIndex => movementInfos[lastIndex];
        private Vector3 lastPosition;
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            movementInfos.Clear();
        }

        private void OnEnable()
        {
            GameManager.OnPlayerFeed.AddListener(Clear);
            lastIndex = 0;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerFeed.RemoveListener(Clear);
        }

        private void FixedUpdate()
        {
            var _transform = transform;

            var _position = _transform.position;
            
            if(_position.IsApproximatelyTo(lastPosition,0.01f)) return;
            // if(movementInfos.Exists(m  => m.position.IsApproximatelyTo(_position,0.01f))) return;
            
            movementInfos.Add(new MovementInfo
            {
                position = _position,
                velocity = rb.velocity,
                rotation = _transform.rotation,
                time = Time.time
            });
            lastPosition = _position;
        }
        
        private void Clear(int param_)
        {
            lastIndex = movementInfos.Count - 1;
        }
    }
}
