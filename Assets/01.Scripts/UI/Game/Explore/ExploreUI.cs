using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace JMT.UISystem
{
    public enum PlanetImageType
    {
        Image1,
        Image2,
    }
    public class ExploreUI : PanelUI
    {
        [SerializeField] private PlanetUI planetUI;
        
        private Button leftPlanetButton, rightPlanetButton, exitButton;
        private Image planetImage1, planetImage2;
        private PlanetImageType currentType = PlanetImageType.Image1;

        private int left = 100, middle = 250, right = 400;
        private float tweenTime = 0.3f;

        private void Awake()
        {
            Transform explorePanelTrm = PanelTrm.Find("ExplorePanel");
            leftPlanetButton = explorePanelTrm.Find("LeftPlanetBtn").GetComponent<Button>();
            rightPlanetButton = explorePanelTrm.Find("RightPlanetBtn").GetComponent<Button>();
            exitButton = explorePanelTrm.Find("ExitBtn").GetComponent<Button>();
            planetImage1 = explorePanelTrm.Find("Image1").GetComponent<Image>();
            planetImage2 = explorePanelTrm.Find("Image2").GetComponent<Image>();

            leftPlanetButton.onClick.AddListener(HandleLeftPlanetButton);
            rightPlanetButton.onClick.AddListener(HandleRightPlanetButton);
            exitButton.onClick.AddListener(CloseUI);
        }

        private void HandleLeftPlanetButton()
        {
            SetPlanet(false);
        }

        private void HandleRightPlanetButton()
        {
            SetPlanet(true);
        }

        private void SetPlanet(bool isRight)
        {
            switch (currentType)
            {
                case PlanetImageType.Image1:
                    currentType = PlanetImageType.Image2;
                    ChangePlanet(planetImage1, planetImage2, isRight);
                    break;
                case PlanetImageType.Image2:
                    currentType = PlanetImageType.Image1;
                    ChangePlanet(planetImage2, planetImage1, isRight);
                    break;
            }
        }

        private void ChangePlanet(Image exitImage, Image enterImage, bool isRight)
        {
            enterImage.rectTransform.anchoredPosition = new(isRight ? right : left, 0);
            enterImage.rectTransform.DOAnchorPosX(middle, tweenTime);
            enterImage.DOFade(1f, tweenTime);
            exitImage.rectTransform.DOAnchorPosX(isRight ? left : right, tweenTime);
            exitImage.DOFade(0f, tweenTime);
        }
    }
}
