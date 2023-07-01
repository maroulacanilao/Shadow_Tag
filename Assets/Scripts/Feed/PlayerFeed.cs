using System;
using Managers;
using UnityEngine;

namespace Feed
{
    public class PlayerFeed : MonoBehaviour
    {
        [SerializeField] private int score;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.gameObject.CompareTag("Player")) return;
            
            GameManager.OnPlayerFeed.Invoke(score);
            Destroy(gameObject);
        }
    }
}