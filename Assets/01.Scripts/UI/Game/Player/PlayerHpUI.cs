using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public class PlayerHpUI : MonoBehaviour
    {
        [SerializeField] private Image fill;

        public void SetHpBar(int currentHp, int maxHp)
        {
            Debug.Log("asdf");
            fill.DOFillAmount(currentHp / (float)maxHp, 0.2f);
        }
    }
}
