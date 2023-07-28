using System.Collections;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private float tickRate = 1f;
        [SerializeField] private int multiplierTickRate = 5;
        [SerializeField] private Rigidbody2D playerRb;
        
        private float timer;
        private int scoreMultiplier;
        private int multiplierTickCounter;
        
        public int score { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            score = 0;
            scoreMultiplier = 1;
        }

        private void OnEnable()
        {
            StartCoroutine(ScoreTick());
            GameManager.OnPlayerFeed.AddListener(OnPlayerFeedHandler);
            Time.timeScale = 1f;
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            GameManager.OnPlayerFeed.RemoveListener(OnPlayerFeedHandler);
        }
        
        private void OnPlayerFeedHandler(int additionalSore_)
        {
            score += additionalSore_;
            GameManager.OnUpdateScore.Invoke(score);
        }
        
        private IEnumerator ScoreTick()
        {
            var _waiter = new WaitForSeconds(tickRate);
            
            scoreMultiplier = 1;
            multiplierTickCounter = 0;

            while (enabled)
            {
                yield return _waiter;
                if(!enabled) yield break;
                if(playerRb.velocity.magnitude < 0.1f) continue;

                    multiplierTickCounter++;
                if(multiplierTickCounter >= multiplierTickRate)
                {
                    multiplierTickCounter = 0;
                    scoreMultiplier++;
                }

                score += scoreMultiplier;
                GameManager.OnUpdateScore.Invoke(score);
            }
        }
    }
}
