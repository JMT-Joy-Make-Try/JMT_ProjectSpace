using JMT.Planets.Tile;
using JMT.UISystem;
using UnityEngine;

namespace JMT.Player
{
    public class PlayerTileFinding : MonoBehaviour
    {
        private Player player;
        [SerializeField] private Transform startTrm;
        [SerializeField] private Vector3 rotateVec;
        [SerializeField] private float rayDistance = 4f;

        private PlanetTile currentTile;

        public Vector3 RayDirection => (startTrm.forward + rotateVec).normalized;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            TileFind(InteractSystem.Instance.InteractType);
        }

        private void TileFind(InteractType type)
        {
            var tileManager = TileManager.Instance;
            if (tileManager.CurrentTile != null)
                tileManager.CurrentTile.EdgeEnable(false);
            if (type == InteractType.Attack) return;
            if (Physics.Raycast(startTrm.position, RayDirection, out RaycastHit hit, rayDistance, player.GroundLayer))
            {
                tileManager.CurrentTile = hit.transform.GetComponent<PlanetTile>();
                tileManager.CurrentTile.EdgeEnable(true);
                InteractSystem.Instance.ChangeInteract(tileManager.GetInteractType());
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
