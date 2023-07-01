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
            foreach (var shadow in shadows)
            {
                Destroy(shadow.gameObject);
            }
            shadows.Clear();
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
            var _pos = MovementRecorder.movementInfos[0].position;
            var _shadow = Instantiate(shadowPrefab, _pos, Quaternion.identity);
            
            shadows.Add(_shadow);
        }
    }
}
