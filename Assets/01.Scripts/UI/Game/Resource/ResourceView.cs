using JMT.Core.Tool;
using TMPro;
using UnityEngine;

namespace JMT.UISystem.Resource
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fuelText, oxygenText;

        public void SetFuelText(float current, float max)
        {
            fuelText.text = $"{current:F1} / {max:N0}";
        }
        public void SetNpcText(int current, int max)
        {
            oxygenText.text = current + "/" + max;
        }
    }
}
