using System;
using System.Collections.Generic;
using Controller;
using Managers;
using UnityEngine;

namespace AI
{
    public class ShadowController : MonoBehaviour
    {
        private int index = 0;

        private void OnEnable()
        {
            index = 0;
        }
        
        private void FixedUpdate()
        {
            if(MovementRecorder.movementInfos.Count == 0) return;
            if (index >= MovementRecorder.movementInfos.Count)
            {
                index = MovementRecorder.movementInfos.Count - 1;
            }
            
            var movementInfo = MovementRecorder.movementInfos[index];
            transform.position = movementInfo.position;

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
