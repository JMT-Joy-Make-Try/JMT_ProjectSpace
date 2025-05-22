using DG.Tweening;
using UnityEngine;

namespace JMT.QuestSystem
{
    public class QuestPing : MonoBehaviour
    {
        private SpriteRenderer spriteCompo;
        private float duration = 0.3f;
        private Vector3 maxScale = new Vector3(0.05f, 0.05f, 0.05f);
        private Vector3 curScale = new Vector3(0.03f, 0.03f, 0.03f);

        public bool IsEnable { get; private set; }
        private void Awake()
        {
            spriteCompo = GetComponent<SpriteRenderer>();
        }

        public void EnablePing()
        {
            spriteCompo.DOFade(1f, duration);
            transform.localScale = maxScale;
            transform.DOScale(curScale, duration);
            IsEnable = true;
        }
        public void DisablePing()
        {
            spriteCompo.DOFade(0f, duration);
            IsEnable = false;
        }

        public void SelectPingLocation(bool isDown)
        {
            Vector3 vec = new(0, isDown ? 0.57f : 1f, 0);
            transform.localPosition = vec;
        }
    }
}
