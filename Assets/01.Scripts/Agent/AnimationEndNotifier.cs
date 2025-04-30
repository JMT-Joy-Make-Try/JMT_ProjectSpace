using UnityEngine;

namespace JMT.Agent
{
    public class AnimationEndNotifier : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.GetComponent<AnimationEndTrigger>()?.SetAnimationEnd();
        }
    }
}