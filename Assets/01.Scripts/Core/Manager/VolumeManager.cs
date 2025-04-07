using DG.Tweening;
using JMT.Core.Tool;
using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace JMT.Core.Manager
{
    public class VolumeManager : MonoSingleton<VolumeManager>
    {
        [Header("Volume")]
        [SerializeField] private Volume  _dayVolume;
        [SerializeField] private Volume  _nightVolume;
        [SerializeField] private float _duration;
        
        private Volume _currentVolume;
        
        private void Start()
        {
            DaySystem.Instance.OnChangeDaytimeEvent += OnChangeDaytime;
            _currentVolume = _dayVolume;
        }
        private void OnDestroy()
        {
            if (DaySystem.Instance == null) return;
            DaySystem.Instance.OnChangeDaytimeEvent -= OnChangeDaytime;
        }

        private void OnChangeDaytime(DaytimeType day)
        {
            if (day == DaytimeType.Day)
            {
                _currentVolume = _nightVolume.ChangeVolume(_dayVolume, _duration);
            }
            else
            {
                _currentVolume = _dayVolume.ChangeVolume(_nightVolume, _duration);
            }
        }

        public T GetVolume<T>() where T : VolumeComponent
        {
            _currentVolume.profile.TryGet(out T component);
            return component;
        }
    }
}