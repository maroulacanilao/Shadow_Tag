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
        
        
    }
}
