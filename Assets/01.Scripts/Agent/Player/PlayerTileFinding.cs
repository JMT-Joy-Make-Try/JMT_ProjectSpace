using JMT.Core;
using JMT.Planets.Tile;
using JMT.UISystem;
using JMT.UISystem.Interact;
using UnityEngine;

namespace JMT.PlayerCharacter
{
    public class PlayerTileFinding : MonoBehaviour, IPlayerComponent
    {
        private Player player;
        [SerializeField] private Transform startTrm;
        [SerializeField] private Vector3 rotateVec;
        [SerializeField] private float rayDistance = 4f;
        private RaycastHit hit;

        public Vector3 RayDirection => (startTrm.forward + rotateVec).normalized;
        public RaycastHit RayHit => hit;
        
        public void Init(IPlayer p)
        {
            player = p as Player;
        }

        private void Update()
        {
            TileFind(GameUIManager.Instance.InteractCompo.InteractType);
        }

        private void TileFind(InteractType type)
        {
            var tileManager = TileManager.Instance;
            if (tileManager.CurrentTile != null)
                tileManager.CurrentTile.EdgeEnable(false);
            if (type == InteractType.Attack) return;
            if (Physics.Raycast(startTrm.position, RayDirection, out hit, rayDistance, player.GroundLayer))
            {
                var hitObject = hit.transform.gameObject;
                if (hitObject.transform.parent.TryGetComponent(out IInteractable interactable))
                {
                    GameUIManager.Instance.InteractCompo.ChangeInteract(InteractType.Trader);
                    return;
                }
                if (hitObject.TryGetComponent(out PlanetTile planetTile))
                {
                    tileManager.CurrentTile = planetTile;
                }
                //tileManager.CurrentTile = hit.transform.GetComponent<PlanetTile>();
                tileManager.CurrentTile?.EdgeEnable(true);
                GameUIManager.Instance.InteractCompo.ChangeInteract(tileManager.GetInteractType());
            }
        }

        private void OnDrawGizmos()
        {
            if (startTrm == null) return;
            Gizmos.color = Color.red;
            Vector3 rayStart = startTrm.position;

            Gizmos.DrawLine(rayStart, rayStart + RayDirection * rayDistance);
            Gizmos.DrawSphere(rayStart + RayDirection * rayDistance, 0.05f);
        }

        
    }
}
