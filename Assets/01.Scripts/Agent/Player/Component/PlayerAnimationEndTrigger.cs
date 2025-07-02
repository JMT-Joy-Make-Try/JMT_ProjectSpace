using JMT.Agent;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerAnimationEndTrigger : MonoBehaviour
    {
        [SerializeField] private AnimationEndEventSO _animationEndEvent;

        private void AnimationEndTrigger()
        {
            _animationEndEvent?.Invoke();
        }
    }
}