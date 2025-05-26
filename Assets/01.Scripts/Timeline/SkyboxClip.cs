using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

namespace JMT
{
    public class SkyboxClip : PlayableAsset
    {
        [SerializeField] private Material skyboxMaterial;

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<SkyboxChanger>.Create(graph); //막대 그래프를 생성

            var behaviour = playable.GetBehaviour();
            behaviour.skyboxMaterial = skyboxMaterial;
            return playable;
        }
    }
}
