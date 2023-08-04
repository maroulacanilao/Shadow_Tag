using System;
using CustomHelpers;
using Managers;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class PlayerLight : MonoBehaviour
    {
        [SerializeField] private Light2D light2D;
        [SerializeField] private AnimationCurve lightIntensityCurve;
        [SerializeField] [NaughtyAttributes.MinMaxSlider(0, 1)] private Vector2 lightIntensityMinMax;
        [SerializeField] private float lightTime;
        
        private float timer = 0;
        
        private void Awake()
        {
            if(light2D == null) light2D = GetComponent<Light2D>();
            GameManager.OnPlayerFeed.AddListener(ResetTimer);
        }

        private void OnDestroy()
        {
            GameManager.OnPlayerFeed.RemoveListener(ResetTimer);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            light2D.intensity = lightIntensityCurve.
                Evaluate(timer / lightTime).
                Remap(0, 1, lightIntensityMinMax.x, lightIntensityMinMax.y);
        }
        
        public void ResetTimer(int score_)
        {
            timer = 0;
        }
    }
}
