using UnityEngine;
using UnityEngine.Playables;

namespace JMT
{
    public class SkyboxChanger : PlayableBehaviour
    {
        public Material skyboxMaterial;

        public override void OnBehaviourPlay(Playable playable, FrameData info)
        {
            if (skyboxMaterial != null)
            {
                RenderSettings.skybox = skyboxMaterial;
                DynamicGI.UpdateEnvironment();
            }
        }
    }
}
