using TMPro;
using UnityEngine;

namespace JMT.UISystem
{
    public class ResourceUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fuelText, oxygenText;

        private void SetFuelText(int current, int max)
        {
            fuelText.text = current + "/" + max;
        }
        private void SetOxygenText(int current, int max)
        {
            oxygenText.text = current + "/" + max;
        }
    }
}
