using System;
using UnityEngine;
using UnityEngine.Events;

public class BrokenAction : MonoBehaviour
{
    [Serializable]
    public class OnBrokenAction : UnityEvent
    {
    }

    public OnBrokenAction onBrokenComplete;

    public void OnAnimationComplete()
    {
        onBrokenComplete?.Invoke();
    }
}
