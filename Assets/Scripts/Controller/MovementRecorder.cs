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
        public float time;
    }
    public class MovementRecorder : MonoBehaviour
    {
        public static readonly List<MovementInfo> movementInfos = new List<MovementInfo>();

        private void Awake()
        {
            movementInfos.Clear();
        }

        private void OnEnable()
        {
            GameManager.OnPlayerFeed.AddListener(Clear);
        }

        private void OnDisable()
        {
            GameManager.OnPlayerFeed.RemoveListener(Clear);
        }

        private void FixedUpdate()
        {
            movementInfos.Add(new MovementInfo
            {
                position = transform.position,
                velocity = GetComponent<Rigidbody2D>().velocity,
                time = Time.time
            });
        }
        
        private void Clear(int param_)
        {
            movementInfos.Clear();
        }
    }
}
