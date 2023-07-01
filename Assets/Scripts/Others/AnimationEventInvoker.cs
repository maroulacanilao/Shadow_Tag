using System.Collections;
using System.Collections.Generic;
using CustomEvent;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventInvoker : MonoBehaviour
{
    public readonly Evt<string> OnAnimationEvent = new Evt<string>();
    
    public void InvokeAnimationEvent(string eventId_)
    {
        OnAnimationEvent.Invoke(eventId_);
    }
}
