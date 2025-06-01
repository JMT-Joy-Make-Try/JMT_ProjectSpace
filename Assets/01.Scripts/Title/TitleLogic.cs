using JMT.UISystem.Title;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JMT
{
    public class TitleLogic : MonoBehaviour
    {
        [SerializeField] private TitleInputSO inputSO;
        [SerializeField] private TitleAnimation titleAnim;
        [SerializeField] private TItleFade titleFade;

        private void Awake()
        {
            inputSO.OnTouchEvent += HandleTouchEvent;
            titleAnim.OnEndAnimationEvent += titleFade.StartAnimation;
            titleAnim.StartAnimation();
        }

        private void HandleTouchEvent()
        {
            if (titleAnim.IsEnd)
                SceneManager.LoadScene("Timeline");
            else
            {
                titleAnim.SkipAnimation();
            }
        }
    }
}
