using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Unity.Cinemachine;
using UnityEngine;

namespace JMT.Core.Tool
{
    public static class DOTweenExtension
    {
        /// <summary>
        /// DOTween을 사용하여 float 값을 변경합니다. - CinemachineCamera 전용
        /// </summary>
        /// <param name="target">카메라</param>
        /// <param name="endValue">카메라 비율</param>
        /// <param name="duration">몇초동안 Tween할지</param>
        /// <returns></returns>
        public static TweenerCore<float, float, FloatOptions> DOZoom(this CinemachineCamera target, float endValue, float duration)
        {
            return DOTween.To(() => target.Lens.OrthographicSize, x => target.Lens.OrthographicSize = x, endValue, duration);
        }

        public static void DOZoom(this CinemachineCamera camera, float endValue, float duration, Ease ease = Ease.Linear)
        {
            DOVirtual.Float(camera.Lens.OrthographicSize, endValue, duration, value =>
            {
                camera.Lens.OrthographicSize = value;
            }).SetEase(ease);
        }

        /// <summary>
        /// DOTween을 사용하여 Vector3 값을 변경합니다. - Transform
        /// </summary>
        /// <param name="target">Transform</param>
        /// <param name="endValue">마지막 위치</param>
        /// <param name="duration">이동할 시간</param>
        /// <param name="lockX">X축 잠금</param>
        /// <param name="lockY">Y축 잠금</param>
        /// <param name="lockZ">Z축 잠금</param>
        /// <returns></returns>
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOMove(
            this Transform target, Vector3 endValue, float duration, bool lockX, bool lockY, bool lockZ)
        {
            Vector3 startValue = target.position;

            Vector3 targetPosition = new Vector3(
                lockX ? startValue.x : endValue.x,
                lockY ? startValue.y : endValue.y,
                lockZ ? startValue.z : endValue.z
            );

            return DOTween.To(() => target.position, x => target.position = x, targetPosition, duration);
        }

        public static void DOFloat(this float target, float endValue, float duration, Ease ease = Ease.Unset)
        {
            DOVirtual.Float(target, endValue, duration, value =>
            {
                target = value;
            }).SetEase(ease);
        }

        public static void DOColor(this Color target, Color endValue, float duration, Ease ease = Ease.Unset)
        {
            DOVirtual.Color(target, endValue, duration, value =>
            {
                target = value;
            }).SetEase(ease);
        }
    }
}