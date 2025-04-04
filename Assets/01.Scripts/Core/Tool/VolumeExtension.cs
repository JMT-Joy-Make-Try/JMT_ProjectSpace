using DG.Tweening;
using UnityEngine.Rendering;

namespace JMT.Core.Tool
{
    public static class VolumeExtension
    {
        public static Volume ChangeVolume(this Volume volume, Volume newVolume, float duration)
        {
            DOVirtual.Float(0, 1, duration, (x) =>
            {
                volume.weight = 1 - x;
                newVolume.weight = x;
            });

            return volume;
        }
    }
}