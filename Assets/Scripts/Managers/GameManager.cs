using System;
using CustomEvent;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static readonly Evt OnShadowCollide = new Evt();
        public static readonly Evt<int> OnPlayerFeed = new Evt<int>();
        public static readonly Evt<int> OnUpdateScore = new Evt<int>();

        public int score { get; private set; }


        protected override void Awake()
        {
            base.Awake();
            score = 0;
        }

        private void OnEnable()
        {
            OnPlayerFeed.AddListener(OnPlayerFeedHandler);
            Time.timeScale = 1f;
        }

        private void OnDisable()
        {
            OnPlayerFeed.RemoveListener(OnPlayerFeedHandler);
        }
        
        private void OnPlayerFeedHandler(int additionalSore_)
        {
            score += additionalSore_;
            OnUpdateScore.Invoke(score);
        }


    }
}
