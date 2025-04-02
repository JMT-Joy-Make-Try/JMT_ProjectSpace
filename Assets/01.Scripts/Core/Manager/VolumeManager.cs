using DG.Tweening;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JMT.Core.Manager
{
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [SerializeField] private Volume _volume;
        
        public T GetVolume<T>() where T : VolumeComponent
        {
            _volume.profile.TryGet(out T component);
            return component;
        }
        
        public void SetIntensity<T>(float startValue, float endValue, float time) where T : VolumeComponent
        {
            var volume = GetVolume<T>();
            if (HasIntensity(volume, out var intensityValue))
            {
                DOVirtual.Float(startValue, endValue, time, value => intensityValue.SetValue(volume, value));
            }
            
        }

        private bool HasIntensity<T>(T volume, out FieldInfo intensityValue) where T : VolumeComponent
        {
            var intensity = volume.GetType().GetField("intensity", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
            
            if (intensity != null)
            {
                intensityValue = intensity;
                return true;
            }
            
            else
            {
                intensityValue = null;
                return false;
            }
        }
    }
}