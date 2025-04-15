using JMT.Resource;
using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fuelText, oxygenText;

        private void Awake()
        {
            ResourceManager.Instance.OnFuelValueChanged += SetFuelText;
            ResourceManager.Instance.OnNpcValueChanged += SetNpcText;
        }

        private void SetFuelText(int current, int max)
        {
            fuelText.text = current + "/" + max;
        }
        private void SetNpcText(int current, int max)
        {
            oxygenText.text = current + "/" + max;
        }
    }
}
