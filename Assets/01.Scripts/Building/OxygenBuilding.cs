using JMT.Agent;
using JMT.Building.Component;
using JMT.Core;
using JMT.Core.Manager;
using JMT.Planets.Tile.Items;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace JMT.Building
{
    public class OxygenBuilding : ItemBuilding
    {
        [SerializeField] private int _purificationContainerAmount = 4;
        [SerializeField] private int _oxygenAmount = 50;
        [SerializeField] private float interactionDistance = 2.5f;
        [SerializeField] private Transform _visualTransform;
        
        private BuildingData _data;
        private PlayerCharacter.Player _player;
        private BuildingBuilder _buildingBuilder;
        
        private float _interactionDistanceSqr;
        private bool _isPlayerGetOxygen = false;

        protected override void Awake()
        {
            base.Awake();
            _buildingBuilder = GetBuildingComponent<BuildingBuilder>();
        }

        protected override void Start()
        {
            base.Start();
            BuildingManager.Instance.OxygenBuildings.Add(this);
            _data = GetBuildingComponent<BuildingData>();
            _player = AgentManager.Instance.Player;

            _interactionDistanceSqr = interactionDistance * interactionDistance;
        }
        
        protected override void AddEvents()
        {
            base.AddEvents();
            _buildingBuilder.OnCompleteEvent += HandleCompleteEvent;
        }
        
        protected override void RemoveEvents()
        {
            base.RemoveEvents();
            _buildingBuilder.OnCompleteEvent -= HandleCompleteEvent;
        }

        private void HandleCompleteEvent()
        {
            StartCoroutine(WorkCoroutine());
            Debug.Log("OxygenBuilding Start");
        }

        private void Update()
        {
            if (_player == null) return;

            Vector3 offset = _visualTransform.position - _player.transform.position;
            if (offset.sqrMagnitude <= _interactionDistanceSqr && !_isPlayerGetOxygen)
            {
                StartCoroutine(GiveOxygenCoroutine());
            }
            else if (offset.sqrMagnitude > _interactionDistanceSqr && _isPlayerGetOxygen)
            {
                _isPlayerGetOxygen = false;
            }
        }

        private IEnumerator GiveOxygenCoroutine()
        {
            if (_isPlayerGetOxygen) yield break;
            _isPlayerGetOxygen = true;
            while (_isPlayerGetOxygen)
            {
                if (GetOxygen())
                {
                    _player.HealthCompo.AddOxygen(_oxygenAmount);
                    Debug.Log("Player Get Oxygen");
                }

                yield return new WaitForSeconds(1f);
            }
        }

        private IEnumerator WorkCoroutine()
        {
            var ws = new WaitForSeconds(0.1f);

            var createItem = data.GetFirstCreateItem();
            while (true)
            {
                if (createItem == null || data.CreateItemList.Count <= 0 || data.Works.Count <= 0)
                {
                    yield return ws;
                    continue;
                }

                int timeSec = createItem.CreateTime.GetSecond();
                //var purificationItem = _data.CurrentItems.Find(x => x.Item1 == ItemType.PurificationContainer);
                var purificationItem = _data.CurrentItems.FirstOrDefault(x => x.Item1 == ItemType.StaleOxygen);
                if (purificationItem?.Item2 >= _purificationContainerAmount)
                {
                    yield return ws;
                    continue;
                }
                
                yield return new WaitForSeconds(timeSec);

                data.AddItem();
            }
        }

        public bool GetOxygen()
        {
            var index = _data.CurrentItems.FindIndex(i => i.Item1 == ItemType.StaleOxygen);
            if (index < 0) return false;

            var item = _data.CurrentItems[index];

            if (item.Item2 <= 0)
            {
                _data.CurrentItems.RemoveAt(index);
                return false;
            }

            item.Item2--;
            _data.CurrentItems[index] = item;

            return true;
        }
    }
}
