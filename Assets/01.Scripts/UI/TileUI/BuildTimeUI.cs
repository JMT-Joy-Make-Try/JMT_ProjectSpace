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
        private TimeData timeData;
        private Transform buildTimePanel;
        private TextMeshProUGUI timeText;
        private Button buildCompleteButton;
        private Image fillBar;

        private void Start()
        {
            buildTimePanel = transform.Find("BuildTime");
            BuildingBase buildingBase = transform.parent.GetComponentInParent<BuildingBase>();
            timeData = buildingBase.GetBuildingData.buildTime;
            rootTile = buildingBase.transform.parent.GetComponentInParent<PlanetTile>();
            timeText = buildTimePanel.GetComponentInChildren<TextMeshProUGUI>();
            buildCompleteButton = transform.Find("BuildComplete").GetComponent<Button>();
            fillBar = buildTimePanel.Find("FillBar").Find("Fill").GetComponent<Image>();
            buildCompleteButton.onClick.AddListener(HandleBuildCompleteButton);
            buildCompleteButton.gameObject.SetActive(false);
            StartCoroutine(TimeCoroutine());
        }

        private void HandleBuildCompleteButton()
        {
            rootTile.AddInteraction<BuildingInteraction>();
            gameObject.SetActive(false);
        }

        private IEnumerator TimeCoroutine()
        {
            int minute = timeData.minute;
            int second = timeData.second;
            fillBar.fillAmount = 0;
            fillBar.DOFillAmount(1f, minute * 60 + second).SetEase(Ease.Linear);
            var waitTime = new WaitForSeconds(1);
            while (true)
            {
                timeText.text = minute.ToString("D2") + ":" + second.ToString("D2");
                yield return waitTime;

                if (second <= 0)
                {
                    if (minute <= 0)
                    {
                        buildTimePanel.gameObject.SetActive(false);
                        buildCompleteButton.gameObject.SetActive(true);
                    }
                    else
                    {
                        minute--;
                        second += 59;
                    }
                }
                else second--;
            }
        }
    }
}
