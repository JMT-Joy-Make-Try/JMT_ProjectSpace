using JMT.Building;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using JMT.Planets.Tile;

namespace JMT.UISystem
{
    public class BuildTimeUI : MonoBehaviour
    {
        private PlanetTile rootTile;
        private Transform buildTimePanel;
        private TextMeshProUGUI timeText;
        private Button buildCompleteButton;
        private Image fillBar;
        
        private float _buildPercent;
        private bool _isBuilding;

        private void Start()
        {
            buildTimePanel = transform.Find("BuildTime");
            BuildingBase buildingBase = transform.parent.GetComponentInParent<BuildingBase>();
            //timeData = buildingBase.BuildingData.buildTime;
            rootTile = buildingBase.transform.parent.GetComponentInParent<PlanetTile>();
            timeText = buildTimePanel.GetComponentInChildren<TextMeshProUGUI>();
            buildCompleteButton = transform.Find("BuildComplete").GetComponent<Button>();
            fillBar = buildTimePanel.Find("FillBar").Find("Fill").GetComponent<Image>();
            buildCompleteButton.onClick.AddListener(HandleBuildCompleteButton);
            buildCompleteButton.gameObject.SetActive(false);
        }

        private void HandleBuildCompleteButton()
        {
            rootTile.AddInteraction<BuildingInteraction>();
            gameObject.SetActive(false);
        }

        private void GaugeUp()
        {
            fillBar.fillAmount = _buildPercent;
        }
    }
}
