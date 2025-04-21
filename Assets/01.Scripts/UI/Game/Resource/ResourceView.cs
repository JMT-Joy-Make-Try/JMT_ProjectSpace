using TMPro;
using UnityEngine;

namespace JMT.UISystem.Resource
{
    public class ResourceView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI fuelText, oxygenText;

        public void SetFuelText(int current, int max)
        {
            fuelText.text = current + "/" + max;
        }
        public void SetNpcText(int current, int max)
        {
            oxygenText.text = current + "/" + max;
        }
    }
}
