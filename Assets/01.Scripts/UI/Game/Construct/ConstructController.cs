using JMT.Building;
using JMT.Core.Manager;
using JMT.Planets.Tile;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem.Interact
{
    public class ConstructController : MonoBehaviour
    {
        [SerializeField] private ConstructView view;
        [SerializeField] private BuildInfoView infoView;
        [SerializeField] private Button exitButton;

        [SerializeField] private PVCBuilding pvcObject;

        private readonly ConstructModel model = new();

        private void Awake()
        {
            infoView.OnBuildEvent += HandleBuildButton;
            view.OnCategoryChangedEvent += HandleChangeCategory;
            view.OnInfoEvent += HandleInfo;

            exitButton.onClick.AddListener(CloseUI);
        }

        private void OnDestroy()
        {
            infoView.OnBuildEvent -= HandleBuildButton;
            view.OnCategoryChangedEvent -= HandleChangeCategory;
            view.OnInfoEvent -= HandleInfo;
            exitButton.onClick.RemoveListener(CloseUI);
        }

        private void HandleInfo(BuildingDataSO data)
        {
            BuildingManager.Instance.CurrentBuilding = data;
            infoView.SetInfo(data);
            TileManager.Instance.CurrentTile.TestBuild(BuildingManager.Instance.CurrentBuilding);
        }

        private void HandleChangeCategory(BuildingCategory category)
        {
            var list = model.SelectCategory(category);
            view.ChangeCell(list);
            view.SetButtonColor((int)category);
        }

        private void HandleBuildButton()
        {
            if (model.Build(TileManager.Instance.CurrentTile, pvcObject))
            {
                CloseUI();
            }
        }

        public void OpenUI()
        {
            GameUIManager.Instance.GameUICompo.CloseUI();
            GameUIManager.Instance.PlayerControlActive(false);
            view.OpenUI();

            HandleChangeCategory(BuildingCategory.ItemBuilding);
        }

        public void CloseUI()
        {
            if (!model.IsBuild)
                TileManager.Instance.CurrentTile.DestroyBuilding();

            infoView.CloseUI();
            view.CloseUI();
            GameUIManager.Instance.GameUICompo.OpenUI();
            GameUIManager.Instance.PlayerControlActive(true);

            model.SetIsBuild(false);
        }
    }
}
