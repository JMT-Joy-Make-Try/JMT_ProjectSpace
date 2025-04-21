using UnityEngine;

namespace JMT.UISystem.Resource
{
    public class ResourceController : MonoBehaviour
    {
        [SerializeField] private ResourceView view;
        [SerializeField] private ResourceSO resourceSO;

        private ResourceModel model;

        public int MaxFuelValue => model.MaxFuelValue;
        public int MaxNpcValue => model.MaxNpcValue;
        public int CurrentFuelValue => model.CurrentFuelValue;
        public int CurrentNpcValue => model.CurrentNpcValue;

        private void Awake()
        {
            model = new ResourceModel(resourceSO.MaxFuelValue, resourceSO.MaxNpcValue);
            model.OnFuelValueChanged += view.SetFuelText;
            model.OnNpcValueChanged += view.SetNpcText;
        }
        private void Start()
        {
            view.SetFuelText(CurrentFuelValue, MaxFuelValue);
            view.SetNpcText(CurrentNpcValue, MaxNpcValue);
        }

        private void OnDestroy()
        {
            model.OnFuelValueChanged -= view.SetFuelText;
            model.OnNpcValueChanged -= view.SetNpcText;
        }

        public void AddFuel(int increaseValue) => model.AddFuel(increaseValue);
        public void AddNpc(int increaseValue) => model.AddNpc(increaseValue);
    }
}
