using DG.Tweening;
using UnityEngine;

namespace JMT
{
    public class QuestPing : MonoBehaviour
    {
        private SpriteRenderer spriteCompo;
        private float duration = 0.3f;
        private Vector3 maxScale = new Vector3(0.05f, 0.05f, 0.05f);
        private Vector3 curScale = new Vector3(0.03f, 0.03f, 0.03f);
        private void Awake()
        {
            spriteCompo = GetComponent<SpriteRenderer>();
        }

        public void EnablePing()
        {
            spriteCompo.DOFade(1f, duration);
            transform.localScale = maxScale;
            transform.DOScale(curScale, duration);
        }
        public void DisablePing()
        {
            spriteCompo.DOFade(0f, duration);
        }
    }
}
