using System;
using System.Collections;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Feed
{
    public class FeedSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerFeed feedPrefab;
        [SerializeField] private Collider2D[] spawnAreas;
        [SerializeField] private float waitTime = 2f;


        private PlayerFeed currentFeed;
        private bool isWaiting;

        private void OnEnable()
        {
            GameManager.OnPlayerFeed.AddListener(OnPlayerFeedWrapper);
            StartCoroutine(Co_Countdown());
        }
        
        private void OnDisable()
        {
            GameManager.OnPlayerFeed.RemoveListener(OnPlayerFeedWrapper);
        }

        private void OnPlayerFeedWrapper(int param) => StartCoroutine(Co_Countdown());
        
        
        private void SpawnFeed()
        {
            var spawnArea = spawnAreas[Random.Range(0, spawnAreas.Length)];
            var spawnPosition = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                0f);
            
            currentFeed = Instantiate(feedPrefab, spawnPosition, Quaternion.identity);
        }

        private IEnumerator Co_Countdown()
        {
            if(isWaiting) yield break;
            isWaiting = true;
            yield return new WaitForSeconds(waitTime);
            
            SpawnFeed();
            isWaiting = false;
        }
    }
}
