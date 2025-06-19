using JMT.NightSummary.Component;
using System;
using UnityEngine;

namespace JMT.NightSummary
{
    public class NightSummaryManager : MonoSingleton<NightSummaryManager>
    {
        // 우주선의 완성도, 획득한 자원, 포섭된 일꾼, 설치되어있는 건물, 평판.
        [field: SerializeField] public RocketStatusModule RocketStatusModule { get; private set; }
        [field: SerializeField] public CollectItemModule CollectItemModule { get; private set; }
        [field: SerializeField] public NPCCollectModule NPCCollectModule { get; private set; }
        [field: SerializeField] public BuildingModule BuildingModule { get; private set; }
        [field: SerializeField] public ReputationModule ReputationModule { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            RocketStatusModule = new RocketStatusModule();
            CollectItemModule = new CollectItemModule();
            NPCCollectModule = new NPCCollectModule();
            BuildingModule = new BuildingModule();
            ReputationModule = new ReputationModule(NPCCollectModule);
        }

        public void ResetModules()
        {
            RocketStatusModule.Reset();
            CollectItemModule.Reset();
            NPCCollectModule.Reset();
            BuildingModule.Reset();
            ReputationModule.Reset();
        }
    }
}