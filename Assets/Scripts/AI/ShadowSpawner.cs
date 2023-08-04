using System;
using System.Collections.Generic;
using Controller;
using Managers;
using UnityEngine;

namespace AI
{
    public class ShadowSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject shadowPrefab;
        [SerializeField] private float tickRate = 5f;
        
        private List<GameObject> shadows = new List<GameObject>();

        private float timer;
        
        private void Awake()
        {
            timer = 0f;
        }

        private void OnEnable()
        {
            GameManager.OnPlayerFeed.AddListener(OnPlayerFeedHandler);
        }

        private void OnDisable()
        {
            GameManager.OnPlayerFeed.RemoveListener(OnPlayerFeedHandler);
        }
        
        private void OnPlayerFeedHandler(int additionalScore_)
        {
            // foreach (var shadow in shadows)
            // {
            //     Destroy(shadow.gameObject);
            // }
            // shadows.Clear();
            var _count = (int) (shadows.Count * 0.5f) - 1;
            
            for (int i = 0; i < _count; i++)
            {
                if(shadows.Count == 0) break;
                
                var _shadow = shadows[0];
                shadows.RemoveAt(0);
                Destroy(_shadow.gameObject);
            }
            timer = 0f;
        }

        private void Update()
        {
            if (timer < tickRate)
            {
                timer += Time.deltaTime;
                return;
            }

            timer = 0f;
            var _pos = MovementRecorder.movementInfos[MovementRecorder.lastIndex].position;
            var _shadow = Instantiate(shadowPrefab, _pos, Quaternion.identity);
            
            shadows.Add(_shadow);
        }
    }
}
