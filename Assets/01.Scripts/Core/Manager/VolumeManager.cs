using JMT.Core.Tool;
using JMT.DayTime;
using JMT.UISystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace JMT.Core.Manager
{
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [Header("Volume")]
        [SerializeField] private Volume _dayVolume;
        [SerializeField] private Volume _nightVolume;
        [SerializeField] private float _duration;

        private Volume _currentVolume;

        private void Start()
        {
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent += OnChangeDaytime;
            _currentVolume = _dayVolume;
        }
        private void OnDestroy()
        {
            if (GameUIManager.Instance == null) return;
            if (GameUIManager.Instance.TimeCompo == null) return;
            GameUIManager.Instance.TimeCompo.OnChangeDaytimeEvent -= OnChangeDaytime;
        }

        private void OnChangeDaytime(DaytimeType day)
        {
            _currentVolume = day == DaytimeType.Day ? _nightVolume.ChangeVolume(_dayVolume, _duration) : _dayVolume.ChangeVolume(_nightVolume, _duration);
        }

        public T GetVolume<T>() where T : VolumeComponent
        {
            _currentVolume.profile.TryGet(out T component);
            return component;
        }

        public List<T> GetAllVolume<T>() where T : VolumeComponent
        {
            List<T> components = new();
            _dayVolume.profile.TryGet(out T component);
            if (component != null)
                components.Add(component);
            _nightVolume.profile.TryGet(out component);
            if (component != null)
                components.Add(component);
            return components;
        }
    }
}