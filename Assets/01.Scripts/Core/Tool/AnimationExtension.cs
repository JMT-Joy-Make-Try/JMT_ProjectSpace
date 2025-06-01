using UnityEngine;

namespace JMT.Core.Tool
{
    public static class AnimationExtension
    {
        public static void ChangeAnimation(this Animator animation, string animationName, AnimationClip animationClip)
        {
            if (animation == null || animationClip == null)
            {
                Debug.LogWarning("Animator or AnimationClip is null.");
                return;
            }
            
            if (animation.HasState(0, Animator.StringToHash(animationName)))
            {
                animation.runtimeAnimatorController.animationClips[0] = animationClip;
                animation.Play(animationName);
            }
            else
            {
                Debug.LogWarning($"Animation '{animationName}' does not exist in the Animator.");
            }
        }
    }
}