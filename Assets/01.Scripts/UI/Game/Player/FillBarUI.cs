using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class FillBarUI : MonoBehaviour
    {
        [SerializeField] private Transform canvas;
        [SerializeField] private Image fill;
        [SerializeField] private bool isPlayerLook = true;

        private void LateUpdate()
        {
            if (!isPlayerLook) return;

            canvas.transform.LookAt(Camera.main.transform.parent);
            canvas.transform.rotation = Quaternion.Euler(0, Camera.main.transform.parent.rotation.eulerAngles.y, 0);
        }

        public void SetHpBar(int currentHp, int maxHp)
        {
            fill.DOFillAmount(currentHp / (float)maxHp, 0.2f);
        }
    }
}
