using System;
using UnityEngine;

namespace JMT.Agent
{
    public class AnimationEndTrigger : MonoBehaviour
    {
        public event Action OnAnimationEnd;
        
        public void SetAnimationEnd()
        {
            OnAnimationEnd?.Invoke();
        }
    }
}