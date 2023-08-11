using System;
using System.Collections.Generic;
using System.Linq;
using Controller;
using Managers;
using UnityEngine;

namespace AI
{
    public class ShadowSpawner : MonoBehaviour
    {
        [SerializeField] private ShadowSpawnIndicator shadowIndicator;
        [SerializeField] private GameObject shadowPrefab;
        [SerializeField] private float tickRate = 5f;
        
        private List<ShadowController> shadows = new List<ShadowController>();

        private float timer;
        
        private void Awake()
        {
            timer = 0f;
        }

        private void OnEnable()
        {
            GameManager.OnPlayerFeed.AddListener(OnPlayerFeedHandler);
            var _player = GameObject.FindWithTag("Player").transform;

            var _startInfo = new MovementInfo()
            {
                position = _player.position,
                rotation = _player.rotation,
            };
            shadowIndicator.SetPosition(_startInfo);
        }

        private void OnDisable()
        {
            GameManager.OnPlayerFeed.RemoveListener(OnPlayerFeedHandler);
        }
        
        private void OnPlayerFeedHandler(int additionalScore_)
        {
            var _count = (int) (shadows.Count * 0.5f);
            Debug.Log($"Num of shadows to Destroy: {_count}");
            
            var _shadowsOrdered = shadows.OrderByDescending(s => s.index).ToList();
            
            for (int i = 0; i < _count; i++)
            {
                if(shadows.Count == 0) break;
                
                var _shadow = _shadowsOrdered[0];
                _shadowsOrdered.RemoveAt(0);
                shadows.Remove(_shadow);
                Destroy(_shadow.gameObject);
            }
            timer = 0f;
            shadowIndicator.SetPosition(MovementRecorder.lastMovementIndex);
        }

        private void Update()
        {
            if (timer < tickRate)
            {
                timer += Time.deltaTime;
                shadowIndicator.SetProgress(timer / tickRate);
                return;
            }

            timer = 0f;
            var _pos = MovementRecorder.lastMovementIndex.position;
            var _shadow = Instantiate(shadowPrefab, _pos, Quaternion.identity);
            
            if(!_shadow.TryGetComponent(out ShadowController _shadowController)) return; 
            
            shadows.Add(_shadowController);
        }
    }
}
