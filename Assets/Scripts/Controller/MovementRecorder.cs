using System;
using System.Collections.Generic;
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
            
            movementInfos.Add(new MovementInfo
            {
                position = _transform.position,
                velocity = rb.velocity,
                rotation = _transform.rotation,
                time = Time.time
            });
        }
        
        private void Clear(int param_)
        {
            lastIndex = movementInfos.Count - 1;
        }
    }
}
